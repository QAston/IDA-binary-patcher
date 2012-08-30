using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;

namespace BinPatcher
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            MaximumSize = MinimumSize = Size;
            if (File.Exists(Properties.Settings.Default.LastTargetName))
                textBoxTargetFileName.Text = Properties.Settings.Default.LastTargetName;

            if (File.Exists(Properties.Settings.Default.LastDiffName))
                textBoxDiffFileName.Text = Properties.Settings.Default.LastDiffName;
        }

        private void buttonTargetFileSelect_Click(object sender, EventArgs e)
        {
            var dial = new OpenFileDialog();
            dial.CheckFileExists = true;
            dial.CheckPathExists = true;
            dial.Title = "Select binary file to patch";
            dial.ValidateNames = true;
            dial.Multiselect = false;
            if (!String.IsNullOrEmpty(textBoxTargetFileName.Text))
            {
                if (Directory.Exists(Path.GetDirectoryName(textBoxTargetFileName.Text)))
                    dial.InitialDirectory = Path.GetDirectoryName(textBoxTargetFileName.Text);
                if (File.Exists(textBoxTargetFileName.Text))
                    dial.FileName = textBoxTargetFileName.Text;
            }
            DialogResult res = dial.ShowDialog();
            if (res == DialogResult.OK)
                textBoxTargetFileName.Text = dial.FileName;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default["LastTargetName"] = textBoxTargetFileName.Text;
            Properties.Settings.Default["LastDiffName"] = textBoxDiffFileName.Text;
            Properties.Settings.Default.Save();
        }

        private void buttonDiffFileSelect_Click(object sender, EventArgs e)
        {
            var dial = new OpenFileDialog();
            dial.CheckFileExists = true;
            dial.CheckPathExists = true;
            dial.Title = "Select diff file";
            dial.ValidateNames = true;
            dial.Multiselect = false;
            dial.DefaultExt = ".dif";
            if (!String.IsNullOrEmpty(textBoxTargetFileName.Text) && Directory.Exists(Path.GetDirectoryName(textBoxTargetFileName.Text)))
            {
                dial.InitialDirectory = Path.GetDirectoryName(textBoxTargetFileName.Text);
                if (File.Exists(Path.GetDirectoryName(textBoxTargetFileName.Text) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(textBoxTargetFileName.Text) + ".dif"))
                    dial.FileName = Path.GetDirectoryName(textBoxTargetFileName.Text) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(textBoxTargetFileName.Text) + ".dif";
            }
            else if (!String.IsNullOrEmpty(textBoxDiffFileName.Text) && Directory.Exists(Path.GetDirectoryName(textBoxDiffFileName.Text)))
            {
                dial.InitialDirectory = Path.GetDirectoryName(textBoxDiffFileName.Text);
                if (File.Exists(textBoxDiffFileName.Text))
                    dial.FileName = textBoxDiffFileName.Text;
            }
            DialogResult res = dial.ShowDialog();
            if (res == DialogResult.OK)
                textBoxDiffFileName.Text = dial.FileName;
        }

        private void buttonPatch_Click(object sender, EventArgs e)
        {
            listViewStatus.Items.Clear();
            listViewStatus.Items.Add("Patching file: " + textBoxTargetFileName.Text + " with dif: " + textBoxDiffFileName.Text);
            if (!File.Exists(textBoxTargetFileName.Text))
            {
                listViewStatus.Items.Add("Aborted: file "+textBoxTargetFileName.Text+" does not exist");
                return;
            }
            if (!File.Exists(textBoxDiffFileName.Text))
            {
                listViewStatus.Items.Add("Aborted: file "+textBoxDiffFileName.Text+" does not exist");
                return;
            }
            FileStream targetFile = null;
            StreamReader difFile = null;
            try
            {
                if (radioButtonReplace.Checked)
                {
                    targetFile = new FileStream(textBoxTargetFileName.Text, FileMode.Open, FileAccess.ReadWrite);
                }
                else if (radioButtonRenameNew.Checked)
                {
                    var moveName = Path.GetDirectoryName(textBoxTargetFileName.Text) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(textBoxTargetFileName.Text) + "_patched";
                    int i = 0;
                    while (File.Exists(moveName + i + Path.GetExtension(textBoxTargetFileName.Text)))
                    {
                        ++i;
                    }
                    moveName = moveName + i + Path.GetExtension(textBoxTargetFileName.Text);
                    File.Copy(textBoxTargetFileName.Text, moveName);
                    targetFile = new FileStream(moveName, FileMode.Open, FileAccess.ReadWrite);
                }
                else if (radioButtonRenameOld.Checked)
                {
                    var moveName = Path.GetDirectoryName(textBoxTargetFileName.Text) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(textBoxTargetFileName.Text) + "_backup";
                    int i = 0;
                    while (File.Exists(moveName + i + Path.GetExtension(textBoxTargetFileName.Text)))
                    {
                        ++i;
                    }
                    moveName = moveName + i + Path.GetExtension(textBoxTargetFileName.Text);
                    File.Move(textBoxTargetFileName.Text, moveName);
                    File.Copy(moveName, textBoxTargetFileName.Text);
                    targetFile = new FileStream(textBoxTargetFileName.Text, FileMode.Open, FileAccess.ReadWrite);
                }
                else
                {
                    listViewStatus.Items.Add("Aborted: radio button error");
                    return;
                }
                difFile = new StreamReader(textBoxDiffFileName.Text);

                var str = difFile.ReadLine(); // title
                if (!str.Contains("This difference file has been created by IDA"))
                {
                    listViewStatus.Items.Add("Aborted: selected file is not a binary dif file");
                    if (targetFile != null)
                        targetFile.Dispose();
                    if (difFile != null)
                        difFile.Dispose();
                    return;
                }
                difFile.ReadLine(); // blank
                listViewStatus.Items.Add("Original dif target file name: " + difFile.ReadLine());

                int lineNum = 3;
                DialogResult? formAnswer = null;
                var datas = new List<Tuple<uint, uint>>();
                while (!difFile.EndOfStream)
                {
                    string buff = difFile.ReadLine();
                    var s = buff.Split(new char[] { ' ', ':', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                    uint offset = UInt32.Parse(s[0], NumberStyles.HexNumber);
                    uint origVal = UInt32.Parse(s[1], NumberStyles.HexNumber);
                    uint newVal = UInt32.Parse(s[2], NumberStyles.HexNumber);

                    string errormsg = "";

                    if (origVal > 0xFF && origVal != 0xFFFFFFFF || newVal > 0xFF && newVal != 0xFFFFFFFF)
                    {
                        listViewStatus.Items.Add("Aborted: selected file is not a binary dif file");
                        if (targetFile != null)
                            targetFile.Dispose();
                        if (difFile != null)
                            difFile.Dispose();
                        return;
                    }

                    if (offset >= targetFile.Length && origVal != 0xFFFFFFFF)
                        errormsg = "Dif error at line " + lineNum + ": target file does not have 0x" + s[0] + " offset, \n dif file expects 0x" + s[1] + " there. 0x"+ s[2] + " will replace current value";
                    else
                    {
                        targetFile.Seek(offset, SeekOrigin.Begin);
                        byte b = (byte)targetFile.ReadByte();
                        if (origVal == 0xFFFFFFFF && offset < targetFile.Length)
                            errormsg = "Dif error at line " + lineNum + ": target file has value 0x" + b.ToString("x") + " at 0x" + s[0] + " \n dif file expects end of file there . 0x" + s[2] + " will replace current value";
                        else
                        {
                            if (b != (byte)origVal)
                                errormsg = "Dif error at line " + lineNum + ": target file has value 0x" + b.ToString("x") + " at 0x" + s[0] + " \n dif file expects 0x" + s[1] + " there. 0x" + s[2] + " will replace current value";
                        }
                    }
                    
                    if (!String.IsNullOrEmpty(errormsg))
                    {
                        DialogResult res;
                        if (formAnswer == null)
                        {
                            var f = new FormChange();
                            f.labelErrorMessage.Text = errormsg;
                            res = f.ShowDialog();
                            if (f.checkBoxRepeatAction.Checked)
                                formAnswer = res;
                            f.Dispose();
                        }
                        else
                            res = (DialogResult)formAnswer;

                        switch (res)
                        {
                            case DialogResult.No:
                                listViewStatus.Items.Add("Dif change skipped by user at line " + lineNum + ".");
                                continue;
                            case DialogResult.Cancel:
                                listViewStatus.Items.Add("Patching canceled by user.");
                                if (targetFile != null)
                                    targetFile.Dispose();
                                if (difFile != null)
                                    difFile.Dispose();
                                return;
                        }
                    }
                    datas.Add(new Tuple<uint,uint>(offset, newVal));
                    ++lineNum;
                }
                uint endOffset = (uint)targetFile.Length;
                foreach (var d in datas)
                {
                    if (d.Item2 == 0xFFFFFFFF)
                    {
                        if (endOffset > d.Item1)
                            endOffset = d.Item1;
                        continue;
                    }
                    while (d.Item1 >= targetFile.Length)
                    {
                        targetFile.Seek(0, SeekOrigin.End);
                        targetFile.WriteByte(0);
                    }
                    targetFile.Seek(d.Item1, SeekOrigin.Begin);
                    byte old = (byte) targetFile.ReadByte();
                    targetFile.Seek(d.Item1, SeekOrigin.Begin);
                    targetFile.WriteByte((byte)d.Item2);
                    listViewStatus.Items.Add("-Replaced " + old.ToString("x") + " by " + ((byte)d.Item2).ToString("x") + " at 0x" + (d.Item1).ToString("x"));
                }
                if (endOffset > (uint)targetFile.Length)
                {
                    listViewStatus.Items.Add("File shortened by patch to length: " + endOffset.ToString("x") + ".");
                    targetFile.SetLength(endOffset);
                }
                listViewStatus.Items.Add("Patching successful!");
            }
            catch(Exception ex)
            {
                listViewStatus.Items.Add("Aborted: Error" + ex.Source);
                listViewStatus.Items.Add(ex.StackTrace);
                listViewStatus.Items.Add(ex.Message);
            }
            finally
            {
                if (targetFile != null)
                    targetFile.Dispose();
                if (difFile != null)
                    difFile.Dispose();
            }
        }
    }
}

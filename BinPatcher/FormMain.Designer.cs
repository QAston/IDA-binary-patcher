namespace BinPatcher
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxTargetFileName = new System.Windows.Forms.TextBox();
            this.textBoxDiffFileName = new System.Windows.Forms.TextBox();
            this.groupBoxTargetFile = new System.Windows.Forms.GroupBox();
            this.buttonTargetFileSelect = new System.Windows.Forms.Button();
            this.groupBoxDiffFile = new System.Windows.Forms.GroupBox();
            this.buttonDiffFileSelect = new System.Windows.Forms.Button();
            this.buttonPatch = new System.Windows.Forms.Button();
            this.radioButtonReplace = new System.Windows.Forms.RadioButton();
            this.radioButtonRenameOld = new System.Windows.Forms.RadioButton();
            this.radioButtonRenameNew = new System.Windows.Forms.RadioButton();
            this.groupBoxDiffApplyOptions = new System.Windows.Forms.GroupBox();
            this.listViewStatus = new System.Windows.Forms.ListView();
            this.groupBoxTargetFile.SuspendLayout();
            this.groupBoxDiffFile.SuspendLayout();
            this.groupBoxDiffApplyOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxTargetFileName
            // 
            this.textBoxTargetFileName.Location = new System.Drawing.Point(47, 19);
            this.textBoxTargetFileName.Name = "textBoxTargetFileName";
            this.textBoxTargetFileName.Size = new System.Drawing.Size(580, 20);
            this.textBoxTargetFileName.TabIndex = 0;
            // 
            // textBoxDiffFileName
            // 
            this.textBoxDiffFileName.Location = new System.Drawing.Point(47, 19);
            this.textBoxDiffFileName.Name = "textBoxDiffFileName";
            this.textBoxDiffFileName.Size = new System.Drawing.Size(580, 20);
            this.textBoxDiffFileName.TabIndex = 1;
            // 
            // groupBoxTargetFile
            // 
            this.groupBoxTargetFile.Controls.Add(this.buttonTargetFileSelect);
            this.groupBoxTargetFile.Controls.Add(this.textBoxTargetFileName);
            this.groupBoxTargetFile.Location = new System.Drawing.Point(12, 12);
            this.groupBoxTargetFile.Name = "groupBoxTargetFile";
            this.groupBoxTargetFile.Size = new System.Drawing.Size(633, 55);
            this.groupBoxTargetFile.TabIndex = 2;
            this.groupBoxTargetFile.TabStop = false;
            this.groupBoxTargetFile.Text = "Target File";
            // 
            // buttonTargetFileSelect
            // 
            this.buttonTargetFileSelect.Location = new System.Drawing.Point(7, 19);
            this.buttonTargetFileSelect.Name = "buttonTargetFileSelect";
            this.buttonTargetFileSelect.Size = new System.Drawing.Size(34, 20);
            this.buttonTargetFileSelect.TabIndex = 1;
            this.buttonTargetFileSelect.Text = "...";
            this.buttonTargetFileSelect.UseVisualStyleBackColor = true;
            this.buttonTargetFileSelect.Click += new System.EventHandler(this.buttonTargetFileSelect_Click);
            // 
            // groupBoxDiffFile
            // 
            this.groupBoxDiffFile.Controls.Add(this.buttonDiffFileSelect);
            this.groupBoxDiffFile.Controls.Add(this.textBoxDiffFileName);
            this.groupBoxDiffFile.Location = new System.Drawing.Point(12, 73);
            this.groupBoxDiffFile.Name = "groupBoxDiffFile";
            this.groupBoxDiffFile.Size = new System.Drawing.Size(633, 57);
            this.groupBoxDiffFile.TabIndex = 3;
            this.groupBoxDiffFile.TabStop = false;
            this.groupBoxDiffFile.Text = ".dif File";
            // 
            // buttonDiffFileSelect
            // 
            this.buttonDiffFileSelect.Location = new System.Drawing.Point(7, 19);
            this.buttonDiffFileSelect.Name = "buttonDiffFileSelect";
            this.buttonDiffFileSelect.Size = new System.Drawing.Size(34, 20);
            this.buttonDiffFileSelect.TabIndex = 2;
            this.buttonDiffFileSelect.Text = "...";
            this.buttonDiffFileSelect.UseVisualStyleBackColor = true;
            this.buttonDiffFileSelect.Click += new System.EventHandler(this.buttonDiffFileSelect_Click);
            // 
            // buttonPatch
            // 
            this.buttonPatch.Location = new System.Drawing.Point(12, 231);
            this.buttonPatch.Name = "buttonPatch";
            this.buttonPatch.Size = new System.Drawing.Size(75, 23);
            this.buttonPatch.TabIndex = 4;
            this.buttonPatch.Text = "Patch!";
            this.buttonPatch.UseVisualStyleBackColor = true;
            this.buttonPatch.Click += new System.EventHandler(this.buttonPatch_Click);
            // 
            // radioButtonReplace
            // 
            this.radioButtonReplace.AutoSize = true;
            this.radioButtonReplace.Location = new System.Drawing.Point(6, 19);
            this.radioButtonReplace.Name = "radioButtonReplace";
            this.radioButtonReplace.Size = new System.Drawing.Size(159, 17);
            this.radioButtonReplace.TabIndex = 7;
            this.radioButtonReplace.Text = "Replace old file with new file";
            this.radioButtonReplace.UseVisualStyleBackColor = true;
            // 
            // radioButtonRenameOld
            // 
            this.radioButtonRenameOld.AutoSize = true;
            this.radioButtonRenameOld.Checked = true;
            this.radioButtonRenameOld.Location = new System.Drawing.Point(6, 42);
            this.radioButtonRenameOld.Name = "radioButtonRenameOld";
            this.radioButtonRenameOld.Size = new System.Drawing.Size(163, 17);
            this.radioButtonRenameOld.TabIndex = 8;
            this.radioButtonRenameOld.TabStop = true;
            this.radioButtonRenameOld.Text = "Rename old file (_backupXX)";
            this.radioButtonRenameOld.UseVisualStyleBackColor = true;
            // 
            // radioButtonRenameNew
            // 
            this.radioButtonRenameNew.AutoSize = true;
            this.radioButtonRenameNew.Location = new System.Drawing.Point(6, 65);
            this.radioButtonRenameNew.Name = "radioButtonRenameNew";
            this.radioButtonRenameNew.Size = new System.Drawing.Size(172, 17);
            this.radioButtonRenameNew.TabIndex = 9;
            this.radioButtonRenameNew.Text = "Rename new file (_patchedXX)";
            this.radioButtonRenameNew.UseVisualStyleBackColor = true;
            // 
            // groupBoxDiffApplyOptions
            // 
            this.groupBoxDiffApplyOptions.Controls.Add(this.radioButtonReplace);
            this.groupBoxDiffApplyOptions.Controls.Add(this.radioButtonRenameNew);
            this.groupBoxDiffApplyOptions.Controls.Add(this.radioButtonRenameOld);
            this.groupBoxDiffApplyOptions.Location = new System.Drawing.Point(12, 136);
            this.groupBoxDiffApplyOptions.Name = "groupBoxDiffApplyOptions";
            this.groupBoxDiffApplyOptions.Size = new System.Drawing.Size(187, 89);
            this.groupBoxDiffApplyOptions.TabIndex = 10;
            this.groupBoxDiffApplyOptions.TabStop = false;
            this.groupBoxDiffApplyOptions.Text = "Dif apply options";
            // 
            // listViewStatus
            // 
            this.listViewStatus.Location = new System.Drawing.Point(217, 137);
            this.listViewStatus.Name = "listViewStatus";
            this.listViewStatus.Size = new System.Drawing.Size(422, 117);
            this.listViewStatus.TabIndex = 11;
            this.listViewStatus.UseCompatibleStateImageBehavior = false;
            this.listViewStatus.View = System.Windows.Forms.View.List;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 260);
            this.Controls.Add(this.listViewStatus);
            this.Controls.Add(this.groupBoxDiffApplyOptions);
            this.Controls.Add(this.buttonPatch);
            this.Controls.Add(this.groupBoxDiffFile);
            this.Controls.Add(this.groupBoxTargetFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.ShowIcon = false;
            this.Text = "Binary Patcher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.groupBoxTargetFile.ResumeLayout(false);
            this.groupBoxTargetFile.PerformLayout();
            this.groupBoxDiffFile.ResumeLayout(false);
            this.groupBoxDiffFile.PerformLayout();
            this.groupBoxDiffApplyOptions.ResumeLayout(false);
            this.groupBoxDiffApplyOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxTargetFileName;
        private System.Windows.Forms.TextBox textBoxDiffFileName;
        private System.Windows.Forms.GroupBox groupBoxTargetFile;
        private System.Windows.Forms.GroupBox groupBoxDiffFile;
        private System.Windows.Forms.Button buttonTargetFileSelect;
        private System.Windows.Forms.Button buttonDiffFileSelect;
        private System.Windows.Forms.Button buttonPatch;
        private System.Windows.Forms.RadioButton radioButtonReplace;
        private System.Windows.Forms.RadioButton radioButtonRenameOld;
        private System.Windows.Forms.RadioButton radioButtonRenameNew;
        private System.Windows.Forms.GroupBox groupBoxDiffApplyOptions;
        private System.Windows.Forms.ListView listViewStatus;
    }
}


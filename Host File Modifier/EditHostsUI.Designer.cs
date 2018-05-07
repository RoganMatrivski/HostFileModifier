namespace Host_File_Modifier
{
    partial class EditHostsUI
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
            this.label1 = new System.Windows.Forms.Label();
            this.typeList = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.linkBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.usernameTextbox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.repositoryLists = new System.Windows.Forms.ComboBox();
            this.githubGroup = new System.Windows.Forms.GroupBox();
            this.githubFilePath = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.linkGroup = new System.Windows.Forms.GroupBox();
            this.checkLink = new System.Windows.Forms.Button();
            this.fileGroup = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.filePath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveButton = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.loadJSON = new System.Windows.Forms.Button();
            this.githubGroup.SuspendLayout();
            this.linkGroup.SuspendLayout();
            this.fileGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Type";
            // 
            // typeList
            // 
            this.typeList.FormattingEnabled = true;
            this.typeList.Items.AddRange(new object[] {
            "Link",
            "Github Repository",
            "File"});
            this.typeList.Location = new System.Drawing.Point(12, 25);
            this.typeList.Name = "typeList";
            this.typeList.Size = new System.Drawing.Size(314, 21);
            this.typeList.TabIndex = 1;
            this.typeList.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Link to hosts File";
            // 
            // linkBox
            // 
            this.linkBox.Location = new System.Drawing.Point(8, 32);
            this.linkBox.Multiline = true;
            this.linkBox.Name = "linkBox";
            this.linkBox.Size = new System.Drawing.Size(299, 95);
            this.linkBox.TabIndex = 3;
            this.linkBox.TextChanged += new System.EventHandler(this.linkBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Github Username";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(233, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 60);
            this.button1.TabIndex = 5;
            this.button1.Text = "Get Repository";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // usernameTextbox
            // 
            this.usernameTextbox.Location = new System.Drawing.Point(9, 32);
            this.usernameTextbox.Name = "usernameTextbox";
            this.usernameTextbox.Size = new System.Drawing.Size(218, 20);
            this.usernameTextbox.TabIndex = 6;
            this.usernameTextbox.TextChanged += new System.EventHandler(this.usernameTextbox_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Repository";
            // 
            // repositoryLists
            // 
            this.repositoryLists.FormattingEnabled = true;
            this.repositoryLists.Location = new System.Drawing.Point(9, 71);
            this.repositoryLists.Name = "repositoryLists";
            this.repositoryLists.Size = new System.Drawing.Size(218, 21);
            this.repositoryLists.TabIndex = 8;
            this.repositoryLists.SelectedIndexChanged += new System.EventHandler(this.repositoryLists_SelectedIndexChanged);
            // 
            // githubGroup
            // 
            this.githubGroup.Controls.Add(this.githubFilePath);
            this.githubGroup.Controls.Add(this.label6);
            this.githubGroup.Controls.Add(this.label3);
            this.githubGroup.Controls.Add(this.repositoryLists);
            this.githubGroup.Controls.Add(this.button1);
            this.githubGroup.Controls.Add(this.label4);
            this.githubGroup.Controls.Add(this.usernameTextbox);
            this.githubGroup.Enabled = false;
            this.githubGroup.Location = new System.Drawing.Point(12, 220);
            this.githubGroup.Name = "githubGroup";
            this.githubGroup.Size = new System.Drawing.Size(314, 142);
            this.githubGroup.TabIndex = 9;
            this.githubGroup.TabStop = false;
            this.githubGroup.Text = "Github";
            // 
            // githubFilePath
            // 
            this.githubFilePath.FormattingEnabled = true;
            this.githubFilePath.Location = new System.Drawing.Point(9, 111);
            this.githubFilePath.Name = "githubFilePath";
            this.githubFilePath.Size = new System.Drawing.Size(299, 21);
            this.githubFilePath.TabIndex = 10;
            this.githubFilePath.SelectedIndexChanged += new System.EventHandler(this.githubFilePath_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "File";
            // 
            // linkGroup
            // 
            this.linkGroup.Controls.Add(this.checkLink);
            this.linkGroup.Controls.Add(this.label2);
            this.linkGroup.Controls.Add(this.linkBox);
            this.linkGroup.Location = new System.Drawing.Point(12, 52);
            this.linkGroup.Name = "linkGroup";
            this.linkGroup.Size = new System.Drawing.Size(314, 162);
            this.linkGroup.TabIndex = 10;
            this.linkGroup.TabStop = false;
            this.linkGroup.Text = "Link";
            // 
            // checkLink
            // 
            this.checkLink.Location = new System.Drawing.Point(233, 133);
            this.checkLink.Name = "checkLink";
            this.checkLink.Size = new System.Drawing.Size(75, 23);
            this.checkLink.TabIndex = 4;
            this.checkLink.Text = "Check";
            this.checkLink.UseVisualStyleBackColor = true;
            this.checkLink.Click += new System.EventHandler(this.checkLink_Click);
            // 
            // fileGroup
            // 
            this.fileGroup.Controls.Add(this.button2);
            this.fileGroup.Controls.Add(this.filePath);
            this.fileGroup.Controls.Add(this.label5);
            this.fileGroup.Enabled = false;
            this.fileGroup.Location = new System.Drawing.Point(12, 368);
            this.fileGroup.Name = "fileGroup";
            this.fileGroup.Size = new System.Drawing.Size(314, 63);
            this.fileGroup.TabIndex = 11;
            this.fileGroup.TabStop = false;
            this.fileGroup.Text = "File";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(233, 30);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Browse";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // filePath
            // 
            this.filePath.Location = new System.Drawing.Point(9, 32);
            this.filePath.Name = "filePath";
            this.filePath.Size = new System.Drawing.Size(218, 20);
            this.filePath.TabIndex = 1;
            this.filePath.TextChanged += new System.EventHandler(this.filePath_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "File Location";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // saveButton
            // 
            this.saveButton.Enabled = false;
            this.saveButton.Location = new System.Drawing.Point(170, 449);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 12;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(251, 449);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 14;
            this.button5.Text = "Close";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // loadJSON
            // 
            this.loadJSON.Location = new System.Drawing.Point(12, 449);
            this.loadJSON.Name = "loadJSON";
            this.loadJSON.Size = new System.Drawing.Size(75, 23);
            this.loadJSON.TabIndex = 15;
            this.loadJSON.Text = "loadJSON";
            this.loadJSON.UseVisualStyleBackColor = true;
            this.loadJSON.Click += new System.EventHandler(this.loadJSON_Click);
            // 
            // EditHostsUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 484);
            this.Controls.Add(this.loadJSON);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.fileGroup);
            this.Controls.Add(this.linkGroup);
            this.Controls.Add(this.githubGroup);
            this.Controls.Add(this.typeList);
            this.Controls.Add(this.label1);
            this.Name = "EditHostsUI";
            this.Text = "EditHostsUI";
            this.Load += new System.EventHandler(this.EditHostsUI_Load);
            this.githubGroup.ResumeLayout(false);
            this.githubGroup.PerformLayout();
            this.linkGroup.ResumeLayout(false);
            this.linkGroup.PerformLayout();
            this.fileGroup.ResumeLayout(false);
            this.fileGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox typeList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox linkBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox usernameTextbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox repositoryLists;
        private System.Windows.Forms.GroupBox githubGroup;
        private System.Windows.Forms.GroupBox linkGroup;
        private System.Windows.Forms.GroupBox fileGroup;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox filePath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ComboBox githubFilePath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button checkLink;
        private System.Windows.Forms.Button loadJSON;
    }
}
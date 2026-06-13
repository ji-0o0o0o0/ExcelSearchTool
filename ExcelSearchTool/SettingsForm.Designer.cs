
namespace ExcelSearchTool
{
    partial class SettingsForm
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblDefaultFolder = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbCaseSensitive = new System.Windows.Forms.RadioButton();
            this.rbCaseInsensitive = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbHiddenExclude = new System.Windows.Forms.RadioButton();
            this.rbHiddenInclude = new System.Windows.Forms.RadioButton();
            this.lblMoveFolder = new System.Windows.Forms.Label();
            this.btnSaveDefaultFolder = new System.Windows.Forms.Button();
            this.btnSaveBasicSettings = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.btnSelectDefaultFolder = new System.Windows.Forms.Button();
            this.btnSelectMoveFolder = new System.Windows.Forms.Button();
            this.btnSaveMoveFolder = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(22, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "설정";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(23, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "※ 기본 폴더 경로";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(23, 210);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "※ 기본설정";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(23, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(282, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "※ 검색어 포함 파일 이동시 폴더 경로";
            // 
            // lblDefaultFolder
            // 
            this.lblDefaultFolder.AutoSize = true;
            this.lblDefaultFolder.Location = new System.Drawing.Point(35, 74);
            this.lblDefaultFolder.Name = "lblDefaultFolder";
            this.lblDefaultFolder.Size = new System.Drawing.Size(63, 12);
            this.lblDefaultFolder.TabIndex = 4;
            this.lblDefaultFolder.Text = "(지정안됨)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbCaseSensitive);
            this.groupBox1.Controls.Add(this.rbCaseInsensitive);
            this.groupBox1.Location = new System.Drawing.Point(24, 238);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(306, 52);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "대소문자 구분";
            // 
            // rbCaseSensitive
            // 
            this.rbCaseSensitive.AutoSize = true;
            this.rbCaseSensitive.Location = new System.Drawing.Point(185, 22);
            this.rbCaseSensitive.Name = "rbCaseSensitive";
            this.rbCaseSensitive.Size = new System.Drawing.Size(59, 16);
            this.rbCaseSensitive.TabIndex = 6;
            this.rbCaseSensitive.TabStop = true;
            this.rbCaseSensitive.Text = "구분함";
            this.rbCaseSensitive.UseVisualStyleBackColor = true;
            // 
            // rbCaseInsensitive
            // 
            this.rbCaseInsensitive.AutoSize = true;
            this.rbCaseInsensitive.Location = new System.Drawing.Point(56, 22);
            this.rbCaseInsensitive.Name = "rbCaseInsensitive";
            this.rbCaseInsensitive.Size = new System.Drawing.Size(75, 16);
            this.rbCaseInsensitive.TabIndex = 0;
            this.rbCaseInsensitive.Text = "구분 안함";
            this.rbCaseInsensitive.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbHiddenExclude);
            this.groupBox2.Controls.Add(this.rbHiddenInclude);
            this.groupBox2.Location = new System.Drawing.Point(25, 296);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(306, 52);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "숨김시트 포함여부";
            // 
            // rbHiddenExclude
            // 
            this.rbHiddenExclude.AutoSize = true;
            this.rbHiddenExclude.Location = new System.Drawing.Point(55, 20);
            this.rbHiddenExclude.Name = "rbHiddenExclude";
            this.rbHiddenExclude.Size = new System.Drawing.Size(75, 16);
            this.rbHiddenExclude.TabIndex = 6;
            this.rbHiddenExclude.Text = "포함 안함";
            this.rbHiddenExclude.UseVisualStyleBackColor = true;
            // 
            // rbHiddenInclude
            // 
            this.rbHiddenInclude.AutoSize = true;
            this.rbHiddenInclude.Location = new System.Drawing.Point(184, 20);
            this.rbHiddenInclude.Name = "rbHiddenInclude";
            this.rbHiddenInclude.Size = new System.Drawing.Size(47, 16);
            this.rbHiddenInclude.TabIndex = 0;
            this.rbHiddenInclude.TabStop = true;
            this.rbHiddenInclude.Text = "포함";
            this.rbHiddenInclude.UseVisualStyleBackColor = true;
            // 
            // lblMoveFolder
            // 
            this.lblMoveFolder.AutoSize = true;
            this.lblMoveFolder.Location = new System.Drawing.Point(35, 148);
            this.lblMoveFolder.Name = "lblMoveFolder";
            this.lblMoveFolder.Size = new System.Drawing.Size(63, 12);
            this.lblMoveFolder.TabIndex = 8;
            this.lblMoveFolder.Text = "(지정안됨)";
            // 
            // btnSaveDefaultFolder
            // 
            this.btnSaveDefaultFolder.Location = new System.Drawing.Point(188, 93);
            this.btnSaveDefaultFolder.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveDefaultFolder.Name = "btnSaveDefaultFolder";
            this.btnSaveDefaultFolder.Size = new System.Drawing.Size(71, 21);
            this.btnSaveDefaultFolder.TabIndex = 11;
            this.btnSaveDefaultFolder.Text = "저장";
            this.btnSaveDefaultFolder.UseVisualStyleBackColor = true;
            this.btnSaveDefaultFolder.Click += new System.EventHandler(this.btnSaveDefaultFolder_Click);
            // 
            // btnSaveBasicSettings
            // 
            this.btnSaveBasicSettings.Location = new System.Drawing.Point(141, 353);
            this.btnSaveBasicSettings.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveBasicSettings.Name = "btnSaveBasicSettings";
            this.btnSaveBasicSettings.Size = new System.Drawing.Size(71, 21);
            this.btnSaveBasicSettings.TabIndex = 12;
            this.btnSaveBasicSettings.Text = "저장";
            this.btnSaveBasicSettings.UseVisualStyleBackColor = true;
            this.btnSaveBasicSettings.Click += new System.EventHandler(this.btnSaveBasicSettings_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(284, 13);
            this.btnBack.Margin = new System.Windows.Forms.Padding(2);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(71, 21);
            this.btnBack.TabIndex = 13;
            this.btnBack.Text = "이전으로";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnSelectDefaultFolder
            // 
            this.btnSelectDefaultFolder.Location = new System.Drawing.Point(113, 93);
            this.btnSelectDefaultFolder.Margin = new System.Windows.Forms.Padding(2);
            this.btnSelectDefaultFolder.Name = "btnSelectDefaultFolder";
            this.btnSelectDefaultFolder.Size = new System.Drawing.Size(71, 21);
            this.btnSelectDefaultFolder.TabIndex = 14;
            this.btnSelectDefaultFolder.Text = "폴더 선택";
            this.btnSelectDefaultFolder.UseVisualStyleBackColor = true;
            this.btnSelectDefaultFolder.Click += new System.EventHandler(this.btnSelectDefaultFolder_Click);
            // 
            // btnSelectMoveFolder
            // 
            this.btnSelectMoveFolder.Location = new System.Drawing.Point(113, 168);
            this.btnSelectMoveFolder.Margin = new System.Windows.Forms.Padding(2);
            this.btnSelectMoveFolder.Name = "btnSelectMoveFolder";
            this.btnSelectMoveFolder.Size = new System.Drawing.Size(71, 21);
            this.btnSelectMoveFolder.TabIndex = 16;
            this.btnSelectMoveFolder.Text = "폴더 선택";
            this.btnSelectMoveFolder.UseVisualStyleBackColor = true;
            this.btnSelectMoveFolder.Click += new System.EventHandler(this.btnSelectMoveFolder_Click);
            // 
            // btnSaveMoveFolder
            // 
            this.btnSaveMoveFolder.Location = new System.Drawing.Point(188, 168);
            this.btnSaveMoveFolder.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveMoveFolder.Name = "btnSaveMoveFolder";
            this.btnSaveMoveFolder.Size = new System.Drawing.Size(71, 21);
            this.btnSaveMoveFolder.TabIndex = 15;
            this.btnSaveMoveFolder.Text = "저장";
            this.btnSaveMoveFolder.UseVisualStyleBackColor = true;
            this.btnSaveMoveFolder.Click += new System.EventHandler(this.btnSaveMoveFolder_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(209, 13);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "btnResetSettings";
            this.button1.Size = new System.Drawing.Size(71, 21);
            this.button1.TabIndex = 17;
            this.button1.Text = "초기화";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnResetSettings_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 402);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSelectMoveFolder);
            this.Controls.Add(this.btnSaveMoveFolder);
            this.Controls.Add(this.btnSelectDefaultFolder);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnSaveBasicSettings);
            this.Controls.Add(this.btnSaveDefaultFolder);
            this.Controls.Add(this.lblMoveFolder);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblDefaultFolder);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblDefaultFolder;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbCaseSensitive;
        private System.Windows.Forms.RadioButton rbCaseInsensitive;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbHiddenExclude;
        private System.Windows.Forms.RadioButton rbHiddenInclude;
        private System.Windows.Forms.Label lblMoveFolder;
        private System.Windows.Forms.Button btnSaveDefaultFolder;
        private System.Windows.Forms.Button btnSaveBasicSettings;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.Button btnSelectDefaultFolder;
        private System.Windows.Forms.Button btnSelectMoveFolder;
        private System.Windows.Forms.Button btnSaveMoveFolder;
        private System.Windows.Forms.Button button1;
    }
}
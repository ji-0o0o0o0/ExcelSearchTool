
namespace ExcelSearchTool
{
    partial class DetailForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chkAllColumns = new System.Windows.Forms.CheckBox();
            this.btnSheetExport = new System.Windows.Forms.Button();
            this.btnSheetClose = new System.Windows.Forms.Button();
            this.clbColumns = new System.Windows.Forms.CheckedListBox();
            this.lblSheetKeyword = new System.Windows.Forms.Label();
            this.cmbColumnFilter = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtColumnFilter = new System.Windows.Forms.TextBox();
            this.btnSheetSearch = new System.Windows.Forms.Button();
            this.dgvSheet = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbSheet = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblSeachName = new System.Windows.Forms.Label();
            this.cmbSearchType = new System.Windows.Forms.ComboBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblFileName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSheetFileName = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSheet)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(2, -1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(930, 538);
            this.tabControl1.TabIndex = 13;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblSheetFileName);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.chkAllColumns);
            this.tabPage1.Controls.Add(this.btnSheetExport);
            this.tabPage1.Controls.Add(this.btnSheetClose);
            this.tabPage1.Controls.Add(this.clbColumns);
            this.tabPage1.Controls.Add(this.lblSheetKeyword);
            this.tabPage1.Controls.Add(this.cmbColumnFilter);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.txtColumnFilter);
            this.tabPage1.Controls.Add(this.btnSheetSearch);
            this.tabPage1.Controls.Add(this.dgvSheet);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.cmbSheet);
            this.tabPage1.Font = new System.Drawing.Font("굴림", 9F);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(922, 512);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "시트 검색";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // chkAllColumns
            // 
            this.chkAllColumns.AutoSize = true;
            this.chkAllColumns.Location = new System.Drawing.Point(748, 72);
            this.chkAllColumns.Name = "chkAllColumns";
            this.chkAllColumns.Size = new System.Drawing.Size(106, 16);
            this.chkAllColumns.TabIndex = 41;
            this.chkAllColumns.Text = "전체 선택/해제";
            this.chkAllColumns.UseVisualStyleBackColor = true;
            this.chkAllColumns.CheckedChanged += new System.EventHandler(this.chkAllColumns_CheckedChanged);
            // 
            // btnSheetExport
            // 
            this.btnSheetExport.Font = new System.Drawing.Font("굴림", 9F);
            this.btnSheetExport.Location = new System.Drawing.Point(759, 473);
            this.btnSheetExport.Margin = new System.Windows.Forms.Padding(2);
            this.btnSheetExport.Name = "btnSheetExport";
            this.btnSheetExport.Size = new System.Drawing.Size(71, 21);
            this.btnSheetExport.TabIndex = 39;
            this.btnSheetExport.Text = "추출";
            this.btnSheetExport.UseVisualStyleBackColor = true;
            this.btnSheetExport.Click += new System.EventHandler(this.btnSheetExport_Click);
            // 
            // btnSheetClose
            // 
            this.btnSheetClose.Font = new System.Drawing.Font("굴림", 9F);
            this.btnSheetClose.Location = new System.Drawing.Point(834, 473);
            this.btnSheetClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnSheetClose.Name = "btnSheetClose";
            this.btnSheetClose.Size = new System.Drawing.Size(71, 21);
            this.btnSheetClose.TabIndex = 38;
            this.btnSheetClose.Text = "닫기";
            this.btnSheetClose.UseVisualStyleBackColor = true;
            this.btnSheetClose.Click += new System.EventHandler(this.btnSheetClose_Click);
            // 
            // clbColumns
            // 
            this.clbColumns.CheckOnClick = true;
            this.clbColumns.FormattingEnabled = true;
            this.clbColumns.Location = new System.Drawing.Point(745, 94);
            this.clbColumns.Name = "clbColumns";
            this.clbColumns.Size = new System.Drawing.Size(169, 308);
            this.clbColumns.TabIndex = 37;
            this.clbColumns.KeyDown += new System.Windows.Forms.KeyEventHandler(this.clbColumns_KeyDown);
            // 
            // lblSheetKeyword
            // 
            this.lblSheetKeyword.AutoSize = true;
            this.lblSheetKeyword.Location = new System.Drawing.Point(367, 44);
            this.lblSheetKeyword.Name = "lblSheetKeyword";
            this.lblSheetKeyword.Size = new System.Drawing.Size(0, 12);
            this.lblSheetKeyword.TabIndex = 29;
            // 
            // cmbColumnFilter
            // 
            this.cmbColumnFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColumnFilter.Font = new System.Drawing.Font("굴림", 9F);
            this.cmbColumnFilter.FormattingEnabled = true;
            this.cmbColumnFilter.Location = new System.Drawing.Point(570, 40);
            this.cmbColumnFilter.Name = "cmbColumnFilter";
            this.cmbColumnFilter.Size = new System.Drawing.Size(111, 20);
            this.cmbColumnFilter.TabIndex = 28;
            this.cmbColumnFilter.SelectedIndexChanged += new System.EventHandler(this.cmbColumnFilter_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(318, 44);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 25;
            this.label5.Text = "검색어 : ";
            // 
            // txtColumnFilter
            // 
            this.txtColumnFilter.Font = new System.Drawing.Font("굴림", 9F);
            this.txtColumnFilter.Location = new System.Drawing.Point(687, 39);
            this.txtColumnFilter.Name = "txtColumnFilter";
            this.txtColumnFilter.Size = new System.Drawing.Size(152, 21);
            this.txtColumnFilter.TabIndex = 24;
            this.txtColumnFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtColumnFilter_KeyDown);
            // 
            // btnSheetSearch
            // 
            this.btnSheetSearch.Font = new System.Drawing.Font("굴림", 9F);
            this.btnSheetSearch.Location = new System.Drawing.Point(844, 39);
            this.btnSheetSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSheetSearch.Name = "btnSheetSearch";
            this.btnSheetSearch.Size = new System.Drawing.Size(71, 21);
            this.btnSheetSearch.TabIndex = 23;
            this.btnSheetSearch.Text = "검색";
            this.btnSheetSearch.UseVisualStyleBackColor = true;
            this.btnSheetSearch.Click += new System.EventHandler(this.btnSheetSearch_Click);
            // 
            // dgvSheet
            // 
            this.dgvSheet.AllowUserToOrderColumns = true;
            this.dgvSheet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSheet.EnableHeadersVisualStyles = false;
            this.dgvSheet.Location = new System.Drawing.Point(8, 71);
            this.dgvSheet.Name = "dgvSheet";
            this.dgvSheet.RowTemplate.Height = 23;
            this.dgvSheet.Size = new System.Drawing.Size(729, 428);
            this.dgvSheet.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(18, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 16);
            this.label3.TabIndex = 14;
            this.label3.Text = "시트선택 :";
            // 
            // cmbSheet
            // 
            this.cmbSheet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSheet.FormattingEnabled = true;
            this.cmbSheet.Location = new System.Drawing.Point(109, 39);
            this.cmbSheet.Name = "cmbSheet";
            this.cmbSheet.Size = new System.Drawing.Size(191, 20);
            this.cmbSheet.TabIndex = 0;
            this.cmbSheet.SelectedIndexChanged += new System.EventHandler(this.cmbSheet_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lblSeachName);
            this.tabPage2.Controls.Add(this.cmbSearchType);
            this.tabPage2.Controls.Add(this.btnExport);
            this.tabPage2.Controls.Add(this.btnClose);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.txtSearch);
            this.tabPage2.Controls.Add(this.btnSearch);
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Controls.Add(this.lblFileName);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Font = new System.Drawing.Font("굴림", 9F);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(922, 512);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "셀검색";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblSeachName
            // 
            this.lblSeachName.AutoSize = true;
            this.lblSeachName.Location = new System.Drawing.Point(70, 45);
            this.lblSeachName.Name = "lblSeachName";
            this.lblSeachName.Size = new System.Drawing.Size(0, 12);
            this.lblSeachName.TabIndex = 22;
            // 
            // cmbSearchType
            // 
            this.cmbSearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchType.Font = new System.Drawing.Font("굴림", 9F);
            this.cmbSearchType.FormattingEnabled = true;
            this.cmbSearchType.Location = new System.Drawing.Point(440, 41);
            this.cmbSearchType.Name = "cmbSearchType";
            this.cmbSearchType.Size = new System.Drawing.Size(111, 20);
            this.cmbSearchType.TabIndex = 21;
            this.cmbSearchType.SelectedIndexChanged += new System.EventHandler(this.cmbSearchType_SelectedIndexChanged);
            // 
            // btnExport
            // 
            this.btnExport.Font = new System.Drawing.Font("굴림", 9F);
            this.btnExport.Location = new System.Drawing.Point(755, 41);
            this.btnExport.Margin = new System.Windows.Forms.Padding(2);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(71, 21);
            this.btnExport.TabIndex = 20;
            this.btnExport.Text = "추출";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("굴림", 9F);
            this.btnClose.Location = new System.Drawing.Point(830, 41);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(71, 21);
            this.btnClose.TabIndex = 19;
            this.btnClose.Text = "닫기";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 45);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "검색어 : ";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("굴림", 9F);
            this.txtSearch.Location = new System.Drawing.Point(560, 40);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(111, 21);
            this.txtSearch.TabIndex = 17;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("굴림", 9F);
            this.btnSearch.Location = new System.Drawing.Point(677, 40);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(71, 21);
            this.btnSearch.TabIndex = 16;
            this.btnSearch.Text = "검색";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(19, 72);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(882, 417);
            this.dataGridView1.TabIndex = 15;
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblFileName.Location = new System.Drawing.Point(82, 15);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblFileName.Size = new System.Drawing.Size(0, 16);
            this.lblFileName.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(18, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 16);
            this.label1.TabIndex = 13;
            this.label1.Text = "파일명 : ";
            // 
            // lblSheetFileName
            // 
            this.lblSheetFileName.AutoSize = true;
            this.lblSheetFileName.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblSheetFileName.Location = new System.Drawing.Point(82, 15);
            this.lblSheetFileName.Name = "lblSheetFileName";
            this.lblSheetFileName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblSheetFileName.Size = new System.Drawing.Size(0, 16);
            this.lblSheetFileName.TabIndex = 43;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.Location = new System.Drawing.Point(18, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 16);
            this.label6.TabIndex = 42;
            this.label6.Text = "파일명 : ";
            // 
            // DetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 532);
            this.Controls.Add(this.tabControl1);
            this.Name = "DetailForm";
            this.Text = "DetailForm";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSheet)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lblSeachName;
        private System.Windows.Forms.ComboBox cmbSearchType;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSheetKeyword;
        private System.Windows.Forms.ComboBox cmbColumnFilter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtColumnFilter;
        private System.Windows.Forms.Button btnSheetSearch;
        private System.Windows.Forms.DataGridView dgvSheet;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbSheet;
        private System.Windows.Forms.CheckBox chkAllColumns;
        private System.Windows.Forms.Button btnSheetExport;
        private System.Windows.Forms.Button btnSheetClose;
        private System.Windows.Forms.CheckedListBox clbColumns;
        private System.Windows.Forms.Label lblSheetFileName;
        private System.Windows.Forms.Label label6;
    }
}
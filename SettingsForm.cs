using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExcelSearchTool
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            this.Text = "설정";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            // 대소문자 구분
            if (Properties.Settings.Default.CaseSensitive)
                rbCaseSensitive.Checked = true;
            else
                rbCaseInsensitive.Checked = true;

            // 숨김시트 포함여부
            if (Properties.Settings.Default.IncludeHiddenSheets)
                rbHiddenInclude.Checked = true;
            else
                rbHiddenExclude.Checked = true;

            // 기본 폴더 경로 표시
            if (!string.IsNullOrEmpty(Properties.Settings.Default.DefaultFolderPath))
            {
                lblDefaultFolder.Text = Properties.Settings.Default.DefaultFolderPath;
                toolTip1.SetToolTip(lblDefaultFolder, Properties.Settings.Default.DefaultFolderPath);
            }

            // 이동 폴더 경로 표시
            if (!string.IsNullOrEmpty(Properties.Settings.Default.MoveFolderPath)) { 
                lblMoveFolder.Text = Properties.Settings.Default.MoveFolderPath;
                toolTip2.SetToolTip(lblMoveFolder, Properties.Settings.Default.DefaultFolderPath);
            }
        }

        private void btnSelectDefaultFolder_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    lblDefaultFolder.Text = fbd.SelectedPath;
                    toolTip1.SetToolTip(lblDefaultFolder, fbd.SelectedPath);
                }
            }
        }

        private void btnSaveDefaultFolder_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DefaultFolderPath = lblDefaultFolder.Text;
            Properties.Settings.Default.Save();
            MessageBox.Show("저장되었습니다.");
        }

        private void btnSelectMoveFolder_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    lblMoveFolder.Text = fbd.SelectedPath;
                    toolTip2.SetToolTip(lblMoveFolder, fbd.SelectedPath);
                }
            }
        }

        private void btnSaveMoveFolder_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.MoveFolderPath = lblMoveFolder.Text;
            Properties.Settings.Default.Save();
            MessageBox.Show("저장되었습니다.");
        }

        private void btnSaveBasicSettings_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.CaseSensitive = rbCaseSensitive.Checked;
            Properties.Settings.Default.IncludeHiddenSheets = rbHiddenInclude.Checked;
            Properties.Settings.Default.Save();
            MessageBox.Show("저장되었습니다.");
        }

        private void btnResetSettings_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "모든 설정을 초기화하시겠습니까?\n(폴더 경로 포함)",
                "초기화 확인",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
                return;

            Properties.Settings.Default.DefaultFolderPath = "";
            Properties.Settings.Default.MoveFolderPath = "";
            Properties.Settings.Default.CaseSensitive = false;
            Properties.Settings.Default.IncludeHiddenSheets = false;
            Properties.Settings.Default.Save();

            // 화면 갱신
            lblDefaultFolder.Text = "(지정안됨)";
            lblMoveFolder.Text = "(지정안됨)";
            toolTip1.SetToolTip(lblDefaultFolder, "");
            toolTip1.SetToolTip(lblMoveFolder, "");
            rbCaseInsensitive.Checked = true;
            rbHiddenExclude.Checked = true;

            MessageBox.Show("초기화되었습니다.");
        }
    }
}

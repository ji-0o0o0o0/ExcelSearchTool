using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;

namespace ExcelSearchTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "엑셀 검색 도구";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string defaultFolder = Properties.Settings.Default.DefaultFolderPath;

            if (!string.IsNullOrEmpty(defaultFolder) && Directory.Exists(defaultFolder))
            {
                lblPath.Text = defaultFolder;
                toolTip1.SetToolTip(lblPath, defaultFolder);
                LoadFilesFromFolder(defaultFolder);
            }
            else
            {
                lblPath.Text = "(폴더주소)";
            }
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    string folderPath = fbd.SelectedPath;
                    lblPath.Text = folderPath;
                    toolTip1.SetToolTip(lblPath, folderPath);

                    LoadFilesFromFolder(folderPath);
                }
            }
        }
        private void LoadFilesFromFolder(string folderPath)
        {
            checkedListBox1.ItemCheck -= checkedListBox1_ItemCheck;
            checkedListBox1.Items.Clear();

            string[] xlsxFiles = Directory.GetFiles(folderPath, "*.xlsx");
            string[] xlsFiles = Directory.GetFiles(folderPath, "*.xls");
            string[] files = xlsxFiles.Concat(xlsFiles).ToArray();

            foreach (string filePath in files)
            {
                string fileName = Path.GetFileName(filePath);
                checkedListBox1.Items.Add(fileName, false);
            }

            chkAll.CheckedChanged -= chkAll_CheckedChanged;
            chkAll.Checked = false;
            chkAll.CheckedChanged += chkAll_CheckedChanged;

            checkedListBox1.ItemCheck += checkedListBox1_ItemCheck;
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            checkedListBox1.ItemCheck -= checkedListBox1_ItemCheck;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, chkAll.Checked);
            }
            checkedListBox1.ItemCheck += checkedListBox1_ItemCheck;
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // 약간 지연시켜서 체크 상태 반영 후 확인
            this.BeginInvoke((MethodInvoker)delegate
            {
                bool allChecked = true;
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (!checkedListBox1.GetItemChecked(i))
                    {
                        allChecked = false;
                        break;
                    }
                }

                // chkAll 이벤트 재귀 방지
                chkAll.CheckedChanged -= chkAll_CheckedChanged;
                chkAll.Checked = allChecked;
                chkAll.CheckedChanged += chkAll_CheckedChanged;
            });
        }
        private int SearchInFile(string filePath, string keyword, bool includeHidden, bool caseSensitive)
        {
            int matchCount = 0;
            IWorkbook workbook;

            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    if (filePath.ToLower().EndsWith(".xlsx"))
                        workbook = new XSSFWorkbook(fs);
                    else
                        workbook = new HSSFWorkbook(fs);
                }
            }
            catch (IOException)
            {
                MessageBox.Show($"{Path.GetFileName(filePath)} 파일이 열려있어 검사할 수 없습니다. 파일을 닫고 다시 시도해주세요.", "파일 사용 중", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }

            string compareKeyword = caseSensitive ? keyword : keyword.ToLower();

            for (int s = 0; s < workbook.NumberOfSheets; s++)
            {
                if (!includeHidden && (workbook.IsSheetHidden(s) || workbook.IsSheetVeryHidden(s)))
                    continue;

                ISheet sheet = workbook.GetSheetAt(s);

                for (int r = 0; r <= sheet.LastRowNum; r++)
                {
                    IRow row = sheet.GetRow(r);
                    if (row == null) continue;

                    for (int c = 0; c < row.LastCellNum; c++)
                    {
                        ICell cell = row.GetCell(c);
                        if (cell == null) continue;

                        string cellValue = cell.ToString();
                        string compareValue = caseSensitive ? cellValue : cellValue.ToLower();

                        if (compareValue.Contains(compareKeyword))
                        {
                            matchCount++;
                        }
                    }
                }
            }

            workbook.Close();
            return matchCount;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtKeyword.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("검색어를 입력하세요.");
                return;
            }

            if (dataGridView1.Columns.Count == 0)
            {
                dataGridView1.Columns.Add("No", "No");
                dataGridView1.Columns.Add("FileName", "파일명");
                dataGridView1.Columns.Add("Count", "매칭개수");
                dataGridView1.Columns["No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns["Count"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                var btnCol = new DataGridViewButtonColumn();
                btnCol.Name = "Detail";
                btnCol.HeaderText = "상세";
                btnCol.Text = "▶";
                btnCol.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(btnCol);

                dataGridView1.Columns["No"].Width = 40;
                dataGridView1.Columns["FileName"].Width = 300;
                dataGridView1.Columns["Count"].Width = 40;
                dataGridView1.Columns["Detail"].Width = 40;
            }
            dataGridView1.Rows.Clear();

            string folderPath = lblPath.Text;

            int rowNum = 1;
            int count = 0;
            bool includeHidden = Properties.Settings.Default.IncludeHiddenSheets;
            bool caseSensitive = Properties.Settings.Default.CaseSensitive;
            foreach (string fileName in checkedListBox1.CheckedItems)
            {
                string filePath = Path.Combine(folderPath, fileName.ToString());
                count = SearchInFile(filePath, keyword, includeHidden, caseSensitive);

                int idx = dataGridView1.Rows.Add(rowNum, fileName, count == -1 ? "사용 중" : count.ToString());

                // 매칭개수 0이거나 사용중이면 버튼 비활성화
                if (count <= 0)
                {
                    dataGridView1.Rows[idx].Cells["Detail"].Value = "";
                    ((DataGridViewButtonCell)dataGridView1.Rows[idx].Cells["Detail"]).FlatStyle = FlatStyle.Flat;
                    dataGridView1.Rows[idx].Cells["Detail"].Style.BackColor = Color.LightGray;
                    dataGridView1.Rows[idx].Cells["Detail"].ReadOnly = true;
                }
                rowNum++;
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.StartPosition = FormStartPosition.Manual;
            settingsForm.Location = new Point(this.Location.X + this.Width, this.Location.Y);
            settingsForm.ShowDialog();

            string defaultFolder = Properties.Settings.Default.DefaultFolderPath;

            if (!string.IsNullOrEmpty(defaultFolder) && Directory.Exists(defaultFolder))
            {
                lblPath.Text = defaultFolder;
                toolTip1.SetToolTip(lblPath, defaultFolder);
                LoadFilesFromFolder(defaultFolder);
            }
            else
            {
                lblPath.Text = "(폴더주소)";
                toolTip1.SetToolTip(lblPath, "");
                checkedListBox1.Items.Clear();
                dataGridView1.Rows.Clear();
            }
        }

        private void btnMoveFound_Click(object sender, EventArgs e)
        {
            string moveFolder = Properties.Settings.Default.MoveFolderPath;

            if (string.IsNullOrEmpty(moveFolder) || !Directory.Exists(moveFolder))
            {
                MessageBox.Show("이동 폴더가 설정되지 않았습니다. 설정에서 지정해주세요.");
                return;
            }

            string folderPath = lblPath.Text;

            // 이동 대상 파일 먼저 확인
            var targetFiles = new List<string>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["FileName"].Value == null) continue;

                string fileName = row.Cells["FileName"].Value.ToString();
                string countValue = row.Cells["Count"].Value.ToString();

                if (countValue != "사용 중" && int.TryParse(countValue, out int count) && count > 0)
                {
                    targetFiles.Add(fileName);
                }
            }

            if (targetFiles.Count == 0)
            {
                MessageBox.Show("이동할 파일이 없습니다.");
                return;
            }

            // 확인창
            var result = MessageBox.Show(
                $"{targetFiles.Count}개의 파일을 다음 폴더로 이동하시겠습니까?\n\n{moveFolder}",
                "이동 확인",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            // 이동 실행
            int movedCount = 0;
            foreach (string fileName in targetFiles)
            {
                string sourcePath = Path.Combine(folderPath, fileName);
                string destPath = Path.Combine(moveFolder, fileName);

                if (File.Exists(sourcePath))
                {
                    File.Move(sourcePath, destPath);
                    movedCount++;
                }
            }

            MessageBox.Show($"{movedCount}개 파일을 이동했습니다.");
            LoadFilesFromFolder(folderPath);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex != dataGridView1.Columns["Detail"].Index) return;
            if (dataGridView1.Rows[e.RowIndex].Cells["Detail"].ReadOnly) return;

            string fileName = dataGridView1.Rows[e.RowIndex].Cells["FileName"].Value.ToString();
            string filePath = Path.Combine(lblPath.Text, fileName);

            DetailForm detailForm = new DetailForm(filePath, txtKeyword.Text.Trim());
            detailForm.Show();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;

namespace ExcelSearchTool
{
    public partial class DetailForm : Form
    {

        private string _filePath;
        private string _keyword;
        private List<string[]> _allRows = new List<string[]>();

        public DetailForm(string filePath, string keyword)
        {
            InitializeComponent();
            _filePath = filePath;
            _keyword = keyword;
            lblFileName.Text = Path.GetFileName(filePath);
            lblSeachName.Text = keyword;

            // 콤보박스 항목
            cmbSearchType.Items.Add("전체");
            cmbSearchType.Items.Add("시트명");
            cmbSearchType.Items.Add("셀값");
            cmbSearchType.SelectedIndex = 0;
            txtSearch.Enabled = false;

            // 그리드 컬럼
            dataGridView1.Columns.Add("No", "No");
            dataGridView1.Columns.Add("SheetName", "시트명");
            dataGridView1.Columns.Add("Row", "행");
            dataGridView1.Columns.Add("Col", "열");
            dataGridView1.Columns.Add("Value", "셀값");
            dataGridView1.Columns["No"].Width = 40;
            dataGridView1.Columns["SheetName"].Width = 200;
            dataGridView1.Columns["Row"].Width = 50;
            dataGridView1.Columns["Col"].Width = 50;
            dataGridView1.Columns["Value"].Width = 370;
            dataGridView1.Columns["No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["Row"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["Col"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            LoadDetail();
        }

        private void LoadDetail()
        {
            _allRows.Clear();
            dataGridView1.Rows.Clear();

            IWorkbook workbook;
            try
            {
                using (FileStream fs = new FileStream(_filePath, FileMode.Open, FileAccess.Read))
                {
                    if (_filePath.ToLower().EndsWith(".xlsx"))
                        workbook = new XSSFWorkbook(fs);
                    else
                        workbook = new HSSFWorkbook(fs);
                }
            }
            catch (IOException)
            {
                MessageBox.Show("파일이 열려있어 불러올 수 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool includeHidden = Properties.Settings.Default.IncludeHiddenSheets;
            bool caseSensitive = Properties.Settings.Default.CaseSensitive;
            string compareKeyword = caseSensitive ? _keyword : _keyword.ToLower();

            int no = 1;
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
                        if (string.IsNullOrEmpty(cellValue)) continue;

                        // 메인 검색어로 먼저 필터링
                        string compareValue = caseSensitive ? cellValue : cellValue.ToLower();
                        if (!compareValue.Contains(compareKeyword)) continue;

                        string[] rowData = new string[] { no.ToString(), sheet.SheetName, (r + 1).ToString(), (c + 1).ToString(), cellValue };
                        _allRows.Add(rowData);
                        dataGridView1.Rows.Add(rowData);
                        no++;
                    }
                }
            }

            workbook.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            string searchType = cmbSearchType.SelectedItem.ToString();
            bool caseSensitive = Properties.Settings.Default.CaseSensitive;

            dataGridView1.Rows.Clear();

            int no = 1;
            foreach (var row in _allRows)
            {
                string sheetName = caseSensitive ? row[1] : row[1].ToLower();
                string cellValue = caseSensitive ? row[4] : row[4].ToLower();
                string kw = caseSensitive ? keyword : keyword.ToLower();

                bool match = string.IsNullOrEmpty(keyword);
                if (!match)
                {
                    if (searchType == "전체")
                        match = sheetName.Contains(kw) || cellValue.Contains(kw);
                    else if (searchType == "시트명")
                        match = sheetName.Contains(kw);
                    else if (searchType == "셀값")
                        match = cellValue.Contains(kw);
                }

                if (match)
                {
                    dataGridView1.Rows.Add(no, row[1], row[2], row[3], row[4]);
                    no++;
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearch.Text.Trim()) && cmbSearchType.SelectedItem.ToString() != "전체")
                btnSearch_Click(sender, e);

            var result = MessageBox.Show(
                        "추출 형식을 선택하세요.\n\n[예] txt\n[아니오] xlsx\n[취소] 취소",
                        "추출 형식 선택",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                ExportToTxt();
            else if (result == DialogResult.No)
                ExportToXlsx();
            // 취소면 그냥 아무것도 안함
        }

        private void ExportToTxt()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "텍스트 파일|*.txt";
            sfd.FileName = Path.GetFileNameWithoutExtension(_filePath) + "_상세";

            if (sfd.ShowDialog() != DialogResult.OK) return;

            string searchType = cmbSearchType.SelectedItem.ToString();

            using (StreamWriter sw = new StreamWriter(sfd.FileName, false, System.Text.Encoding.UTF8))
            {
                sw.WriteLine($"파일명: {Path.GetFileName(_filePath)}");
                sw.WriteLine($"검색어: {_keyword}");
                sw.WriteLine($"대소문자 구분: {(Properties.Settings.Default.CaseSensitive ? "구분함" : "구분안함")}");
                sw.WriteLine($"숨김시트 포함: {(Properties.Settings.Default.IncludeHiddenSheets ? "포함함" : "포함안함")}");

                if (searchType != "전체")
                {
                    sw.WriteLine($"추가 검색 대상: {searchType} | 추가 검색어: {txtSearch.Text.Trim()}");
                }

                sw.WriteLine($"추출일시: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                sw.WriteLine("----------------------------------------");
                sw.WriteLine("No\t시트명\t행\t열\t셀값");

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    string cellValue = row.Cells["Value"].Value?.ToString()?.Replace("\n", " ").Replace("\r", " ") ?? "";
                    sw.WriteLine($"{row.Cells["No"].Value}\t{row.Cells["SheetName"].Value}\t{row.Cells["Row"].Value}\t{row.Cells["Col"].Value}\t{cellValue}");
                }
            }

            MessageBox.Show("txt로 추출 완료!");
        }

        private void ExportToXlsx()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "엑셀 파일|*.xlsx";
            sfd.FileName = Path.GetFileNameWithoutExtension(_filePath) + "_상세";

            if (sfd.ShowDialog() != DialogResult.OK) return;

            string searchType = cmbSearchType.SelectedItem.ToString();

            IWorkbook wb = new XSSFWorkbook();
            ISheet sheet = wb.CreateSheet("상세결과");

            int rowIdx = 0;

            // 메타정보
            sheet.CreateRow(rowIdx++).CreateCell(0).SetCellValue($"파일명: {Path.GetFileName(_filePath)}");
            sheet.CreateRow(rowIdx++).CreateCell(0).SetCellValue($"검색어: {_keyword}");
            sheet.CreateRow(rowIdx++).CreateCell(0).SetCellValue($"대소문자 구분: {(Properties.Settings.Default.CaseSensitive ? "구분함" : "구분안함")}");
            sheet.CreateRow(rowIdx++).CreateCell(0).SetCellValue($"숨김시트 포함: {(Properties.Settings.Default.IncludeHiddenSheets ? "포함함" : "포함안함")}");

            if (searchType != "전체")
            {
                sheet.CreateRow(rowIdx++).CreateCell(0).SetCellValue($"추가 검색 대상: {searchType} | 추가 검색어: {txtSearch.Text.Trim()}");
            }

            sheet.CreateRow(rowIdx++).CreateCell(0).SetCellValue($"추출일시: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            sheet.CreateRow(rowIdx++); // 빈 줄

            // 헤더
            IRow header = sheet.CreateRow(rowIdx++);
            header.CreateCell(0).SetCellValue("No");
            header.CreateCell(1).SetCellValue("시트명");
            header.CreateCell(2).SetCellValue("행");
            header.CreateCell(3).SetCellValue("열");
            header.CreateCell(4).SetCellValue("셀값");

            // 데이터
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                if (row.Cells["No"].Value == null) continue;
                IRow r = sheet.CreateRow(rowIdx++);
                r.CreateCell(0).SetCellValue(row.Cells["No"].Value.ToString());
                r.CreateCell(1).SetCellValue(row.Cells["SheetName"].Value.ToString());
                r.CreateCell(2).SetCellValue(row.Cells["Row"].Value.ToString());
                r.CreateCell(3).SetCellValue(row.Cells["Col"].Value.ToString());
                r.CreateCell(4).SetCellValue(row.Cells["Value"].Value.ToString());
            }

            using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create))
                wb.Write(fs);

            MessageBox.Show("xlsx로 추출 완료!");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSearchType.SelectedItem.ToString() == "전체")
            {
                txtSearch.Text = "";
                txtSearch.Enabled = false;
            }
            else
            {
                txtSearch.Enabled = true;
            }
        }
    }
}

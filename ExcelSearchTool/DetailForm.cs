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
            this.Text = "상세보기";
            this.Icon = new Icon("icon.ico");
            _filePath = filePath;
            _keyword = keyword;
            lblFileName.Text = Path.GetFileName(filePath);
            lblSeachName.Text = keyword;
            lblSheetFileName.Text = Path.GetFileName(_filePath);
            lblSheetKeyword.Text = _keyword;

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
            LoadSheets();
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
                MessageBox.Show("파일을 불러올 수 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                        string cellValue = GetCellValue(cell);
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

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSearch_Click(sender, e);
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

        //------------------
        private void LoadSheets()
        {
            cmbSheet.Items.Clear();
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
            catch (IOException) { return; }

            bool includeHidden = Properties.Settings.Default.IncludeHiddenSheets;
            bool caseSensitive = Properties.Settings.Default.CaseSensitive;
            string compareKeyword = caseSensitive ? _keyword : _keyword.ToLower();

            for (int s = 0; s < workbook.NumberOfSheets; s++)
            {
                if (!includeHidden && (workbook.IsSheetHidden(s) || workbook.IsSheetVeryHidden(s)))
                    continue;

                ISheet sheet = workbook.GetSheetAt(s);
                bool hasMatch = false;

                for (int r = 0; r <= sheet.LastRowNum; r++)
                {
                    IRow row = sheet.GetRow(r);
                    if (row == null) continue;

                    for (int c = 0; c < row.LastCellNum; c++)
                    {
                        ICell cell = row.GetCell(c);
                        if (cell == null) continue;

                        string cellValue = caseSensitive ? GetCellValue(cell) : GetCellValue(cell).ToLower();
                        if (cellValue.Contains(compareKeyword))
                        {
                            hasMatch = true;
                            break;
                        }
                    }
                    if (hasMatch) break;
                }

                if (hasMatch)
                    cmbSheet.Items.Add(sheet.SheetName);
            }

            workbook.Close();

            if (cmbSheet.Items.Count > 0)
                cmbSheet.SelectedIndex = 0;
        }

        private void cmbSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            clbColumns.Items.Clear();
            cmbColumnFilter.Items.Clear();
            cmbColumnFilter.Items.Add("전체");
            cmbColumnFilter.SelectedIndex = 0;
            txtColumnFilter.Enabled = false;
            dgvSheet.Columns.Clear();
            dgvSheet.Rows.Clear();

            if (cmbSheet.SelectedItem == null) return;

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
            catch (IOException) { return; }

            ISheet sheet = workbook.GetSheet(cmbSheet.SelectedItem.ToString());

            // 시트 전체에서 값이 있는 열 인덱스 수집
            var usedCols = new HashSet<int>();
            for (int r = 0; r <= sheet.LastRowNum; r++)
            {
                IRow row = sheet.GetRow(r);
                if (row == null) continue;

                for (int c = 0; c < row.LastCellNum; c++)
                {
                    ICell cell = row.GetCell(c);
                    if (cell != null && !string.IsNullOrEmpty(GetCellValue(cell)))
                        usedCols.Add(c);
                }
            }

            // A열, B열... 형식으로 추가
            foreach (int colIdx in usedCols.OrderBy(c => c))
            {
                string colName = GetColumnName(colIdx);
                clbColumns.Items.Add(colName, false);
            }

            workbook.Close();

            chkAllColumns.CheckedChanged -= chkAllColumns_CheckedChanged;
            chkAllColumns.Checked = true;
            chkAllColumns.CheckedChanged += chkAllColumns_CheckedChanged;

            chkAllColumns_CheckedChanged(sender, e);
            btnSheetSearch_Click(sender, e);
        }

        // 열 인덱스를 A, B, C... AA, AB... 형식으로 변환
        private string GetColumnName(int colIdx)
        {
            string name = "";
            colIdx++;
            while (colIdx > 0)
            {
                colIdx--;
                name = (char)('A' + colIdx % 26) + name;
                colIdx /= 26;
            }
            return name + "열";
        }

        private void clbColumns_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                // 콤보박스 갱신
                cmbColumnFilter.Items.Clear();
                cmbColumnFilter.Items.Add("전체");
                foreach (string item in clbColumns.CheckedItems)
                    cmbColumnFilter.Items.Add(item);
                cmbColumnFilter.SelectedIndex = 0;
                txtColumnFilter.Enabled = false;

                // 전체선택 체크박스 동기화
                bool allChecked = true;
                for (int i = 0; i < clbColumns.Items.Count; i++)
                {
                    if (!clbColumns.GetItemChecked(i))
                    {
                        allChecked = false;
                        break;
                    }
                }
                chkAllColumns.CheckedChanged -= chkAllColumns_CheckedChanged;
                chkAllColumns.Checked = allChecked;
                chkAllColumns.CheckedChanged += chkAllColumns_CheckedChanged;
            });
        }

        private void chkAllColumns_CheckedChanged(object sender, EventArgs e)
        {
            clbColumns.ItemCheck -= clbColumns_ItemCheck;
            for (int i = 0; i < clbColumns.Items.Count; i++)
                clbColumns.SetItemChecked(i, chkAllColumns.Checked);
            clbColumns.ItemCheck += clbColumns_ItemCheck;

            cmbColumnFilter.Items.Clear();
            cmbColumnFilter.Items.Add("전체");
            if (chkAllColumns.Checked)
                foreach (string item in clbColumns.Items)
                    cmbColumnFilter.Items.Add(item);
            cmbColumnFilter.SelectedIndex = 0;
            txtColumnFilter.Enabled = false;
        }

        private void cmbColumnFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbColumnFilter.SelectedItem?.ToString() == "전체")
            {
                txtColumnFilter.Text = "";
                txtColumnFilter.Enabled = false;
            }
            else
            {
                txtColumnFilter.Enabled = true;
            }
        }

        private void btnSheetSearch_Click(object sender, EventArgs e)
        {
            if (cmbSheet.SelectedItem == null) return;

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
            catch (IOException) { return; }

            ISheet sheet = workbook.GetSheet(cmbSheet.SelectedItem.ToString());
            bool caseSensitive = Properties.Settings.Default.CaseSensitive;
            string compareKeyword = caseSensitive ? _keyword : _keyword.ToLower();

            var selectedCols = new Dictionary<int, string>();
            if (clbColumns.CheckedItems.Count == 0)
                return;

            foreach (string colName in clbColumns.CheckedItems)
            {
                int colIdx = ColNameToIndex(colName);
                selectedCols[colIdx] = colName;
            }

            // 그리드 초기화 부분에 행 번호 컬럼 먼저 추가
            dgvSheet.Columns.Clear();
            dgvSheet.Rows.Clear();
            dgvSheet.Columns.Add("RowNum", "");
            dgvSheet.Columns["RowNum"].Width = 30;
            dgvSheet.Columns["RowNum"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvSheet.Columns["RowNum"].DefaultCellStyle.BackColor = Color.LightGray;
            dgvSheet.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;

            foreach (var col in selectedCols.OrderBy(c => c.Key))
                dgvSheet.Columns.Add(col.Value, col.Value);

            // 필터 조건
            string filterCol = cmbColumnFilter.SelectedItem?.ToString();
            string filterKw = txtColumnFilter.Text.Trim();

            // 매칭된 행만 가져오기
            for (int r = 0; r <= sheet.LastRowNum; r++)
            {
                IRow row = sheet.GetRow(r);
                if (row == null) continue;

                // 메인 검색어 매칭 여부 확인
                bool hasKeyword = false;
                for (int c = 0; c < row.LastCellNum; c++)
                {
                    ICell cell = row.GetCell(c);
                    if (cell == null) continue;
                    string cellVal = caseSensitive ? GetCellValue(cell) : GetCellValue(cell).ToLower();
                    if (cellVal.Contains(compareKeyword))
                    {
                        hasKeyword = true;
                        break;
                    }
                }
                if (!hasKeyword) continue;

                // 열 검색 필터 적용
                var rowValues = new Dictionary<int, string>();
                foreach (var kvp in selectedCols)
                {
                    ICell cell = row.GetCell(kvp.Key);
                    rowValues[kvp.Key] = cell != null ? GetCellValue(cell) : "";
                }

                bool match = true;
                if (filterCol != "전체" && !string.IsNullOrEmpty(filterKw))
                {
                    int filterColIdx = ColNameToIndex(filterCol);
                    string cellVal = caseSensitive
                                     ? (rowValues.ContainsKey(filterColIdx) ? rowValues[filterColIdx] : "")
                                     : (rowValues.ContainsKey(filterColIdx) ? rowValues[filterColIdx] : "").ToLower();
                    string kw = caseSensitive ? filterKw : filterKw.ToLower();
                    match = cellVal.Contains(kw);
                }

                if (match)
                {
                    var values = new List<object> { r + 1 }; // 행 번호
                    values.AddRange(selectedCols.OrderBy(c => c.Key)
                        .Select(k => (object)(rowValues.ContainsKey(k.Key) ? rowValues[k.Key] : "")));
                    dgvSheet.Rows.Add(values.ToArray());
                }
            }

            workbook.Close();

            dgvSheet.ClearSelection();
        }

        // 열 이름을 인덱스로 변환 (A열 → 0, B열 → 1)
        private int ColNameToIndex(string colName)
        {
            colName = colName.Replace("열", "").Trim();
            int result = 0;
            foreach (char c in colName)
            {
                result = result * 26 + (c - 'A' + 1);
            }
            return result - 1;
        }

        private void btnSheetExport_Click(object sender, EventArgs e)
        {
            btnSheetSearch_Click(sender, e);
            if (dgvSheet.Rows.Count == 0)
            {
                MessageBox.Show("추출할 데이터가 없습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                "추출 형식을 선택하세요.\n\n[예] txt\n[아니오] xlsx\n[취소] 취소",
                "추출 형식 선택",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                ExportSheetToTxt();
            else if (result == DialogResult.No)
                ExportSheetToXlsx();
        }

        private void ExportSheetToTxt()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "텍스트 파일|*.txt";
            sfd.FileName = Path.GetFileNameWithoutExtension(_filePath) + "_시트검색";
            if (sfd.ShowDialog() != DialogResult.OK) return;

            using (StreamWriter sw = new StreamWriter(sfd.FileName, false, System.Text.Encoding.UTF8))
            {
                sw.WriteLine($"파일명: {Path.GetFileName(_filePath)}");
                sw.WriteLine($"검색어: {_keyword}");
                sw.WriteLine($"시트: {cmbSheet.SelectedItem}");
                if (cmbColumnFilter.SelectedItem?.ToString() != "전체" && !string.IsNullOrEmpty(txtColumnFilter.Text))
                {
                    sw.WriteLine($"컬럼 검색 대상: {cmbColumnFilter.SelectedItem} | 검색어: {txtColumnFilter.Text.Trim()}");
                }
                sw.WriteLine($"추출일시: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                sw.WriteLine("----------------------------------------");

                // 헤더
                sw.WriteLine(string.Join("\t", dgvSheet.Columns.Cast<DataGridViewColumn>().Select(c => c.HeaderText)));

                // 데이터
                foreach (DataGridViewRow row in dgvSheet.Rows)
                {
                    var values = dgvSheet.Columns.Cast<DataGridViewColumn>()
                        .Select(c => row.Cells[c.Name].Value?.ToString()?.Replace("\n", " ").Replace("\r", " ") ?? "");
                    sw.WriteLine(string.Join("\t", values));
                }
            }
            MessageBox.Show("txt로 추출 완료!");
        }

        private void ExportSheetToXlsx()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "엑셀 파일|*.xlsx";
            sfd.FileName = Path.GetFileNameWithoutExtension(_filePath) + "_시트검색";
            if (sfd.ShowDialog() != DialogResult.OK) return;

            IWorkbook wb = new XSSFWorkbook();
            ISheet sheet = wb.CreateSheet("시트검색결과");
            int rowIdx = 0;

            sheet.CreateRow(rowIdx++).CreateCell(0).SetCellValue($"파일명: {Path.GetFileName(_filePath)}");
            sheet.CreateRow(rowIdx++).CreateCell(0).SetCellValue($"검색어: {_keyword}");
            sheet.CreateRow(rowIdx++).CreateCell(0).SetCellValue($"시트: {cmbSheet.SelectedItem}");
            if (cmbColumnFilter.SelectedItem?.ToString() != "전체" && !string.IsNullOrEmpty(txtColumnFilter.Text))
                sheet.CreateRow(rowIdx++).CreateCell(0).SetCellValue($"컬럼 검색 대상: {cmbColumnFilter.SelectedItem} | 검색어: {txtColumnFilter.Text.Trim()}");
            sheet.CreateRow(rowIdx++).CreateCell(0).SetCellValue($"추출일시: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            sheet.CreateRow(rowIdx++);

            // 헤더
            IRow header = sheet.CreateRow(rowIdx++);
            for (int c = 0; c < dgvSheet.Columns.Count; c++)
                header.CreateCell(c).SetCellValue(dgvSheet.Columns[c].HeaderText);

            // 데이터
            foreach (DataGridViewRow row in dgvSheet.Rows)
            {
                IRow r = sheet.CreateRow(rowIdx++);
                for (int c = 0; c < dgvSheet.Columns.Count; c++)
                    r.CreateCell(c).SetCellValue(row.Cells[c].Value?.ToString() ?? "");
            }

            using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create))
                wb.Write(fs);

            MessageBox.Show("xlsx로 추출 완료!");
        }

        private void btnSheetClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtColumnFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSheetSearch_Click(sender, e);
        }

        private string GetCellValue(ICell cell)
        {
            if (cell == null) return "";

            switch (cell.CellType)
            {
                case CellType.Formula:
                    switch (cell.CachedFormulaResultType)
                    {
                        case CellType.String:
                            return cell.StringCellValue;
                        case CellType.Numeric:
                            return cell.NumericCellValue.ToString();
                        case CellType.Boolean:
                            return cell.BooleanCellValue.ToString();
                        default:
                            return cell.ToString();
                    }
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Numeric:
                    return cell.NumericCellValue.ToString();
                case CellType.Boolean:
                    return cell.BooleanCellValue.ToString();
                default:
                    return cell.ToString();
            }
        }

        private void clbColumns_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSheetSearch_Click(sender, e);
        }
    }
}

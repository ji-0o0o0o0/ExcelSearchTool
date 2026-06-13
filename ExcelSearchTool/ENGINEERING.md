# 🛠 Engineering Decisions & 트러블슈팅

## Engineering Decisions (핵심 고민과 선택)

### 1️⃣ 엑셀 라이브러리: EPPlus vs NPOI
처음에는 EPPlus로 시작했으나, 검색 대상 폴더에 `.xls`(2003 이전 구버전 바이너리 포맷) 파일이
섞여있을 가능성이 높았습니다 (공기업 등 오래된 문서 다수 보유).

EPPlus는 `.xlsx`만 지원하는 반면, NPOI는 `.xls`(HSSF)와 `.xlsx`(XSSF)를 모두 지원하고
별도 라이선스 제약이 없어 NPOI로 전환했습니다.

- 엑셀 포맷 무관하게 동일한 검색 로직 적용 가능
- 라이선스 비용/제약 없음

---

### 2️⃣ 검색 결과를 파일로 남길지, 화면에서만 보여줄지
초기에는 검색 결과(미포함 파일 목록 등)를 별도 결과 파일로 남기는 방식을 고려했습니다.

WinForms 자체 화면(DataGridView)에서 파일별 매칭개수를 바로 확인할 수 있으므로,
별도 결과 파일 없이 화면 표시 + 즉시 이동 처리로 단순화했습니다.

- 관리해야 할 부가 파일이 늘어나지 않음
- 검색 → 확인 → 이동까지 한 화면에서 처리

---

### 3️⃣ 설정 분리: Properties.Settings
기본 폴더, 이동 폴더, 대소문자 구분, 숨김 시트 포함 여부를
코드에 하드코딩하지 않고 `Properties.Settings`(사용자별 config)로 분리했습니다.

- 설정 변경 시 재빌드/재배포 불필요
- 검색/이동 버튼 클릭 시점마다 최신 설정값을 다시 읽으므로,
  설정 화면에서 값을 바꾸고 돌아오면 재실행 없이 즉시 반영됨

---

### 4️⃣ 폴더 구조: 검사 폴더와 이동 폴더 분리
검색어 포함 파일을 이동할 폴더를 기본 폴더의 하위 폴더(예: `Found/`)로 고정할지,
완전히 별도 경로로 지정 가능하게 할지 고민했습니다.

이동 폴더를 별도 경로로 자유롭게 지정 가능하게 하여,
검사 대상 폴더와 후속 처리(보고 등) 폴더를 분리해서 관리할 수 있도록 했습니다.

---

### 5️⃣ 이동 전 확인창
검색어 포함 파일을 곧바로 이동시키면 실수로 잘못 이동할 위험이 있었습니다.

이동 버튼 클릭 시 대상 파일 개수와 이동 폴더 경로를 보여주는 확인창(Yes/No)을 거치도록 하여,
사용자가 마지막으로 한 번 더 확인할 수 있게 했습니다.

```csharp
var result = MessageBox.Show(
    $"{targetFiles.Count}개의 파일을 다음 폴더로 이동하시겠습니까?\n\n{moveFolder}",
    "이동 확인",
    MessageBoxButtons.YesNo,
    MessageBoxIcon.Question);
```

---

## 🔧 트러블슈팅

### 검색 결과 개수가 엑셀 "모두 찾기"와 다른 문제
**문제**: 동일한 키워드로 검색했는데, 엑셀에서 직접 "모두 찾기"한 결과(53개)와
코드 검색 결과(111개)가 크게 차이남.

**원인 분석**: 매칭된 셀의 값을 직접 출력해서 비교하는 방식으로 원인을 추적.
```csharp
if (cellValue.Contains(keyword))
{
    matchCount++;
    System.Diagnostics.Debug.WriteLine($"Sheet:{sheet.SheetName} R{r}C{c} = {cellValue}");
}
```
출력 결과, `CellType`(Blank/String/Numeric/Formula)에 따라 `cell.ToString()`이
화면에 보이는 값과 다르게 나오는 셀들이 다수 포함되어 있었음.

**결과**: 디버그 출력으로 실제 매칭 셀을 하나씩 확인하여 원인이 되는 셀들을 특정,
검색 로직/대상 범위를 조정하여 엑셀 결과와 일치시킴.

---

### NPOI 디버거에서 `NotImplementedException` 발생
**문제**: 디버그 중 `workbook` 객체를 펼쳐볼 때 `IsReadOnly` 속성에서
`System.NotImplementedException` 표시.

**원인**: NPOI의 `HSSFWorkbook`이 일부 속성에 대해 디버거의 자동 평가(autos window)
요청에 응답하지 못해 발생하는 것으로, 실제 코드 흐름과는 무관함.

**결론**: 코드 실행 자체에는 영향 없음. `cellValue`, `CellType` 등 실제 사용하는
속성값을 직접 확인하며 디버깅 진행.

---

### 파일이 열려있을 때 검색 중단되는 문제
**문제**: 검사 대상 파일 중 하나가 엑셀에서 열려있는 상태면 `FileStream` 생성 시
`IOException` 발생, 전체 검색이 중단됨.

**해결**: `try-catch`로 `IOException`을 처리하여 해당 파일만 "사용 중"으로 표시하고,
나머지 파일은 정상적으로 검색이 계속되도록 변경.

```csharp
catch (IOException)
{
    MessageBox.Show($"{Path.GetFileName(filePath)} 파일이 열려있어 검사할 수 없습니다. 파일을 닫고 다시 시도해주세요.",
        "파일 사용 중", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    return -1;
}
```

---

### 설정 화면 라디오버튼이 저장된 값을 반영하지 않는 문제
**문제**: 설정에서 대소문자 구분/숨김 시트 옵션을 저장해도, 설정 창을 다시 열면
항상 디자이너 기본값("구분안함"/"포함안함")으로만 표시됨.

**원인**: `Properties.Settings`의 기본값은 프로그램 최초 실행 시의 초기값일 뿐,
설정 폼이 열릴 때 저장된 값을 화면에 반영하는 코드가 없었음.

**해결**: `SettingsForm_Load`에서 저장된 설정값을 읽어와 라디오버튼/라벨에 반영.
```csharp
private void SettingsForm_Load(object sender, EventArgs e)
{
    if (Properties.Settings.Default.CaseSensitive)
        rbCaseSensitive.Checked = true;
    else
        rbCaseInsensitive.Checked = true;

    if (Properties.Settings.Default.IncludeHiddenSheets)
        rbHiddenInclude.Checked = true;
    else
        rbHiddenExclude.Checked = true;
}
```

---

### 설정 초기화 시 폴더 경로가 초기화되지 않는 문제
**문제**: 설정 화면에서 "초기화"를 눌러도 메인 화면의 폴더 경로/파일 목록이 그대로 남아있음.

**원인**: `btnSettings_Click`에서 `ShowDialog()` 종료 후, `DefaultFolderPath`가
빈 문자열이 되면 `string.IsNullOrEmpty` 조건에 걸려 아무 처리도 하지 않고 넘어감.

**해결**: else 분기를 추가하여, 기본 폴더 경로가 없을 경우 메인 화면도 함께 초기화.
```csharp
else
{
    lblPath.Text = "(폴더주소)";
    toolTip1.SetToolTip(lblPath, "");
    checkedListBox1.Items.Clear();
    dataGridView1.Rows.Clear();
}
```

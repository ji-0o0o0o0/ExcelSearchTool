# 📝 CHANGELOG

## [v1.4.0]
### 추가
- 설정 화면 "초기화" 버튼 추가
  - 기본 폴더 경로 / 이동 폴더 경로 → "(지정안됨)"
  - 대소문자 구분 / 숨김 시트 포함 → 기본값(둘 다 안함)
- 메인 폼: 설정 초기화 시 폴더/파일 목록도 함께 초기화

### 수정
- `SettingsForm_Load` 추가: 설정 창 열 때 저장된 값을 라디오버튼/라벨에 반영
- 설정 폼 위치를 메인 폼 우측으로 고정 (겹침 방지)

---

## [v1.3.0]
### 추가
- 설정 화면(SettingsForm) 신설
  - 기본 폴더 경로 (폴더 선택 + 저장, ToolTip 적용)
  - 이동 폴더 경로 (폴더 선택 + 저장, 완전 별도 경로 지정 가능)
  - 대소문자 구분 / 숨김 시트 포함 여부 (라디오버튼, 기본값 둘 다 안함)
- `Properties.Settings`에 `DefaultFolderPath`, `MoveFolderPath`, `CaseSensitive`, `IncludeHiddenSheets` 추가

### 변경
- `Form1_Load`: DefaultFolderPath가 있으면 자동으로 폴더+파일목록 로드
- 폴더 로딩 로직을 `LoadFilesFromFolder` 메서드로 분리 (재사용)
- `btnSettings_Click`: 설정 창 닫힌 후 DefaultFolderPath 변경사항 메인 폼에 재반영

---

## [v1.2.0]
### 추가
- 이동 버튼(`btnMoveFound`) 클릭 시 확인창 추가
  - "N개의 파일을 [경로]로 이동하시겠습니까?" (Yes/No)
- 검색어 포함(매칭개수 > 0) 파일을 `Found` 폴더로 이동하는 기능
- 이동 완료 후 파일 목록 자동 갱신

---

## [v1.1.0]
### 추가
- 대소문자 구분 옵션 (`caseSensitive` 파라미터)
- 숨김 시트 포함 여부 옵션 (`workbook.IsSheetHidden` / `IsSheetVeryHidden`)
- 파일이 열려있을 때 IOException 처리 → 알림창 표시, 결과 "사용 중"으로 표시
- DataGridView 결과에 No(순번) 컬럼 추가

### 변경
- 엑셀 라이브러리 EPPlus → **NPOI**로 전환 (`.xls`/`.xlsx` 모두 지원)
- 매칭 셀 값을 `Debug.WriteLine`으로 출력하여 엑셀 "모두 찾기"와 결과 차이 원인 분석

### 고민했던 것들
- **엑셀 결과와 매칭 개수 불일치**: `CellType`(Blank/String/Numeric/Formula)에 따라
  `cell.ToString()` 결과가 달라질 수 있음을 확인, 디버그 출력으로 검증

---

## [v1.0.0]
### 추가
- 폴더 선택(FolderBrowserDialog) → `.xls`/`.xlsx` 파일 목록을 CheckedListBox에 표시
- 전체선택/해제 체크박스 (개별 해제 시 전체선택 자동 해제, `CheckOnClick=True`)
- 경로가 길 경우 Label에 AutoEllipsis + ToolTip으로 전체 경로 표시
- 키워드 부분일치 검색, 결과를 DataGridView(파일명/매칭개수)에 표시

### 고민했던 것들
- **엑셀 라이브러리 선택**: EPPlus(.xlsx만 지원) vs NPOI(.xls/.xlsx 모두 지원)
  → 오래된 `.xls` 파일 대응을 위해 NPOI 채택
- **검색 결과 보관 방식**: 결과 파일 저장 vs 화면 표시
  → 화면(DataGridView)에서 바로 확인 가능하므로 결과 파일은 만들지 않음

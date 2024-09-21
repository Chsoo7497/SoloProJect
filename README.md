Subproject(유니티)
==========

유니티에서 FireBase 연동
------------------------
연관 스크립트 : Manager/UserDataManager.cs
- Firebase Auth를 이용하여 이메일 로그인 구현
- Firebase Auth에서 제공하는 UID를 이용하여 Firebase Database에 유저 데이터저장

UI관리 시스템을 구현
------------------------------
연관 스크립트 : Manager/UIManager.cs, UI/UIDesc.cs, Scenes/UIController.cs
 - prefab을 이용하여 UI를 구현하고 기본 UI를 세팅하여 씬 시작 시 기본UI가 생성되도록 구현
 - 모든 UI가 UIDesc를 소지하고 Layer값을 가져 레이어 순으로 정렬되도록 구현

Excel을 이용한 DataTable 구현
------------------------------
연관 스크립트 : CSVToJsonConverter, Table/Base/DataTableBase, Table/..
 - 엑셀 파일을 CSV로 저장하고 에디터 커스터마이징으로 csv파일을 Json으로 컨버트 할수있도록 구현
 - json파일로 저장된 DataTable을 읽고 List형태로 저장하여 사용하기 용이하도록 구현

유니티 에서 팝업 시스템을 구현
------------------------------
연관 스크립트 : Manager/PopupManager.cs, UI/Poopup/PopupDesc.cs, UI/Poopup/Content/..
 - prefab을 이용하여 팝업 외곽과 내부 컨텐츠 파트를 나눠 스크립트로 팝업을 생성 가능하도록 구현
 - 외부에서 팝업을 열때 DataTable을 통해 컨텐츠를 선택하고 팝업 생성
 - 생성 이후 PopupObject와 ContentObject, OnClick이벤트를 받아 외부에서 설정 가능하고록 구현
 - 예시 UI/LogInComponent.CS

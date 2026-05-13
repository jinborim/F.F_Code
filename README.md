플레이 영상 https://www.youtube.com/watch?v=SZ4O9bAiLio

F.F 폴더 https://github.com/jinborim/F.F

시스템 구조
1. Inventroy System
- 아이템 획득 및 슬롯 관리
- 소비 아이템 / 장비 아이템 분리 처리
- 드래그 앤 드롭으로 슬롯 이동
- 숫자키로 무기 선택
특징
 - 일반 아이템은 하단 슬롯
 - 무기는 상단 슬롯으로 분리
 - 동일 아이템 자동 스택 처리
 - 드래그 시 DragSlot UI 사용

2. Gun System
  - 무기 장착 시 bulletPrefab 변경
  - 슬롯 선택 시 활성 무기 변경
  - 드래그로 무기 슬롯 교체
흐름
  - 슬롯에서 무기 선택
  - bulletTest에 gun 전달
  - bulletPrefab 교체
  - A키 입력 시 총알 발사

3. Dialogue System
  - 타이핑 효과 대화 출력
  - 선택지 UI 분기 처리
  - 씬 이동 선택 가능
흐름
  - 플레이어가 오브젝트 접근
  - Door_Test가 대화 실행
  - Dialog_Test가 타이핑 출력
  - 선택 시 씬 이동 또는 종료

4. HP System
  - 하트 단위 체력 시스템
  - 데미지 분산 처리
  - 회복 시 다음 하트로 전이
특징
  - 100 단위로 Heart 하나 구성
  - 데미지 -> 오른쪽부터 감소
  - 회복-> 왼쪽부터 증가
  - 0이면 사망 처리

5. Monster System
  - 이동 AI
  - 플레이어 충돌 시 데미지
  - 체력 감소 및 사망 처리
흐름
  - 이동 -> Rigidbody 기반 이동
  - 충돌 -> HP_Manager 호출
  - 체력0 -> Destroy

6. Stage /Scene System
기능
  - 씬 이동 시 시작 위치 설정
  - DontDestroyOnLoad로 데이터 유지
  - UI 초기화 처리

7. Input / Weapon Select System
기능
  - 숫자키로 무기 선택
  - 슬롯 활성화 표시
  - 선택 무기에 따라 bullet 변경

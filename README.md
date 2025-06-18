# DragonSlayer

![1](https://github.com/user-attachments/assets/c449f8a1-5ebc-4f29-bd8b-b58aa37ae844)

## 🎮 1. 게임 개요
이 프로젝트는 내일배움캠프에서 진행한 과제로, Unity 기반의 2D 플랫포머 액션 게임입니다. 플레이어는 적과 보스를 물리치고 다양한 맵을 탐험하며, 아이템 수집 및 장착하며 성장합니다. 

### 개발 기간
- 2025.6.12 ~ 2025.06.18

### 팀 구성 및 역할 분담
- 팀장 : 박정현(Item, Inventory, UI)
- 팀원 : 박도현(Map, Camera, Save), 김기현(Boss), 김성주(Enemy, Intro), 진주호(Player)

### 주요 특징
- **전투:** 상태머신 기반으로 플레이어, 적, 보스가 동작
![image](https://github.com/user-attachments/assets/a7f2f118-6ef7-42ab-849b-236a2bceebcf)
- **보스:** 다양한 스킬로 구성된 패턴
![image](https://github.com/user-attachments/assets/62d57003-d5e4-4fad-bfea-9b6b9c97f53e)
- **스테이지:** 여러 스테이지, 적 스폰, 상호작용 오브젝트(상자, 저장포인트 등)


![3](https://github.com/user-attachments/assets/a0c7ad0d-0f4f-491b-9fe4-f55b79a63a0b)
![5](https://github.com/user-attachments/assets/3fa2bd50-8689-4c3b-9fb4-3a89b0b17d7d)
![6](https://github.com/user-attachments/assets/5ae7b751-02d1-4994-ab40-c2da2e2af770)
![4](https://github.com/user-attachments/assets/44ee58c9-6c3c-4918-83ef-0e65e8f498f9)



- **아이템/인벤토리 시스템:** 무기, 방어구, 소모품 등 다양한 아이템과 장착/퀵슬롯/사용
![image](https://github.com/user-attachments/assets/bbb36038-88ae-40ef-9353-e090649ba3f2)

- **세이브/로드:** Json 기반 플레이어 데이터 저장/불러오기

## ⚙ 2. 유니티 버전
Unity 2022.3.17f1

## 🚀 3. 실행 방법
Unity Hub에서 프로젝트를 Unity 2022.3.17f1로 엽니다.
Assets/01_Scenes/01_Intro1.unity 씬을 로드합니다.
Play 버튼을 눌러 게임을 시작합니다.

### 조작법
- **플레이어 이동:** 방향키
- **점프:** C
- **공격:** X
- **상호작용:** E
- **구르기:** Z
- **스킬:** 스페이스바
- **아이템 스왑:** V
- **아이템 사용:** F
- 인벤토리에서 마우스로 아이템 이동


## 🏗️ 4. 프로젝트 구조
```
📁 에셋 구조
Assets/
├── 01_Scenes/         # 게임 씬
├── 02_Scripts/        # C# 스크립트
├── 03_Prefabs/        # 프리팹 오브젝트
├── 04_Images/         # 이미지 리소스
├── 05_Animations/     # 애니메이션 클립/컨트롤러
├── 06_Externals/      # 외부 에셋(사운드, 이펙트, 맵 등)
├── 07_Material/       # 머티리얼/피직스 머티리얼
├── 08_Fonts/          # 폰트 리소스
├── Editor/            # 에디터 확장
├── Plugins/           # 외부 플러그인(DOTween 등)
└── Resources/         # 런타임 리소스(아이템, UI 등)
```

📁 Scripts 폴더 구성
```
Assets/02_Scripts/
├── Common/         # 공통 유틸리티, 게임 매니저
├── Player/         # 플레이어 컨트롤, 스탯, 스킬
├── Enemy/          # 적 AI, 상태머신
├── Boss/           # 보스 AI, 상태머신, 공격 패턴
├── Map/            # 맵, 상호작용 오브젝트(상자, 저장 등)
├── Item/           # 아이템 데이터, 데이터베이스, 에디터
├── UI/             # UI 매니저, 인벤토리, 팝업, HUD
├── Save/           # 세이브 시스템
├── Load/           # 로드 시스템
├── SoundManager/   # 사운드 관리
├── Interface/      # 인터페이스 정의
└── Camera/         # 카메라 제어
```
## 🔧 5. 주요 시스템 및 기능
### 1) 매니저 시스템
`UIManager`
- Fixed UI, Window UI, Popup UI 등 UI 전체 관리 및 오브젝트 풀링

`SaveManager` / `LoadManger`
- 플레이어 데이터(스탯, 인벤토리, 위치, 진행도) 저장/불러오기

`SoundManager`
- 배경음악과 효과음 등 게임 내 사운드 일괄 관리


### 2) 전투/AI 시스템
`Player`, `Enemy`, `BossEnemy`
- 각 캐릭터의 기본 동작 및 전투 로직 담당

`PlayerStat`
- 플레이어의 스탯(체력, 공격력, 이동속도 등) 관리

`EnemySO`
- 적의 스탯, AI 속성, 드롭 아이템 등 데이터 관리 (ScriptableObject 기반)

`BossStateMachine`, `EnemyStateMachine`
- 상태머신 기반으로 Idle, Move, Attack, Skill, Die 등 다양한 상태를 관리
- 각 상태별로 독립적인 행동 및 전이 로직 구현


### 3) UI 시스템
- Fixed UI: 체력, 경험치, 스킬 쿨타임 등 HUD
- Window UI: 인벤토리, 장비, 옵션 등 모달 창
- Popup UI: 사망, 알림 등 일시적 팝업 창 관리
- 오브젝트 풀링: UI 재사용 최적화

### 4) 아이템/인벤토리 시스템
- `ItemData`, `ItemDataBase`: ScriptableObject 기반 아이템 데이터 관리
- `Inventory`: 아이템 획득, 장착, 퀵슬롯, 사용, UI 연동
- `ItemEditor`: 커스텀 에디터로 아이템 생성/관리

- ### 5) 맵/상호작용 시스템
- `Map`: 적 스폰, 트리거, 맵 이동
- `SavePoint`, `Chest`, `Trap`: 저장, 보물상자, 트랩 등 상호작용 오브젝트

## 🛠️ 6. 기술 스택
- **엔진**: Unity
- **언어**: C#
- **패턴**: Singleton, State Machine, Object Pool
- **UI**: Unity UI System (Canvas, UGUI)
- **카메라**: Cinemachine

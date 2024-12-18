# open-source-project-bgm

![대문](https://github.com/user-attachments/assets/7dffd24f-abf1-4ad1-b3f2-dbb6c1c0949a)




## Contributor
<table>
  <tbody>
    <tr>
      <td align="center" valign="top" width="25%">
        <a href="https://github.com/Jiho12315">
          <img src="https://avatars.githubusercontent.com/u/144876236?v=4" width="100px;" alt="KDHen"/><br />
          <b style="font-size: 16px;">JiHo Bae</b>
        </a>
        <br />
        <a href="https://github.com/MinBeom2/open-source-project-bgm/commits/main/?author=Jiho12315" title="커밋 내역 보기">커밋 내역 보기</a>
      </td>
      <td align="center" valign="top" width="25%">
        <a href="https://github.com/MinBeom2">
          <img src="https://avatars.githubusercontent.com/u/154870745?s=96&v=4" width="100px;" alt="MINBEOM2"/><br />
          <b style="font-size: 16px;">MinBeom Kim</b>
        </a>
        <br />
        <a href="https://github.com/MinBeom2/open-source-project-bgm/commits/main/?author=MinBeom2" title="커밋 내역 보기">커밋 내역 보기</a>
      </td>
      <td align="center" valign="top" width="25%">
        <a href="https://github.com/GreenAppleSoda">
          <img src="https://avatars.githubusercontent.com/u/151068526?v=4" width="100px;" alt="CHTuna"/><br />
          <b style="font-size: 16px;">SeungMin Kim</b>
        </a>
        <br />
        <a href="https://github.com/MinBeom2/open-source-project-bgm/commits/main/?author=GreenAppleSoda" title="커밋 내역 보기">커밋 내역 보기</a>
      </td>
      <td align="center" valign="top" width="25%">
        <a href="https://github.com/wipheg">
          <img src="https://avatars.githubusercontent.com/u/63744049?v=4" width="100px;" alt="Hyung-Junn"/><br />
          <b style="font-size: 16px;">SangWon Park</b>
        </a>
        <br />
        <a href="https://github.com/MinBeom2/open-source-project-bgm/commits/main/?author=wipheg" title="커밋 내역 보기">커밋 내역 보기</a>
      </td>
    </tr>
  </tbody>
</table>


# Patch Notes

## System
### 2024-11-5
- 회원가입, 로그인 구현 

### 2024-11-7
- 회원가입 씬 구현

### 2024-11-9
- 계정 싱글톤으로 변경

### 2024-11-11
- Firebase Realtime Database를 이용하여 클라우드 세이브 및 불러오기 구현

### 2024-11-24
- 계정 싱글톤 삭제
- 이전에 로그인했던 기기에서는 로그인 없이 자동 로그인이 가능하도록 구현

### 2024-11-25
- 플레이어 위치 데이터 저장 구현

### 2024-11-27
- 저장된 위치로 불러오는 기능 구현
- 세이브 씬에서 게임으로 돌아올 때, 세이브 씬을 호출한 위치에서 시작하도록 구현
- 간헐적으로 플레이어 위치가 제대로 불러와지지 않는 문제 수정

### 2024-11-28
- 게임 중 ESC 키를 눌러 게임을 멈추는 기능 구현

### 2024-11-30
- 세이브 슬롯에 스테이지 정보 표시 기능 구현

### 2024-12-1
- 로그인, 회원가입 씬 UI개선 및 오류 메세지 출력 기능 구현

### 2024-12-2
- 메인, 불러오기, 저장 메뉴 UI개선
- 저장 씬을 패널로 변경

### 2024-12-3
- 로그인 씬에 게임 종료 버튼 추가

### 2024-12-4
- 사운드 옵션 패널 추가
   
### 2024-12-5
- 메인 메뉴 이미지 변경
- 게임 오버 기능 추가

### 2024-12-7
- 사운드 조절 옵션 추가

### 2024-12-8
- 스테이지 통합

### 2024-12-10
- 엔딩 추가, 게임 이름(WeirdSpace)변경
- stage1 마우스 커서 보이는 버그 해결
- 
### 2024-12-11
- 엔딩 씬 마우스 커서 보이게 수정


### 2024-12-18
- 회원가입 씬 뒤로가기 버튼 버그 해결
  
---

## Endless Passage
### 2024.11.20
- 초기 커밋 : 레벨 디자인, 오브젝트 배치, 문 여닫기, 상자 여닫기, 열쇠기능 구현

### 2024.11.22
- 1차 패치 : 클리어 지점 맵 수정, 계단 스케일 수정, 오브젝트 애니메이션 추가

### 2024.11.28
- 2차 패치 : 걸음 속도에 따른 발소리 출력, AI proto type 추가

### 2024.12.08
- 3차 패치 : 오브젝트 사운드 출력, AI 사운드 출력 및 난이도 조절, 게임 오버 조건 추가, 클리어 지점 추가

### 2024.12.08
- 4차 패치 : 클리어 지점 패널추가, 클리어 조건 달성시 이동, 소리 비활성화

### 2024.12.10
- 5차 패치 : 열쇠 소유 유무 변수를 두 가지 스크립트에서 접근하는 것 수정 

---

## Playground
### 2024.11.20
- 초기 커밋 : 맵, 오브젝트 배치 및 디자인

### 2024.11.28
- 애니메이션과 다이얼로그 삽입

### 2024.12.02
- 다이얼로그 구현 및 기능 추가 완료
- Part1 개발 시작 (시작부터 눈알 찾기 퍼즐)

### 2024.12.03
- Part1 개발 완료
- Part2 개발 시작 (눈알 퍼즐 부터 심장 찾기 퍼즐)
- 이펙트 및 사운드 삽입완료

### 2024.12.05
- Part2 개발 완료
- Part3 개발 시작(심장 퍼즐부터 마지막 추격)
- 인형 AI 추격 기능 추가

## Abruptive Attack
### 
- 

---

## Aisle
### 2024-11-25
- 스테이지 간 전환을 위한 스테이지 구현
- 다음 스테이지로의 이동 및 세이브 상호작용 기능 구현
  
### 2024-11-27
- 밝기 조절
  

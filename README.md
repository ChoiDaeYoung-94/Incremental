# About

unity 3d incremental game project

### Trailer

https://user-images.githubusercontent.com/66999597/215687283-d704df78-6818-46a8-8b24-f8daa84d277e.mp4

## Getting Started

1. Clone
2. [Download Assets](https://drive.google.com/file/d/1VuCwsd5O7US-Bosk__K-TOJ5crAo-YVt/view?usp=sharing)
3. Open Project in Unity

## Requirements
- Unity2020.3.25f1 LTS

## SDK, Package ...
- [PlayFabEditorExtensions, PlayFabSDK](https://docs.microsoft.com/ko-kr/gaming/playfab/sdks/unity3d/installing-unity3d-sdk)
- [play games plugin](https://github.com/playgameservices/play-games-plugin-for-unity/releases)

## Download

- [Google Play](https://play.google.com/store/apps/details?id=com.AeDeong.Incremental)
- Clone the repository locally:
~~~
git clone https://github.com/ChoiDaeYoung-94/Incremental.git
~~~

## Build

| platform  | output   |
| --------- | -------- |
| AOS       | apk, aab |
| iOS       |   TODO   |

build 추출물은 Project root/Build/AOS, Project root/Build/iOS 에 위치한다.

### Unity Scenario

시작 전 게임 프로젝트의 root 경로에 Build 폴더를 만든 뒤 진행한다.

- apk
  - Unity Menu - Build - AOS - APK
- aab
  - Unity Menu - Build - AOS - AAB

### CLI Scenario

https://github.com/ChoiDaeYoung-94/unity-cicd 레포의 build.py를 사용하여 빌드한다.

build.py를 통해 build 시 aab, apk 모두 빌드된다.

terminal > python build.py > 매개변수 입력 > build

### Github Action Scenario
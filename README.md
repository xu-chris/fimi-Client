# fimi Client
![Build Unity Project](https://github.com/xu-chris/fimi-Client/workflows/Build%20Unity%20Project/badge.svg)

**Check out the [documentation of fimi](https://xu-chris.github.io/fimi-Documentation/) for full information about the whole system.**

fimi is an application which tries to resemble a coach while you doing workout by analyzing your posture and giving you feedback about what you should watch more often. To run it, you need a **webcam**, a big screen and your smartphone (and some room space). You control the big screen with your smartphone by simply scanning the displayed QR code (being in the same wifi net is necessary).

fimi generates a 3d pose estimation based on the webcam image and checks every frame your posture based on a fixed set of rules per exercise. In the end, you receive a small summary of your results on your smartphone.

The application also compares your posture with your previous trainings while still being designed with privacy in mind. All your information is stored only temporarily while running the application on your PC or Mac, whereas the training data is stored on your smartphone directly. This also means that you could theoretically log in at your friend's house and have your profile with you.

This is the client software of the fimi fitness mirror application.

![Demo running on my machine](docs/demo.gif)

## Credits
Special thanks to Dushyant Mehta for providing [WoodenMan](https://gvv.mpi-inf.mpg.de/projects/XNect/) as the basis of this client. Although nothing of the code is in this project, It helped me to understand the core idea how XNECT and other pose estimation systems are able to getting connected to other apps like Unity.

## Features
- Perform your training with a web cam and see in real time when you might hurt your body by performing it wrong.
- Choose from multiple trainings by using the webapp as a controller (see [fimi Controller](https://github.com/xu-chris/fimi-Controller) for details).
- Compare training results: user profiles will be stored locally on the smartphone and will be transferred temporarily for stat comparison. You see what you have performed better in comparison to the last time.
- Adjust and add multiple new exercises by adding them to the YAML file only.
- Add more trainings by adjusting the YAML file only.
- Interchangeable pose estimation server: use XNECT or any other that performs similar. Locally or in the cloud.

## Used technology
- [Unity3d](https://unity.com)
- [IBM Watson Text-To-Speech](https://www.ibm.com/cloud/watson-text-to-speech)

## Install / Build it
1. Download [Unity3d 2020.2.1f1](https://unity3d.com/unity/whats-new/2020.2.1) ([Open in Unity Hub](unityhub://2020.2.1f1/270dd8c3da1c))
2. Open the repository folder with Unity Hub
3. Open `Start` scene and hit Play

### Add IBM Watson information
This project has used the IBM Watson technology for creating Text-To-Speech output. It is quite confusing to set it up on IBM cloud, so I hope this guide is at least helping you find your path through the jungle of IBM's confusing service management system.

To get this part running as well, do the following:
1. If not already done, create an [IBM Cloud](https://cloud.ibm.com/registration?target=/developer/watson&cm_sp=WatsonPlatform-WatsonServices-_-OnPageNavLink-IBMWatson_SDKs-_-Unity) account.
2. After account confirmation, you will land on the IBM Cloud Dashboard. Since IBM thinks every service of them is a resource, you need to look for `Add Resources`.
3. After clicking, you find the `IBM Cloud Products` page. Enter `Text To Speech` in the search bar and select it if it pops up as suggestion.
4. Select the `Lite` plan which is for free and click on the right hand side on `Create`.
5. After that you should get an `API Key` and a `URL`. You will need both of them, so keep the page open.
6. Go To `Assets/Scripts/General/Authentication` and rename the `SecretsExample.yaml` to `Secrets.yaml` (to prevent it to be uploaded to your Git fork in the future)
7. Add the `API Key` to `IamApiKey: ` and the `URL` to `ServiceUrl: ` in the YAML file.
8. Save and try to build the project. Look in the logs. Usually it says that it successfully created a connection to IBM cloud servers after some seconds.

## Start it
Clone or download this repository and open it with `Unity v2020.2.1f1`. Alternatively, grab one of the binaries in the [Releases](https://github.com/xu-chris/fimi-Client/releases) section of this repository.

You can use the [fimi Mock Server](https://xu-chris.github.io/fimi-Mock-Server/) to mock the usually needed [fimi Server](https://xu-chris.github.io/fimi-Server/) for the pose estimation data.

## Adjust it

### Change configurations
The client application comes with two distinctive paths of changing either the application or the content configuration:

1. For the application configuration, check out the 
  - `Scenes` for scene based configuration (like the name of the next scene, the duration of the T-Pose detection and so on)
  - `Prefabs` for adjusting the settings per prefab. For example you can check out the `WebSocketClient` prefab to change the IP and port to the WebSocketServer (make sure you have adjusted the port as well on the server part)

2. For the content, like texts or the rules checked, check out the `Asset/Content` folder where you will find two YAML files:
  - `Exercises.yaml`: Keeps all the different exercises with the specified rule sets
  - `Trainings.yaml`: Keeps the different trainings and their respective exercises mappings, including the duration of each exercise

Although you can add more exercises, keep in mind that the animations need to be made interchangeable which is not done currently.

### Change pose estimation server
If you want to use a different pose estimation server, acknowledge the following requirements for the server:
- The pose estimation server detects 21 joints
- All of these joints have 3D vector data
- The joint information are transferred in raw data, separated with comma (`,`). No space in between by using the WebSocket protocol
- The joints are transferred in the following order (specified in `Assets/Scripts/General/Skeleton/JointToIndex.cs`):
  - `SPINE1_RX` (index: `0`)
  - `SPINE2_RX` (index: `1`)
  - `SPINE3_RX` (index: `2`)
  - `NECK1_RX` (index: `3`)
  - `HEAD_EE_RY` (index: `4`)
  - `LEFT_SHOULDER_RX` (index: `5`)
  - `LEFT_ELBOW_RX` (index: `6`)
  - `LEFT_HAND_RX` (index: `7`)
  - `LEFT_HAND_EE_RX` (index: `8`)
  - `LEFT_HIP_RX` (index: `9`)
  - `LEFT_KNEE_RX` (index: `10`)
  - `LEFT_ANKLE_RX` (index: `11`)
  - `LEFT_FOOT_EE` (index: `12`)
  - `RIGHT_SHOULDER_RX` (index: `13`)
  - `RIGHT_ELBOW_RX` (index: `14`)
  - `RIGHT_HAND_RX` (index: `15`)
  - `RIGHT_HAND_EE_RX` (index: `16`)
  - `RIGHT_HIP_RX` (index: `17`)
  - `RIGHT_KNEE_RX` (index: `18`)
  - `RIGHT_ANKLE_RX` (index: `19`)
  - `RIGHT_FOOT_EE` (index: `20`)
- If multiple people are detected, the joint information of each person is just right behind the last one in one row

## Known limitations and issues
- **Only one person at a time**: The system is not able to identiy different person and might map the results of you wrongly. Use it only with one person at a time.
- **Only Squat-Arm-Raise**: The set of exercises is currently limited to Squat-Arm-Raise. If you wish to exthend it with more than just that, add it in the YAML and in the code.
- **Multiple trainer animation logic missing**: The animation of the trainer avatar is only concerning the squat-arm-raise for now. To add a new exercise, you need to add some logic which animation will be chosen for the trainer avatar.
- **Depending on [fimi server](https://github.com/xu-chris/fimi-Server)**: This is not a standalone application. It needs fimi server and many other dependencies first installed on the same machine and running (or a distanced one) to run successfully.
- **Web server for controller unstable**: Sometimes the site won't load. It's safer to restart the application then.
- Opening the controller by using `localhost` address does not work.
- **IP / domain limitation of profile**: currently, the profile is only transferred as long as the IP address / domain of the client machine remains the same. This can be resolved by storing the results on a central domain based `LocalStorage` which gets loaded and stored whenever the client application loads and stores the user data from the client app.

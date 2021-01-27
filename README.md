# fimi Client
![Build Unity Project](https://github.com/creichel/fimi-Client/workflows/Build%20Unity%20Project/badge.svg)

Check out the [documentation of fimi](https://creichel.github.io/FiMi-Fitness-Smart-Mirror/) for full information about the whole system.

fimi is an application which tries to resemble a coach while you doing workout by analyzing your posture and giving you feedback about what you should watch more often. To run it, you need a **webcam**, a big screen and your smartphone (and some room space). You control the big screen with your smartphone by simply scanning the displayed QR code (being in the same wifi net is necessary).

fimi generates a 3d pose estimation based on the webcam image and checks every frame your posture based on a fixed set of rules per exercise. In the end, you receive a small summary of your results on your smartphone.

The application also compares your posture with your previous trainings while still being designed with privacy in mind. All your information is stored only temporarily while running the application on your PC or Mac, whereas the training data is stored on your smartphone directly. This also means that you could theoretically log in at your friend's house and have your profile with you.

This is the client software of the fimi fitness mirror application.

![Demo running on my machine](docs/demo.gif)

## Credits

This project is based on
- [XNECT by Max Planck Institute](https://gvv.mpi-inf.mpg.de/projects/XNect/)
- [Unity3d](https://unity.com)
- [Quasar](https://quasar.dev)
- [IBM Watson Text-To-Speech](https://www.ibm.com/cloud/watson-text-to-speech)
- [WoodenMan by Dushyant Mehta](https://gvv.mpi-inf.mpg.de/projects/XNect/)

## Get it running
Clone or download this repository and open it with `Unity v2020.2.1f1`. Alternatively, grab one of the binaries in the [Releases](https://github.com/creichel/fimi-Client/releases) section of this repository.

## Known limitations and issues
- **Only one person at a time**: The system is not able to identiy different person and might map the results of you wrongly. Use it only with one person at a time.
- **Only Squat-Arm-Raise**: The set of exercises is currently limited to Squat-Arm-Raise. If you wish to exthend it with more than just that, add it in the YAML and in the code.
- **Multiple trainer animation logic missing**: The animation of the trainer avatar is only concerning the squat-arm-raise for now. To add a new exercise, you need to add some logic which animation will be chosen for the trainer avatar.
- **Depending on [fimi server](https://github.com/creichel/fimi-Server)**: This is not a standalone application. It needs fimi server and many other dependencies first installed on the same machine and running (or a distanced one) to run successfully.
- **Web server for controller unstable**: Sometimes the site won't load. It's safer to restart the application then.
- Opening the controller by using `localhost` address does not work.
- **IP / domain limitation of profile**: currently, the profile is only transferred as long as the IP address / domain of the client machine remains the same. This can be resolved by storing the results on a central domain based `LocalStorage` which gets loaded and stored whenever the client application loads and stores the user data from the client app.
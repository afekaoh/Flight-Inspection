# Flight-Inspection

## About the project
the project can be divided into 5 different sections

### Charts
1. In the charts we use the library of LiveCharts -> https://lvcharts.net/.
2. If anomalies has been detected the points on the chart will be bigger and blue.
3. hovering above the anomaly point will open a small window with the details of the anomaly.
4. clicking on the anomaly point will move the video to the point in time.

### Video Panel
1. The video panel works like most of the generic video controllers you can play, pause, change where you are in the video and control the speed of the video.
2. The video panel is responsible to update all the other components about the current time and what to play

### Joystick
1. In the data window you can see a view of a Joystick and other controls that synching with the video in real time

### Other Data
1. We displayed some data about the flight, such as: height, speed, direction and the yaw-roll-pitch.
2. For the display of the speed and the direction, we used LiceChars as well.
For both of them, we used an angular chart - for the speed in the range of 0 - 250 and for the campus in the range of 0 - 359. 

### Anomaly Detection
1. In the app the user have the option to upload a DLL (the app will work even if he didn't load one)
2. In order to load external DLLs for anomaly detection we implemented a our own DLL and a class that import him
3. the DLL handles all the communication with the user DLL, all the memory allocation and de-allocation, and defines the API that connects between the app and the user DLL
4. If the user wish to add a DLL he will need to make sure that he implements two interfaces (the interfaces located in the extras folder)

### Miscellaneous
1. The UML, Video and all  is in the project.

## How to operate
1. If you run the project through Visual Studio make sure to run it in CPU mode of x64 so that the DLL would work
2. Before you start the project make sure that you have Flight Gear installed on your computer and that you added the XML you want to use to it.
3. After the app has loaded go to settings and upload your normal flight csv(or the test one), your XML and the path to the Flight Gear executable (usually located in C:\Program Files\FlightGear 2020.3.6\bin).
4. After each load there should be V marked next to the Button.
5. the program is ready to operate now, you can view the csv that you upload.
6. if you wish also to use an anomaly detection you can upload another csv to test and a DLL and the app will play the test one.
7. Notice that if you didn't uploaded a test csv, the Normal csv will play, and after that only the test csv will play. Also to make sure everything works correctly, you can't upload the test csv and dll path before you upload the other components.
8. After you upload all the files, click save and move to the Flight Gear tab and press "open FG".Now the Flight Gear program should open. 
9. Wait until it's loaded and then press "start simulation". It will open a new window with all the controls and data.
10. If you wish to switch the csv or dll, you can simply close the window (without closing the Flight Gear) and go to the settings page, upload the new files, and press start simulation again.
11. When you want to close the app, simply close the data window, and after that the main window and the Flight Gear program will close automatically.


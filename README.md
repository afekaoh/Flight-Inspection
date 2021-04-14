# Flight-Inspection

the project can be divided to 5 different sections

## Charts
1. In the charts we use the library of LiveCharts -> https://lvcharts.net/.
2. If anomalies has been detected the points on the chart will be bigger and blue.
3. hovering above the anomaly point will open a small window with the details of the anomaly.
4. clicking on the anomaly point will move the video to the point in time.

## Video Panel
1. The video panel works like most of the generic video controllers you can play, pause, change where you are in the video and control the speed of the video.
2. The video panel is responsible to update all the other components about the current time and what to play

## Joystick
1. In the data window you can see a view of a Joystick and other controls that synching with the video in real time

## Other Data
// todo

## Anomaly Detection
1. In the app the user have the option to upload a DLL (the app will work even if he didn't load one)
2. In order to load external DLLs for anomaly detection we implemented a our own DLL and a class that import him 
3. the DLL handles all the communication with the user DLL, all the memory allocation and de-allocation, and defines the API that connects between the app and the user DLL
4. If the user wish to add a DLL he will need to make sure that he implements two interfaces (the interfaces located in the extras folder)
  
## extra
7. In order to run this program in the VS you need to use CPU Windows X64 and not any CPU.
8. The UML is in the project.

# Bitalino_EMG_Processing
WPF GUI - EMG Signal Visualization 

Goal:
This application was implemented to measure single finger movements, process them, and run 
a small melody game by moving the fingers. 

Description:
For this project the EMG singals of each finger was measured by using EMG electrodes and 
placing them around the forearm. Each electrode was connected to one EMG sensor. The five 
EMG sensors were connected to a Bitalino Board Kit which receives the signals and processes them 
into data. The data is send via frames to program which is written in C#. Through a native Bitalino library 
the interface between Visual Studio is granted. Each frame gets identifies as a finger class. Data gets clustered 
and a threshold gets calculated. 

Requirements: 
IDE: Visual Studio
Language: C#
Hardware: Bitalino Board Kit
Sensor: 5x EMG sensor
Measurement: EMG electrodes



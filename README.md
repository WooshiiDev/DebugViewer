# Debug Viewer
[![Unity 2018.3+](https://img.shields.io/badge/unity-2018.4%2B-blue.svg)](https://unity3d.com/get-unity/download) [![License: MIT](https://img.shields.io/badge/License-MIT-brightgreen.svg)](https://github.com/WooshiiDev/HierarchyDecorator/blob/master/LICENSE)
### Downloads
[UnityPackage](https://github.com/WooshiiDev/DebugViewer/raw/main/Assets/DebugViewer.unitypackage) | [Zip Download](https://github.com/WooshiiDev/DebugViewer/archive/main.zip)

### Summary
Debug Viewer is an extremely lightweight built into the unity game window to remove overuse of the console and to allow for easier visualization of variables directly.

This is achieved by catergorizing all data shown, with a toggle to show and hide, keeping it clean, easy and simple.

![](https://i.imgur.com/mLFl0JA.gif)

### Usage
There are 3 main classes to use.

`DebugViewer.cs` is the MonoBehaviour that is required to run the debug. This is also the class that contains methods to add debug information.

`DebugCatergory.cs` is the class to store all catergory information, including draw position and the list of information.

`DebugInformation.cs` is the class for fields to use to store and hold the values for target variables.

To use DebugViewer, after downloading and installing into your project, there will be two ways to add in Debug Viewers functionality.
*Note: Using Attributes is much easier and faster, but the methods have been kept in for convenience and accessibility*

**Methods**

You can use methods directly from `DebugViewer.cs` to add required information:
```cs
//public static void AddInformationToCatergory(string catergory, DebugInformation information, bool addCatergory = false)
DebugViewer.AddInformationToCatergory ("Catergory Name", new DebugInformation ("Display Name", this, "variableName"), true);
```

**Attributes**

Attributes are straight forward, by adding the attribute directly to the variable:
```cs
[Debug ("Player", "Max Health")] public int maxHealth = 100;
[Debug ("Player", "Current Health")] public int currentHealth = 100;
```

### Support
Even though this is a small project, things like this and other repositories I work on and upload on here do take time, to not just implement, but to also design and sometimes make efficient. If you would like to support me, you can do so below:

[![PayPal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://paypal.me/Wooshii?locale.x=en_GB)
[![ko-fi](https://www.ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/L3L026UOE)

Development will be continued with this and will forever stay public and free.

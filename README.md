# Update-Checker

ChobbyCode Update Checker is a free open source tool designed to make checking for updates easier. The application is run on a DLL. The DLL can be shipped with the application. Unlike old DLL's this DLL cannot be broken as c# DLL's are shipped with the application so legacy features can be kept :)

### Features
- ✅ - Checking for updates
- ✅ - Data caching to reduce API calls
- ❌ - Automatically download updates or atleast get the download link
- ❌ - Access private repos using auth key

If you want to contribute fork the repository then create a push request and I will review your code and add or decline it.

## Usage

### Dependencies

Update-Checker requires NetwonSoft as a NuGet Package, if you do not have already download the NuGet package, learn how to add it [here](**docs/CONTRIBUTING.md**).

### Installation

To install Update-Checker simpily navigate to the 'Releases' and download the latest DLL. You can store the DLL anywhere on your computer but I recommend storing it in a folder which you will not forget about. You can also store the DLL in your application. This doesn't really need to be done as the DLL is copied when the application is build. So it can be just stored in a folder on the Desktop.

Next we need to add the Update-Checker to the application, 

This can be done by navigating to the Project in the solution explorer > Add > Project Reference

![Screenshot 2023-09-01 113603](https://github.com/ChobbyCode/Update-Checker/assets/100038952/9516d9b7-381c-43cf-b5ec-3e48620b6f3e)

In project references go down to Browse, if you have added the DLL before just make sure it is checked and click if okay. If you haven't added the DLL before then go to the bottom right click Browse. Then navigate to the location where you stored the DLL file. Make sure the DLL file is checked and click okay.

### Setup

Update-Checker requires its own function to run all the operations in. You can have as many of these functions as you want but they cannot be called the same thing for clear reasons.

At the top of you class file include the Update-Checker.

```c#
using Update_Checker;
```

Create a new method which can run the Update Checker methods. In the method create a instance of the UpdateChecker class. This method cannot return any information and must be ran async from the main program. Change the method's name to something more appropriate for what the class is doing. 
```c#
public static async void doStuff()
{
    // Create a instance of the UpdateChecker class
    UpdateChecker checker = new UpdateChecker();
}
```

## Checking for updates

In the UpdateChecker function we can call the different functions.

First we need to setup the repo which UpdateChecker will be accessing. Change the OWNERNAME to your github username, then set REPONAME to the name of repo you are trying to get the current version of

```c#
checker.OWNERNAME = "ChobbyCode";
checker.REPONAME = "Update-Checker";
```

We can quickly get to see if there is a update by using the method CheckForUpdates and passing in the current version. The CheckForUpdates method returns a bool which returns true if there is an update and false if there is not

> **IMPORTANT:** When calling a UpdateChecker method make sure it is awaited. i.e "await checker.LatestUpdateTag".

```c#
// Checks for updates
string currentVersion = "v0.1.0"
bool isUpdate = await checker.CheckForUpdates(currentVersion); // Checks for update with current version and stores in variable

if(isUpdate){
    // Do update stuff here
}
```

If you want to reduce the amount of lines you can do as below. Both of the code snippets do the same thing.

```c#
if(await checker.CheckForUpdates("v0.1.0")){
    // Do update stuff here
}
```

# Docs

## Simple Demo

```c#
public static async void checkForUpdate()
{
    UpdateChecker checker = new UpdateChecker();

    checker.OWNERNAME = "ChobbyCode";
    checker.REPONAME = "Update-Checker";

    string currentVersion = "v0.1.0";
    string latestVersion = await checker.LatestVersionTag();

    if(await checker.CheckForUpdates(currentVersion))
    {
        Console.WriteLine("There is an update available. Update " + latestVersion + " is available.");
    }
}
```
Sees if there is a new update available. If there is a new update available it prints there is a new update available to the console and the latest version.


## GetLatestVersionTag

```c#
checker.LatestVersionTag();
```

Returns the current version tag in string. No parameters.

## CheckForUpdates

```c#
checker.CheckForUpdates(currentVersion);
```

Returns true if there is a update and false if there is not a update. Takes string currentVersion (i.e v0.1.0, v1.6.5)

## GetVersionPart

```c#
checker.GetVersionPart(part);
```

Returns a part of the latest version (i.e. if latest version was v1.2.3 and input was 0 it would return 1, if it was 1 it would retunr 2, so on), takes int part from 0 - 2.


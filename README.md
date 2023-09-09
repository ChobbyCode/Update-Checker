# Update-Checker

ChobbyCode Update Checker is a free open source tool designed to make checking for updates on github easier. UC has features such as getting the latest version, seeing if a update is available and more. UC has native Chill support. However, even though Chill is my own software, it does get flagged by AntiViruses for been dangerous which may deter anyone from using YOUR software.

> #### I usually abbreviate the app name to just UC, so don't get confused.

### Why use this
- ✅ - Makes code tidier
- ✅ - Simple & Easy
- ✅ - 4 Click Install

  
### Features
- ✅ - Checking for updates
- ✅ - Data caching to reduce API calls
- ✅ - Automatic Downloads (via Chill)

### Planned Features
- ❌ - Access private repos using auth key
- ❌ - Log Files
- ❌ - Multiple repo support
- ❌ - Add support for not github stuff :)

If you want to contribute fork the repository then create a push request and I will review your code and add or decline it.

## Installation

## NuGet

To install with NuGet, create a new c# project and click on your solution or project, then click "Manage NuGet Packages", under Browse make sure "Show Pre-releases" is checked and then last of all search for Update_Checker and add the one ChobbyCode.

## Usage

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
        Console.WriteLine($"There is an update available. Update {latestVersion} is available.");
    }
}
```
Sees if there is a new update available. If there is a new update available it prints there is a new update available to the console and the latest version.

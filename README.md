# Update-Checker

## Usage

At the top of your file create a instance of the Update_Checker DLL file. Make sure you have the DLL file installed.
```c#
using Update_Checker;
```

Then create a new function dedicated to the updateChecker. You can have multiple functions this is called from too.

```c#
public async static void updateChecker()
{
    // Creates a instance of Update Checker
    UpdateChecker checker = new UpdateChecker();
    // This returns the latest version's tag & stores it
    string info = await checker.LatestVersionTag();
}
```

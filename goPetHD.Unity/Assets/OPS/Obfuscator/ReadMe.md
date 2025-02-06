**NOTE: Check out the online docs for the latest version plus images: https://docs.guardingpearsoftware.com/manual/Obfuscator/Description.html**

# How To Start

This guide will walk you through the process of installing, configuring, and utilizing the Obfuscator to strengthen your game's code. You will be able to make your code significantly more resistant to reverse engineering, replication, and unauthorized modification.

## Step 1 - Get & Install Obfuscator

The very first step is to get either [Obfuscator Free](https://assetstore.unity.com/packages/slug/89420), [Obfuscator Pro](https://assetstore.unity.com/packages/slug/89589) or [Obfuscator Source](https://assetstore.unity.com/packages/slug/210262) from the Unity Asset Store. Then download it into your current project.

The main difference between these 3 versions are that the Free version does not support MonoBehavior class and, in general, namespace obfuscation. The security functions are also not available. The Pro version contains all functions. While the source version additionally contains the source code of the obfuscator itself.

The Obfuscator project structure 'Assets/OPS/Obfuscator' will look like the following.

You find the following directories and files in the root directory of Obfuscator:
- **Editor:** Contains all resources and sources for the editor.
- **Logs:** The default location for log files.
- **Plugins:** Contains platform specific plugins.
- **Settings:** Stores the Obfuscator setting files.
- **License.pdf:** The license applying to the usage of the Obfuscator.
- **ReadMe.md:** A readme file that contains the same content as this page, but locally.
- **VersionHistory.md:** This file contains a detailed record of all the changes made in each version of the asset.

## Step 2 - Integration

GuardingPearSoftware's Obfuscator is designed for seamless integration, requiring no complex setup – simply plug it in and it works! Activated, it automatically obfuscates your code during every build, safeguarding your game without adding any extra burden to your workflow.

**Build in Editor.** You can simply build our game in the editor and the obfuscator will automatically run within the build pipeline to protect your game.

**Build with CI/CD.** Custom build scripts or CI/CD are also supported.

```cs
using UnityEditor;
using System.Diagnostics;

public class ScriptBatch 
{
    [MenuItem("MyTools/Windows Build With Postprocess")]
    public static void BuildGame ()
    {
        // Get filename.
        string path = EditorUtility.SaveFolderPanel("Choose Location of Built Game", "", "");
        string[] levels = new string[] {"Assets/Scene1.unity", "Assets/Scene2.unity"};

        // Build player.
        BuildPipeline.BuildPlayer(levels, path + "/BuiltGame.exe", BuildTarget.StandaloneWindows, BuildOptions.None);

        // Copy a file from the project folder to the build folder, alongside the built game.
        FileUtil.CopyFileOrDirectory("Assets/Templates/Readme.txt", path + "Readme.txt");

        // Run the game (Process class from System.Diagnostics).
        Process proc = new Process();
        proc.StartInfo.FileName = path + "/BuiltGame.exe";
        proc.Start();
    }
}
```

To run automatically inside the Unity build pipeline, the Obfuscator uses multiple hooks. These are the following:
- **IPreprocessBuildWithReport:** A interface used to receive a callback before the Player build is started. During this phase, the Obfuscator performs an analysis of the assets.
- **IFilterBuildAssemblies:** A interface used to receive a callback to filter assemblies away from the build. During this phase, the Obfuscator removes the reference to itself in release builds.
- **IPostBuildPlayerScriptDLLs:** A interface used to receive a callback just after the player scripts have been compiled. During this phase, the Obfuscator performs the actual obfuscation.
- **IUnityLinkerProcessor:** A interface used to receive a callback related to the running of UnityLinker. During this phase, the Obfuscator ensures the compatibility to IL2CPP.
- **IPostprocessBuildWithReport:** A interface used to receive a callback after the build is complete. During this phase, the Obfuscator obfuscates assets that got not obfuscated in prior.

The hooks can be found in the *BuildPostProcessor.cs* file at *Assets/OPS/Obfuscator/Editor*. If you use custom hooks or use assets that use pipeline hooks, have a look at the *callbackOrder* and make sure the Obfuscator runs last.

```cs
public int callbackOrder
{
    get { return int.MaxValue; }
}
```

## Step 3 - Setup Settings

Obfuscator has a centralized configuration which applies to the obfuscation pipeline. You can find it at *OPS -> Obfuscator -> Settings*.

The obfuscation settings separate into 4 topics:
- **Obfuscation:** General settings for code obfuscation. This includes specifications for which assemblies are to be obfuscated, as well as detailed instructions for the obfuscation of their respective classes and members.
- **Security:** Contains advanced security settings, like 'String Obfuscation', 'Random Code Generation', etc.
- **Compatibility:** Certain third-party assets require specific configurations to ensure compatibility with obfuscation, owing to the unique way Unity operates.
- **Optional:** Contains optional integration settings. For example 'Custom Obfuscation Pattern', 'Logging', 'Name Mapping', etc.

> [!NOTE]
> In most instances, the default settings provide the best optimization for the majority of games and applications.
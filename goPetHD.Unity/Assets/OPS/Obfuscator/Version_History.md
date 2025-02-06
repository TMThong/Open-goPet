# Version History:

## 5.5.0: Enums & Structs - Update
Features:
- [Feature-2025-2] Obfuscation of enum and struct class fields can now be customized to meet specific needs. The field settings have been extended to include dedicated settings for fields in enums and structs.
- [Feature-2025-1] Obfuscation of enum and struct classes can now be customized to meet specific needs. The class settings have been extended to include dedicated settings for enums and structs.

Improvements:
- [Imprv-2025-1] The Obfuscate Anyway attribute now includes support for structs. Additionally, it allows for random obfuscation names when the passed name field is left empty or null.

Bug Fixes:
- [Bug-2025-1] Fixed an issue where internal classes were only obfuscated when obfuscation of private classes was enabled, but not when obfuscation of internal classes was enabled. This has been corrected and internal classes are now obfuscated correctly. Notably, nested internal classes were correctly recognized and handled during this process.

## 5.4.1: Internal - Update
- Improvement: Some internal optimizations to make the Obuscator more robust.

## 5.4.0: Mapping - Update
- Feature: The obfuscation mapping can now be loaded from a web endpoint (Get) and saved at a web endpoint (Post).
- Improvement: If an obfuscation mapping could not be saved in the location specified by the user, it is saved in the obfuscator temp directory 'Assets\OPS\Obfuscator\Temp' [thanks David].

## 5.3.2: data.unity3d - Hotfix 2
- Fix: Error while obfuscating data.unity3d file '.../data.unity3d': Value cannot be null. Parameter name: _Assembly [thanks David].
- Compatibility: Most compatibility components can now be manually activated or deactivated. Default is activated.

## 5.3.1: data.unity3d - Hotfix
- Fix: Error while obfuscating data.unity3d file : Unable to read beyond the end of the stream [thanks Josh].

## 5.3.0: iOS - Update
- Improvement: Unity animation compatibility - You can now customize where animation callbacks should be automatically searched (models, animation clips, animator controllers).
- Improvement: Unity event compatibility - You can now customize where event callbacks should be automatically searched (scenes, prefabs).
- Fix: iOS build - MonoBehaviours were not obfuscated correctly in Unity 2022+ iOS builds. Resulting in MonoBehaviours failed to be resolved while runtime.

## 5.2.0: Addressable - Update
As this is a major update, it is recommended to remove the current OPS directory, make a backup copy of the Obfuscator settings file and download the new version.
- Feature: Lz4 / Lz4HC - The Obfuscator now supports obfuscation of builds that use compression with Lz4 and Lz4HC. This does not affect your workflow, but only improves the obfuscation of MonoBehaviours, which previously required a workaround.
- Feature: Addressables - A popular Unity asset to address assets from different locations. Obfuscator now supports obfuscation of addressable bundles using the JSON catalog. To activate obfuscation of Addressables activate it under the 'Compatibility' tab [thanks Dmitriy].
- Improvement: Memory usage - Obfuscator estimates the compatibility of assets for obfuscation. This included model or texture files. These were not disposed of correctly after loading, leading to potential memory issues.
- Fix: XCode - Obfuscator failed when creating XCode projects. This has been fixed [thanks Misha].

## 5.1.7: Burst - Update
- Fix: Obfuscated strings compiled by 'Burst' throw an exception that they cannot call managed code in a native environment. Therefore, strings in 'BurstCompile' jobs are now skipped from obfuscation. They are automatically safe due to their native environment [thanks Curtis].
- Fix: There was a rare case in which a type could not be resolved correctly. This caused an exception when building the Unity project. This is now fixed and even if a type still cannot be resolved, a warning is now displayed and the type is skipped from the obfuscation [thanks Cameron].

## 5.1.6: MongoDB Realm - Update
- Compatibility: Made the Obfuscator compatible with 'MongoDB Realm' [thanks Hector].
  -> Classed that inherite from Realms.IAsymmetricObject, Realms.IEmbeddedObject, Realms.IRealmObject and read/store data in the storage can be skipped from obfuscation to preserve the data integrity.
- Improvement: The random code generation no longer will create random code in classes that are skipped from obfuscation. This often lead to unexpected problems, as these classes were skipped by the user for a specific reason.

## 5.1.5:
- Fix: When disabling obfuscating for serializeable classes, serializeable fields still got obfuscated [thanks Eric].
- Compatibility: Fixed a compatibility issue with Zenject [thanks Dmitriy].

## 5.1.4:
- Fix: The obfuscation pipline did not check correctly for a development build. So "NotObfuscatedCauseAttribute" were not added to development builds.
- Fix: When obfuscating "older" .Net-Framework assemblies that had native pdb symbols, those symbols could not be rewritten, causing a failed build. The new default for these are portable pdf symbols (which makes even more sense for unity).
- Compatibility: Fixed a compatibility issue with StateMachineBehaviours when obfuscating ScriptableObjects.

## 5.1.3:
- Compatibility: .NetStandard - Obfuscator 2021 and 2022 now use .NetStandard 2.0 and 2023 and onwards use .NetStandard 2.1.
- QoL: The log writer now checks for invalid file locations.

## 5.1.2:
- Fix: Bee compiler could not resolve 'OPS.Obfuscator.Editor'.

## 5.1.1:
- Fix: Compatibility with Unity 2023.1 and onwards by switching the obfuscator build to .NetStandard 2.1.
- Add: Support of Meta SDKs - Facebook and Oculus
- Add: Support of Microsoft Playfab.

## 5.1.0:
- Add: OnRectTransformDimensionsChange will now be skipped from obfuscation for MonoBehaviours.
- Update: The UI / GUI compatibility addon was changed to an Unity-Event addon. Mostly UI / GUI or similiar methods are based on UnityEvent.
  When activated (by default it is activated), all methods attached to these events will be skipped.

## 5.0.5:
- Fix: Compatibility fix for the Universal Render Pipeline (URP).

## 5.0.4:
- Fix: Json serialization for public fields - Class requires Serializable Attribute.
- Fix: Dependency fix at release build.

## 5.0.3:
- Fix: OPS.Obfuscator.dll assembly was loaded as to obfuscate assembly. Is now a helper assembly.

## 5.0.2:
- Fix: Reference bug to OPS.Obfuscator.dll.

## 5.0.1:
- Improved Gui/Animation method recognition.
- Obfuscator UI improvement.
- OPS.Obfuscator.dll is now an AssemblyDefinitionFile.
- Fix: Removed OPS.Obfuscator.dll from release builds.

## 5.0

== Obfuscator 5.0 Introduction ==
The Obfuscator 5.0 version optimizes the MonoBehaviour protection and brings the obfuscation through
new features on a new level. By introducing ControlFlow Obfuscation, method bodies will be scrambled
and transformed into a nearly human unreadable form. An also new feature is the AntiTampering Obfuscation.
This feature builds a system of micro-checks to prevent modification and keeps hacker away.

----------------------------------------

4.0.6:
- Fix: Building Addressable Assets will no longer trigger the obfuscator and a so failed build.
- Add: You can now enter a custom log directory, instead of a file path only. In this directory, the log will be seperated by platform.

4.0.5:
- Fix: ArgumentException: ... Type .. has no scope?

4.0.4:
- Fix: On Analyse Scenes / Components - MissingMethodException.
- Fix: Enum wont be obfuscated if 'Obfuscate Serializable Classes' is deactivated.

4.0.3
- Fix: While processing pre build pipeline: System.MissingMethodException

4.0.2
- Fix: InvalidCastException while analysing assemblies.
- Fix: Yaml failed deserialization while analysing assets.

4.0.1
- Add: EventTrigger inspector method will now be recognized too.

4.0

== Obfuscator 4.0 Introduction ==

The Obfuscator 4.0 version is a bottom up new implemented Obfuscator, to increase the performance, security and
compatibility to Unity 2018+ versions. For you the process keeps the same. The Obfuscator still automatically applies
the obfuscation at build time. The Setting and Error-Stack-Track windows got a face lifting to be more intuitive and easier
to use. The settings and rename-mapping are now stored in a Json serialized file. This means for you,
because of the modification and the extension of the settings, you have to readjust the settings in the settings window.
It is not necessary to change the rename-mapping files. They will still be normally interpreted. But new rename-mappings
will be stored in the new Json format.

Most important obfuscation changes:
- You have more options to decide which assemblies you want to obfuscate.
- The string obfuscation is now way more advanced. (Pro only)
- The obfuscation of MonoBehaviour/ScriptableObject/Playable subclasses is way more advanced too. (Pro only)
- Also inspector set values / methods are now even better recognized.

----------------------------------------

3.9.10
- Add: Support of System.Reflection.ObfuscationAttribute 

3.9.9
- Fix: Obfuscation of Indexer Properties with custom names.
- Add: Support of AppodealAds.

3.9.8
- Fix: Could not resolve abc/xyz (Enums in Nested Classes)
- Fix: Obfuscation of Indexer Properties
- New: Added 'OPS.Obfuscator.Attribute.ObfuscateAnyway', allows to obfuscate class members even if they should be skipped.

3.9.7
- Fix: "Could not copy Temp/xyz to Library/xyz"
- Fix: Button background in Settings Gui.

3.9.6
- Update: Compatibility Update for next Obfuscator Version 4.0
- Fix: Fix for Playable classes

3.9.5
- Fix: Obfuscation of UWP/Windows Store applications may fail if some dependency is missing.
- Fix: A NullReferenceException might occur when obfuscating multiple assemblies.
- Update: You can now manually activate/deactivate the obfuscation of 'Assembly-CSharp.dll'
and 'Assembly-CSharp-firstpass.dll'. (Do not forget to activate the obfuscation of those
through the belonging settings, in the Obfuscator Settings General Tab, after updating!!)
- Update: Third party assembly Mono.Cecil got an update.
- Pro: Removed: Removed the 'OPS.ObfuscatorAssets.dll' assembly located in the 
'OPS\Obfuscator.Pro\Editor\Plugins' directory, it is now part of the 'OPS.Obfuscator.Editor.dll' 
assembly. (Depending on the used Unity Editor, you have to remove the assembly 
'OPS.ObfuscatorAssets.dll' manually after updating!!)

3.9.4
- Pro: Fix: Unity obfuscation had a small artifact causing build errors, fixed that.
- Fix: NullPointer Exception in the Assembly analysation phase. (sorry for that!)
- New: Unity Editor before version 5.6 are now theoretically supported (but not tested yet!)
- Change: Public Field and Public Method Obfuscation are now part of the free version again.

3.9.3
- New: In the obfuscator settings (general tab) you can now add additional assembly references, if the obfuscator has a problem resolving those.

3.9.2
- Fix: Whole obfuscation breaks if one assembly was not found.
- Pro: Fix: Unity class obfuscation fixes.
- New: Unity Linux Editor support.

3.9.1
- Fix: Assembly resolving fix.

3.9:
- Pro: Fix: Unity class (monobehaviour/scriptableobject) name obfuscation is active again for unity builds later than 2018.2!
Not in the same way as before, but still very effective!
- Fix: Virtual/Abstract Properties got not obfuscated correctly.
- Pro: Improvement: String obfuscation.
- New: Suppress Debug through Visual Studio.

3.8
- Pro: Fix: Test AssemblyDefinition Files got obfuscated too, causing an error.
- Fix: Properties did get obfuscated correctly (virtual/abstract properties still have the bug, but a fix is on the way.)
- Add: Support for unity 2019.2 beta and 2019.3 alpha.
- Add: You can now obfuscate serializeable field.
- Add: Notification inside development builds, showing you why something got not obfuscated.
- Pro: Currently deprecated: Unreadability for decompilers is no longer working, the trick got worked around by decompilers. (But working on a new way.)

3.7
- Change: Renamed OPS.Obfuscator assembly to OPS.Obfuscator.Editor
- Add: Added a assembly called OPS.Obfuscator containing the obfuscator attributes.
Fixes that you could not use Obfuscator Attributes inside AssemblyDefinition Files.

3.6.1
- New: Now you can log in a custom file. Go to the Obfuscator Window->Advanced->Logging.

3.6
- Fix + Improvement: Various minor fixes and improvement to increase the obfuscation performance.
- Fix: WSA/UWP obfuscation build error.
- Change: Obfuscator Windows are now located at OPS->Obfuscator->... instead of Window->...

3.5.5
- Fix: Obfuscation sometimes wont run on first build.
- Fix: Obfuscation of generic nested classes in generic classes.
- Pro: Improvement: Improvement of String obfuscation/encryption.

3.5.4
- Pro: Fix: Old reference to OPS.RSA.

3.5.3
- Pro: Fix: Obfuscator_UnityObject_RenamingTable.obf still exists.
- Pro: Change: String encryption is now a symmetric encryption instead of a asymmetric encryption. The power needy asymmetric decryption could cause hickups.

3.5.2
- Fix: Seperated obfuscation of enum fields/values from obfuscation of class fields.

3.5.1
- Adjustment: Removed usage of custom fonts. May correlate with a editor font bug while using NGUI.

3.5
- Fix: Property and Event Obfuscation might cause unknown Type in mscorlib errors.
- Add: Better controlability of obfuscation for string based invokes. Added a setting at Advanced -> Reflection and Coroutines,
  to activate/deactivate obfuscation of members matching string. (Activate if you use for example Type.GetField([Name]) or StartCoroutine([Name]))
- Add: Obfuscation setting of internal members.
- Change: The obfuscation of internal members is now seperated from the private members.
- Update: Updated Mono.Cecil to 0.10.3.

3.4.1
- Update: Updated Mono.Cecil to 0.10 to fix the out of memory bug.

3.4
- Fix: Obfuscation for Reflection and Unity Coroutines
- Fix: Obfuscation of Internal members.
- Change: Private setting controls now the obfuscation of private and internal members.
- Change: Public field and method obfuscation is now only available in Obfuscator Pro.

3.3
- Bug fix: Mac OS X support
- Bug fix: Automatically finding of Gui/Animation/... methods
- Change: Logging - The logs filename depends now one the buildtarget and not the date anymore
- Pro: Change: You can now add additional assemblies by it full path

3.2
- Bug fix: Parameter renaming
- Bug fix: Namespace and unity classes renaming
- Bug fix: Nested classes renaming
- Bug fix: Attribute renaming

3.1
- New: StackTrace unobfuscator: You can find it at Unity->Window->Obfuscator StackTrace.
- Bug fix: Obfuscation of Class/Method using RuntimeInitializeOnLoadMethodAttribute
- Some adjustments for property and event obfuscation
- Various small bugfixes

3.0
Obfuscator got reimplemented to improve the obfuscation process
to optimize your security. Because of this, there is a plenty
amount of new feature included:

- Pro: New: Assembly Definition File obfuscation
- Pro: New: External Assembly obfuscation
- New: Obfuscate Serializeable classes/fields
- New: Save/Load renaming mapping
- Many Bugfixes and improvements
- Logs are now stored inside the Obfuscator folder.
- Attributes have moved to OPS.Obfuscator.Attribute
- Pro: The Obfuscator Code is still unprotected inside the .dll. But moved to a assembly because of the massive amount of new code.

INFO: Please remove your old Obfuscator installation(but you can keep your settings file)!

2.9
- Next to the build game, there will be a file called: MyGame_ObfuscatorRenaming.txt 
containing the obfuscated name and the real name. Useful for reading the stack trace of build games.
- Improvement: Finding gui / unityevent methods
- Bug fix: Namespace collidation of 'System.IO.Path' for some users
- Bug fix: Namespaces in the ignore list shared the same value with the do not rename attribute list
- Pro: Bug fix: String obfuscation and namespace vice versa ignoring
- Pro: Improvement: String obfuscation speed and process of encryption
2.81
- First fixing of MonoBehaviour class name obfuscation in Unity 2018.2
- Obfuscator performance update
- Improved intercompatibility with AssetProtection
- Beta: Unity Methods Obfuscation ( like Awake, Start, Update, ... )

INFO: Important to know: Still not all MonoBehaviour class names get obfuscated in Unity 2018.2.x. But research is in progress.

2.8
- Some Gui adjustments
- Beta: Unity Methods Obfuscation
2.7.1
- RSA Encryption Upgrade

INFO: Unity 2018.2.x seems to have a bug with Obfuscation of MonoBehaviour class names. Please use a prior verion until this got fixed.

2.7
- Some Gui adjustments
- Some adjustments with IL2CPP builds
- Pro: Adjustments with the 'make assembly unreadable' feature
2.6
- Compatibility with PlayMaker
- Some adjustments for PS4 and XboxOne build
- Some logging adjustments
- Pro only: Some adjustments for jenkins builds
2.55
- Resolving Fix for abstract classes.
- Compatibility with Anti Cheat
2.54
- Fix for an error while loading assets.
- Fix for a bug happening while resolving class hierarchies.
- Pro only: Fix for the string obfuscation causing: 'Cannot perform dot operator' or 'expected ;'
2.53
Hotfix for UWP
- WinRt assmembly gets now resolved too.
- Fixes Bug: Could not resolve Nested Type XYZ.
- Some adjustments with obfuscation of generic classes.
2.52
Hotfix for IL2CPP and UWP
- Fixed obfuscation error of nested generic methods in generic classes.
- Streamwriter fix for UWP
- Some IL2CPP adjustments
2.51
Hotfix for IL2CPP
- Fixed obfuscation error of nested classes in generic classes.
- Fixed bug, while building with IL2CPP: Field/Method is not definied/found.
- Pro only: Some fixes for random code creation.
2.5
- Important! The folder structure changed. The Obfuscator files will now be found in the folder OPS!
- Fix for a possible Nullpointer exception in the BuildPostProcessor.
- Added to all scripts, using an unityeditor, an #define to prevent possible resource.asset errors.
- Renaming fix for IEnumeration methods.
- Added a new setting, under the Advanced settings, to define custom attributes to behave like DoNotRename.
- Pro only: Some fixes for random code creation.
- Some gui adjustments
2.4
- IBM Watson SDK compatibility
- New method to find GUI methods
- New user Gui
- Warning fix for old build platforms
- Bug fix: Vice Versa settings wont get saved
- Bug fix: Sometimes base classes wont get obfuscated, but inherited classes get obfuscated
2.31
- Needs now at least Unity 5.6.1 (Because of IL2cpp)
- Javascript/Unityscript obfuscation is no longer supported, because of the Unity Asset Store Guidelines.
2.3
- Now with IL2CPP obfuscation!
- New Readme.
- Some Gui fixes.
- Error fix trough random code containing methods with try/catch
2.2
Animation Update:
- Find automatically animation event method option.
- Fixed some problems with animation itself.
- Fixed some problems with inheritance.
- Added more Unity messages to skip.
2.11
=> IMPORTANT UPDATE!
- Fixed Problem with Generic Addon!
- Fixed Problem with classes sharing the same name!
2.1
- Beta: Xbox 360/One and Playstation 3/4
- Option for obfuscation of Abstract and Generic Unity classes
- Some minor fixes
=> Close Obfuscator Window before update!
2.0
- Now, after dll compile Obfuscation. Not post project build Obfuscation.
-> If you notice any problems switch back to obfuscator version 1.37.
-> Auto GUI finder now will only find methods from the first scene.
- New Enum Obfuscation option
- Now with better progress bar
- Some bugfixes

1.37
- Dynamic DLL fix
- Some Adjustment with Attributes
1.36
- Saving Settings in 'Settings.txt', but Android Settings. (To protect passwords)
- Error Code 1 and 10 fix are now automatic fallbacks.
- Fixed a bug when calling zipalign on an Android apk and Unity has not enough rights. (An GAME_Obfuscated.apk gets now created)
1.35
- Error Code 1 auto fix adjusted and optimized!
- Namespace obfuscation vice versa option.
- Serialization Bug Fix (#5)
- Some intern optimizations
1.34
- Fixed Bug: Finding GUI methods containing characters like 'P' or '0'. (#4)
- Adjusted Obfuscation for Serializable classes and fields.
- Some intern optimizations
1.33
- Auto scan for GUI methods (No longer need of DoNotRename on GUI methods)
- Find paths for Android sign process
- Added new GUI Elements to activate Error Code 1 and 10 auto fix.
- Adjusted UnityScript Attributes
1.32
- Performance Improvement
- Fix for Android Sign / Zip Bug
- New Message for Inheritance Problems between Obfuscated and not 
Obfuscated classes
- New Attribute(Class/Method) that make the obfuscator ignore MethodBodies obfuscation
1.31
- Switching from AES String Obfuscation to custom RSA String Obfuscation to allow Metro (Windows Universal/…) platform support.
- Metro (Windows Universal/…) platform support.
- Fixed Bug ‘Store in Ram’ (#2)
- Fixed Bug ‘Code E9’ (#3)
1.3
- Added Facebook Platform support
- Added WebGL Platform support
- Demo has not to get removed anymore
1.22
- Fixed 'Adding Random Code' bug #1
- Upgraded to Mono.Cecil 0.96
- Fixed some errors in the Readme
1.21 Switched from Unity 5.6.0 to 5.1.0
1.2 Fixed Bug on Mac. And added IPhone support!
1.11 Android Sign Process
1.1 Added Android Build
1.01 Several BugFixes.
1.0 First official release of Obfuscator Free and Pro.
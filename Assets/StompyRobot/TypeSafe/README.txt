  _______                _____        __     
 |__   __|              / ____|      / _|    
    | |_   _ _ __   ___| (___   __ _| |_ ___ 
    | | | | | '_ \ / _ \\___ \ / _` |  _/ _ \
    | | |_| | |_) |  __/____) | (_| | ||  __/
    |_|\__, | .__/ \___|_____/ \__,_|_| \___|
        __/ | |                              
       |___/|_|                    
   
--- (C) Stompy Robot LTD 2015 ---

TypeSafe scans your project and generates strong-typed static classes from your resources, layers and tags. Using these classes instead of 'naked-strings' in your code will dramatically reduce the number of runtime errors caused by mistyping, moving or renaming resources, layers or tags.

For detailed documentation, visit the web page at https://www.stompyrobot.uk/tools/typesafe/documentation

Trigger a scan from the Assets menu (Assets/TypeSafe Refresh)

The settings and welcome windows can be opened from the Window menu (Window/TypeSafe)

Support:
--------

If you encounter problems while using TypeSafe, feel free to post in the thread on the Unity forums or send us an email at contact@stompyrobot.uk

Source code is available for free by request. See the FAQ on the website for details.

Restrictions:
--------

 - Unauthorised distribution of this library is not permitted. See Unity Asset Store EULA for details.
 
Credits:
--------

- Programming/Design by Simon Moles @ Stompy Robot (simon@stompyrobot.uk, www.stompyrobot.uk)
- Settings/Welcome window header background provided by Subtle Patterns (www.subtlepatterns.com)

Change Log:
--------

1.1.1
----------

New:
- Added Unload() method to resources.
- Added UnloadAll() and UnloadAllRecursive() methods to resource folders.

Improvements:
- Resource wrappers now cache with a weak reference, so they do not prevent Resources.UnloadUnusedResources() working.

1.1.0
----------

New:
- Added ITypeSafeDataSource interface for using TypeSafe for custom data. See documentation for details.
- Added TypeSafeApi static class for invoking TypeSafe scan/compile process via script.
- PrefabResource.Instantiate(Vector3) overload added (no rotation)

Fixes:
- Fixed "Object has been cleaned up" warning when saving a scene with TypeSafe enabled.

1.0.1
----------

New:
- Added documentation comments to generated resource folder methods (GetContents, etc).

Fixes:
- Welcome window no longer shows up after the first time
- Fixed Editor-only resources being included in scan

1.0.0
----------

Initial version.

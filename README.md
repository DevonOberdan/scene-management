# Scene Management
Custom Unity package containing system to load/request new scenes.


Derived from the Unity Open Project, [Chop Chop](https://github.com/UnityTechnologies/open-project-1/tree/main).


## Initial Setup:

1.   Import the `Scene Management Sample` sample into project.
2.   Inside of the generated `SceneManagementSetup` folder, do the following:

        - Make a path to `Assets/ScriptableObjects/SceneManagement`; select the `Events` and `SceneConfigs` folder and place them inside this SceneManagement folder.
        - Mark both the `Events` and `SceneConfigs` folders as **Addressable** on the top of the Inspector window.
        - Move the contents of the `Prefabs` folder to `Assets/Prefabs/SceneManagement`.
    
**Note**: 

- You will need to add a `SceneConfig` ScriptableObject asset for each new Scene you create, so be sure to add them to this `SceneConfigs` folder to ensure they are automatically setup to be addressable.

### Scenes:

1. Create a folder called `GameScenes` inside your `Scenes` folder.
2. Move the `ManagersScene` scene into this folder.
3. Mark this folder as **Addressable**. All new game scenes should be added to this folder.

### ManagerScene:

The imported sample includes a `ManagerScene` scene and a `ManagerSceneConfig` asset.
The intent of this scene is to house any scripts that should exist in perpetuity throughout the lifetime of your game. At a minimum, this contains `SceneLoader` script (housed in a prefab in the sample version of this scene). This may also include:

- AudioManager that serves as the single source for playing audio.

**Note**: 

- This scene, and its corresponding Config asset, will need to be configured similarly to the actual scenes of your game, as outlined below.

### Creating a new scene:

1.  Make your new scene file as you would normally.
2.  Place the scene into the `GameScenes` folder.
3.  Create a new `SceneConfig` inside of the `SceneConfigs` folder discussed above; follow naming convention of "*SceneName*Config".
4.  Select the new config asset; add the Scene file to the `SceneRef` field.

**Notes**:

- This variable takes in an AddressableAsset type, so you will be able to add your scene file if it is either marked as addressable or in a folder that has been marked as addressable, hence the setup of the GameScenes folder above.

- This process needs to be followed for the ManagerScene scene and the ManagerSceneConfig asset that was brought in through the sample.


### Loading a New Scene:

1. Add the SceneLoadRequester prefab to the scene you want to load from.
2. Call `SceneLoadRequester.Request()` with the SceneConfig belonging to the Scene that you want to load (this can be done directly via another script, but is commonly done through a UnityEvent).

### Load ManagerScene in Editor From any Scene:

1. Add in the EditorInitializer prefab into the scene you are working in.
2. `ManagerScene` and `EditorStartEventSO` should already be populated; add the `SceneLoadConfigSO` belonging to the given scene you are in.


### Initialization:

This scene is what your build loads into, and immediately loads in the Manager Scene and additively loads in the first scene of your game loop.

1. Move this scene into the top level of your Scenes folder.
2. Add Initialization to your Build Settings; it will be the only scene in your project's Build Settings due to the nature of how the remaining scenes are loaded through the Addressable system.

**Note**:

- This scene, and the folder it is in, should not be marked as Addressable.



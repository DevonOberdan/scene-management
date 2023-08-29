using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace FinishOne.SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private SceneLoadEventSO loadEvent = default;
        [SerializeField] private SceneLoadConfigSO startScene;

        [SerializeField] private SceneLoadEventSO editorStartEvent;

        private SceneLoadConfigSO currentScene;
        private SceneLoadConfigSO newSceneToLoad;
        private AsyncOperationHandle<SceneInstance> sceneOperationHandle;
        private AsyncOperationHandle<SceneInstance> unloadSceneOperationHandle;

#if UNITY_EDITOR
        private AsyncOperation editorStartUnloadOperation;
#endif

        private void OnEnable()
        {
            loadEvent.OnRequestLoad += LoadScene;

#if UNITY_EDITOR
            editorStartEvent.OnRequestLoad += EditorStart;
#endif
        }

        private void OnDisable()
        {
            loadEvent.OnRequestLoad -= LoadScene;
#if UNITY_EDITOR
            editorStartEvent.OnRequestLoad -= EditorStart;
#endif
        }

        private void EditorStart(SceneLoadConfigSO editorSceneStarted)
        {
            currentScene = editorSceneStarted;
        }

        private void LoadScene(SceneLoadConfigSO gameScene)
        {
            newSceneToLoad = gameScene;

            UnloadPreviousScene();

            if (currentScene == newSceneToLoad)
            {
                if (unloadSceneOperationHandle.IsValid())
                    unloadSceneOperationHandle.Completed += handle => LoadNewScene();
#if UNITY_EDITOR
                else
                    editorStartUnloadOperation.completed += action => LoadNewScene();
#endif
            }
            else
            {
                LoadNewScene();
            }
        }

        private void UnloadPreviousScene()
        {
            if (currentScene != null)
            {
                if (currentScene.sceneRef.OperationHandle.IsValid())
                    unloadSceneOperationHandle = currentScene.sceneRef.UnLoadScene();
#if UNITY_EDITOR
                else
                    editorStartUnloadOperation = SceneManager.UnloadSceneAsync(currentScene.sceneRef.editorAsset.name);
#endif
            }
        }

        private void LoadNewScene()
        {
            sceneOperationHandle = newSceneToLoad.sceneRef.LoadSceneAsync(LoadSceneMode.Additive, true, 0);
            sceneOperationHandle.Completed += OnNewSceneLoaded;
        }

        private void OnNewSceneLoaded(AsyncOperationHandle<SceneInstance> obj)
        {
            currentScene = newSceneToLoad;
            newSceneToLoad = null;

            SceneManager.SetActiveScene(obj.Result.Scene);
        }
    }
}
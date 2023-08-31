using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace FinishOne.SceneManagement
{
    public class EditorInitializer : MonoBehaviour
    {
#if UNITY_EDITOR

        [SerializeField] private SceneLoadConfigSO managerScene;

        [SerializeField] private SceneLoadConfigSO currentStartScene;

        [SerializeField] private AssetReference editorStartEventSO;

        private void Awake()
        {
            if (!SceneManager.GetSceneByName(managerScene.sceneRef.editorAsset.name).isLoaded)
                managerScene.sceneRef.LoadSceneAsync(LoadSceneMode.Additive, true).Completed += OnManagerLoaded;
        }

        private void OnManagerLoaded(AsyncOperationHandle<SceneInstance> obj)
        {
            editorStartEventSO.LoadAssetAsync<SceneLoadEventSO>().Completed += NotifyOfEditorStart;
        }

        private void NotifyOfEditorStart(AsyncOperationHandle<SceneLoadEventSO> obj)
        {
            if (currentStartScene != null)
                obj.Result.Raise(currentStartScene);
            else
                print("Manager Scene loaded, but currentScene value not set to broadcast out.");
        }
#endif
    }
}
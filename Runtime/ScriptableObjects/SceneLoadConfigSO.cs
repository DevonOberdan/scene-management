using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ScriptableObjectLibrary {

    [CreateAssetMenu(fileName = nameof(SceneLoadConfigSO), menuName = "SceneManagement/" + nameof(SceneLoadConfigSO), order = 0)]
    public class SceneLoadConfigSO : ScriptableObject
    {
        public AssetReference sceneRef;
    }
}

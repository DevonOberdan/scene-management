using UnityEngine;
using UnityEngine.AddressableAssets;

namespace FinishOne.SceneManagement
{
    [CreateAssetMenu(fileName = nameof(SceneLoadConfigSO), menuName = "SceneManagement/" + nameof(SceneLoadConfigSO), order = 0)]
    public class SceneLoadConfigSO : ScriptableObject
    {
        public AssetReference sceneRef;
    }
}

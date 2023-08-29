using UnityEngine;
using UnityEngine.Events;

namespace FinishOne.SceneManagement
{
    [CreateAssetMenu(fileName = nameof(SceneLoadEventSO), menuName = "SceneManagement/" + nameof(SceneLoadEventSO), order = 0)]
    public class SceneLoadEventSO : ScriptableObject
    {
        public UnityAction<SceneLoadConfigSO> OnRequestLoad;

        public void Raise(SceneLoadConfigSO sceneToLoad)
        {
            if (OnRequestLoad != null)
                OnRequestLoad(sceneToLoad);
        }
    }
}

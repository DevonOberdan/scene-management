using UnityEngine;

namespace FinishOne.SceneManagement
{
    public class SceneLoadRequester : MonoBehaviour
    {
        [SerializeField] private SceneLoadEventSO loadEvent = default;
        [SerializeField] private SceneLoadConfigSO defaultScene;

        public void Request(SceneLoadConfigSO sceneToLoad = null)
        {
            if (sceneToLoad != null)
                loadEvent.OnRequestLoad?.Invoke(sceneToLoad);
            else
                loadEvent.OnRequestLoad?.Invoke(defaultScene);
        }
    }
}

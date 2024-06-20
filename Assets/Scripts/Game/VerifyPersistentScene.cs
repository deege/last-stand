using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Deege.Game
{
    /**
     * Verify that a scene designated as persistent is loaded.
     */
    public class VerifyPersistentScene : MonoBehaviour

    {
        [SerializeField] private string requiredSceneName = "PersistentGameManager";

        private void Awake()
        {
#if UNITY_EDITOR
            CheckAndLoadScene();
#endif
        }

#if UNITY_EDITOR
        private void CheckAndLoadScene()
        {
            Scene requiredScene = SceneManager.GetSceneByName(requiredSceneName);

            // Check if the scene is not loaded.
            if (!requiredScene.isLoaded)
            {
                string scenePath = GetSceneAssetPathByName(requiredSceneName);
                if (!string.IsNullOrEmpty(scenePath))
                {
                    SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);
                }
                else
                {
                    Debug.LogError($"Scene with name {requiredSceneName} not found!");
                }
            }
        }

        private string GetSceneAssetPathByName(string sceneName)
        {
            string filter = $"t:Scene {sceneName}";
            string[] guids = AssetDatabase.FindAssets(filter);
            if (guids.Length > 0)
            {
                return AssetDatabase.GUIDToAssetPath(guids[0]);
            }

            return null;
        }
#endif
    }


}
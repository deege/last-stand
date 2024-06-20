using System.Collections;
using System.Collections.Generic;
using Deege.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Deege.Game
{
    public class ApplicationManager : MonoBehaviour
    {
        [Header("Game Event Channels")]
        [SerializeField] internal VoidEventChannelSO OnApplicationStart;
        [SerializeField] internal VoidEventChannelSO OnGameQuit;
        [SerializeField] internal VoidEventChannelSO OnGameStart;
        [SerializeField] internal VoidEventChannelSO OnGameStartInitialize;
        [SerializeField] internal VoidEventChannelSO OnGameOver;

        [Header("Configuration")]
        [SerializeField] internal string FirstGameLevel = "";


        // Start is called before the first frame update
        void Awake()
        {
            LoadLevel("UI");
        }

        /// <summary>
        /// Initiates the loading process for a specified level (scene).
        /// </summary>
        /// <param name="sceneName">The name of the scene to load.</param>
        public void LoadLevel(string sceneName)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }

        /// <summary>
        /// Checks if a specified scene is currently loaded in the game.
        /// </summary>
        /// <param name="sceneName">The name of the scene to check.</param>
        /// <returns>True if the scene is loaded, false otherwise.</returns>
        internal bool IsSceneLoaded(string sceneName)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                if (SceneManager.GetSceneAt(i).name == sceneName)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Coroutine for asynchronously unloading a scene.
        /// </summary>
        /// <param name="sceneName">The name of the scene to unload.</param>
        /// <returns>An IEnumerator for coroutine functionality.</returns>
        private IEnumerator UnloadScene(string sceneName)
        {
            Debug.Log($"Unloading {sceneName}");
            if (SceneManager.sceneCount > 1)
            {
                AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(sceneName);
                while (asyncUnload != null && !asyncUnload.isDone)
                {
                    yield return null;
                }
            }
        }
    }
}
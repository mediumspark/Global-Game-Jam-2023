using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Singleton;

        //TODO: Search for old GameManagers and delete them upon entering new 
        private void Awake()
        {
            Singleton = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }

        private void SceneManager_sceneLoaded(Scene NextScene, LoadSceneMode arg1)
        {
            switch (NextScene.buildIndex)
            {
                case 0:
                    Destroy(
                    FindObjectsOfType<GameManager>()[1].gameObject);
                    return;

                default:
                    //ResetBattleManager
                    break; 
            }
        }
        
    }
}

namespace Interactables
{
    public interface IInteractable
    {
        public void OnInteract(); 
    }

    public interface IEnemy
    {
        public void OnEnemyHit();
    }
}

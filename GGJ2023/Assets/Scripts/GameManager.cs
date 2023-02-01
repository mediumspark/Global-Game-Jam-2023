using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Battle;

namespace Managers
{
    [RequireComponent(typeof(BattleManager))]
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
                case 2:
                    GetComponent<BattleManager>().StartBattle();
                    return;

                case 0:
                    Destroy(
                    FindObjectsOfType<GameManager>()[1].gameObject);
                    return;

                default:
                    //ResetBattleManager
                    break; 
            }
        }

        public void ToBattleScene(EnemyPartyEntity Enemy, PlayerPartyEntity Player)
        {
            SceneManager.LoadScene(2); 
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
        public void OnEnemyHit(EnemyPartyEntity e, PlayerPartyEntity p);
    }
}

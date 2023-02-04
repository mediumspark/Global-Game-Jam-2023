using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Player;
using Fungus; 

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Singleton;
        public Flowchart Flow; 

        //TODO: Search for old GameManagers and delete them upon entering new 
        private void Awake()
        {
            Singleton = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            Flow.ExecuteIfHasBlock("Getting to the pyramid");
        }

        private void SceneManager_sceneLoaded(Scene NextScene, LoadSceneMode arg1)
        {
            switch (NextScene.buildIndex)
            {
                case 0:
                    Destroy(
                    FindObjectsOfType<GameManager>()[1].gameObject);
                    return;

                case 2:
                    Party p = GetComponent<Party>(); 
                    for(int i = 0; i <= p.PlayerParty.Count; i++)
                    {
                        try
                        {
                            PlayerUnit Unit = Instantiate(p.PlayerParty[i].BattleUnit, transform.GetChild(i)) as PlayerUnit;
                            BattleManager.Singleton.PlayerUnits.Add(Unit); 
                        }
                        catch
                        {
                        //    Debug.Log("Empty");
                        }
                    }

                    for(int i = 3; i < transform.childCount -1; i++)
                    {
                        try
                        {
                            BattleUnit Unit = Instantiate(p.EnemyParty[i - 3].BattleUnit, transform.GetChild(i));
                            BattleManager.Singleton.EnemyUnits.Add(Unit); 
                        }
                        catch
                        {
                          //  Debug.Log("Empty");
                        }
                    }

                    BattleManager.Singleton.OnBattleStarted(); 
                    
                    return; 

                default:
                    //ResetBattleManager
                    break; 
            }
        }
        
        public void LoadBattleLevel()
        {
            SceneManager.LoadScene(2); 
        }

        public void OverWorld()
        {
            SceneManager.LoadScene(1); 
        }

        public void BackToMainMenu()
        {
            SceneManager.LoadScene(0); 
        }

        public void CloseGame()
        {
            Application.Quit(); 
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

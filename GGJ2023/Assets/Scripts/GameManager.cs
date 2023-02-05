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
        public bool PlayedStart = false;
        public int OWEnemyID; 
        public Dictionary<string, Vector3> LastPositionInScene = new Dictionary<string, Vector3>(); 
        public Player.Player player; 

        //TODO: Search for old GameManagers and delete them upon entering new 
        private void Awake()
        {
            Singleton = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }

        private void Update()
        {
            if(player != null)
                LastPositionInScene[SceneManager.GetActiveScene().name] = player.transform.position; 
        }

        private void SceneManager_sceneLoaded(Scene NextScene, LoadSceneMode arg1)
        {
            switch (NextScene.buildIndex)
            {
                case 0:
                    try
                    {
                        Destroy(FindObjectsOfType<GameManager>()[1].gameObject);
                    }
                    catch
                    {
                        Debug.Log("Only one Game Manager, YAY");
                    }
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

                case 1:
                    player = FindObjectOfType<Player.Player>();
                    Flow = GameObject.Find("LevelFlow").GetComponent<Flowchart>();

                    if (!LastPositionInScene.ContainsKey(NextScene.name))
                        LastPositionInScene.Add(NextScene.name, Vector3.zero);
                    else
                    {
                        gameObject.GetComponent<LevelManager>().SetLevelThings(NextScene.name);
                        player.GetComponent<CharacterController>().enabled = false; 
                        player.transform.position = LastPositionInScene[NextScene.name];
                        player.GetComponent<CharacterController>().enabled = true;

                    }

                    if (!PlayedStart)
                    {
                        Flow.ExecuteIfHasBlock("Getting to the pyramid");
                    } PlayedStart = true; 
                    return; 

                default:
                    //ResetBattleManager
                    Flow = GameObject.Find("LevelFlow").GetComponent<Flowchart>();
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

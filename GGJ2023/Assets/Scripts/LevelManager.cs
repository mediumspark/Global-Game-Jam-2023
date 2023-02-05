using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Interactables;
using System.Linq; 

public class LevelManager : MonoBehaviour
{
    public Dictionary<string, int> TimesLevelLoaded = new Dictionary<string, int>();
    public List<OverworldEnemy> CurrentEnemiesList = new List<OverworldEnemy>();
    public List<int> DefeatedEnemiesInLevel = new List<int>(); 

    public void SetLevelThings(string scene)
    {
        CurrentEnemiesList = FindObjectsOfType<OverworldEnemy>().ToList();

        if (!TimesLevelLoaded.ContainsKey(scene))
        {
            TimesLevelLoaded.Add(scene, 0); 
        }

        TimesLevelLoaded[scene]++;
        foreach (var i in DefeatedEnemiesInLevel)
        {
            CurrentEnemiesList[i].gameObject.SetActive(false);
        }
        
        
    }
}

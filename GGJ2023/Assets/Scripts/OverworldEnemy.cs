using UnityEngine;
using Managers;
using System.Collections.Generic; 

[System.Serializable]
public struct Stats
{
    public int Health; 
}

namespace Interactables
{
    public class OverworldEnemy : MonoBehaviour
    {
        public List<Units> Party = new List<Units>(); 
    }
}

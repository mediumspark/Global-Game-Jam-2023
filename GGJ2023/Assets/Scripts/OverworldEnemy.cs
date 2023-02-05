using UnityEngine;
using Managers;
using System.Collections.Generic;

[System.Serializable]
public struct Stats
{
    public int maxHealth, health;
    public int MaxHealth { get => maxHealth; set => maxHealth = value;}
    public int Health { get => health; set => health = value;  }
}

namespace Interactables
{
    public class OverworldEnemy : MonoBehaviour
    {
        public List<Units> Party = new List<Units>(); 
    }
}

using UnityEngine;
using Managers;

[SerializeField]
public struct Stats
{
    public int Health, Speed, Defense, Attack; 
}

namespace Interactables
{
    public class OverWorldEnemy : MonoBehaviour
    {
        public Stats EnemeyStates; 
    }
}

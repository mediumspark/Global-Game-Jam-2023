using UnityEngine;
using System.Collections.Generic;

//Player State Machine for the overworld character
namespace Player
{
    public class Party : MonoBehaviour
    {
        public List<PlayerUnitScriptableObject> PlayerParty;

        public List<Units> EnemyParty;
    }
}

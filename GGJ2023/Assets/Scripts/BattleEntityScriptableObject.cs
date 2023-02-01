using UnityEngine;
using System.Collections.Generic;

namespace Battle
{
    public class BattleEntityScriptableObject : ScriptableObject
    {
        public GameObject Model; 
        public List<Attack> Attacks = new List<Attack>(); 
        /*Stats 
         */
    }
}
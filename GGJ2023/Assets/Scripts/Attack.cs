using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers; 

namespace Battle
{
    [System.Serializable]
    public class Attack
    {
        public string name; 
        public int speed;
        public Sprite Icon;
        
        public Attack() { }

        public Attack(string name)
        {
            this.name = name; 
        }

        public virtual void OnCast(BattleEntityInstance Target) 
        {
            GameManager.Singleton.GetComponent<BattleManager>().MoveUp(speed); 

        }
    }
}

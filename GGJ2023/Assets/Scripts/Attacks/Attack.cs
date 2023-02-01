using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    [System.Serializable]
    public class Attack
    {
        public string name; 
        public float speed;
        public Sprite Icon;
        
        public Attack() { }

        public Attack(string name)
        {
            this.name = name; 
        }

        protected virtual void OnCast() { }
    }
}

using UnityEngine;
using System.Linq;
using Managers; 

namespace Battle
{
    [CreateAssetMenu(fileName = "new Battle Entity", menuName = "Battle Entity/PlayerParty/A")]
    public class A : BattleEntityScriptableObject
    {
        [System.Serializable]
        public class A_Basic : Attack
        {
            public A_Basic(string name)
            {
                this.name = name; 
            }

            public override void OnCast(BattleEntityInstance target)
            {
                Debug.Log("Attack 1");
            }
        }

        [System.Serializable]
        public class A_Special_1 : Attack
        {
            public A_Special_1(string name)
            {
                this.name = name;
            }

            public override void OnCast(BattleEntityInstance target)
            {
                Debug.Log("Attack 2");
            }
        }

        private void OnValidate()
        {
            for(int i = 0; i < Attacks.Count(); i++)
            {
                string it = Attacks[i].name;

                if (it == "Finger Gun" ||
                    it == "Hand Gambit")
                    return; 
            }

            Attacks.Add(new A_Basic("Finger Gun")); 
            Attacks.Add(new A_Special_1("Hand Gambit")); 
        }

    }
}
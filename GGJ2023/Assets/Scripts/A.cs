using UnityEngine;
using System.Linq; 

namespace Battle
{
    [CreateAssetMenu(fileName = "new Battle Entity", menuName = "Battle Entity/PlayerParty/A")]
    public class A : BattleEntity
    {
        [System.Serializable]
        public class A_Basic : Attack
        {
            public A_Basic(string name)
            {
                this.name = name; 
            }

            protected override void OnCast()
            {
            }
        }

        [System.Serializable]
        public class A_Special_1 : Attack
        {
            public A_Special_1(string name)
            {
                this.name = name;
            }

            protected override void OnCast()
            {
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
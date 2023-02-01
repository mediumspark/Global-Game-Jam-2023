using UnityEngine;
using UnityEngine.UI;
using Managers; 

namespace Battle
{
    public class BattleEntityInstance : MonoBehaviour
    {
        public BattleEntityScriptableObject Entity;
        public bool Attacking; 
        public int Health;
        public bool Alive => Health > 0;

        public class UnitToClickOn : MonoBehaviour
        {
            CapsuleCollider Col;
            public BattleEntityInstance BattleInstance; 

            private void Awake()
            {
                if (!GetComponent<CapsuleCollider>())
                    gameObject.AddComponent<CapsuleCollider>();

                Col = GetComponent<CapsuleCollider>();
                Col.height = 4;
                Col.radius = 1;
                Col.center = new Vector3(0, 1, 0); 
            }

            private void OnMouseDown()
            {
                BattleInstance.OnSelect(); 
            }
        }

        public void AddClickable(GameObject Unit)
        {
            Unit.AddComponent<UnitToClickOn>().BattleInstance = this; 
        }

        public void OnSelect()
        {
            BattleManager BM = GameManager.Singleton.GetComponent<BattleManager>();
            if (!BM.Targeting || BM.Attacker == this && BM.CurrentAttackSelecting == null)
            {
                return;
            }
            BM.Target = this;
            BM.BattleAttackQueue += () => BM.CurrentAttackSelecting.OnCast(this);
            BM.Attacker = BM.NextPlayerUnit();

            BM.ClearAbilityPanel();
            BM.AddAbilitiesToPanel(BM.Attacker.Entity.Attacks);
        }

    }
}
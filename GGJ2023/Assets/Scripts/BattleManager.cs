using UnityEngine;
using Battle;
using Player;
using Interactables;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using TMPro;

namespace Managers
{
    public delegate void BattleEnactment(); 

    public class BattleManager : MonoBehaviour
    {
        PlayerPartyEntity Player;
        EnemyPartyEntity Enemy;
        [SerializeField]
        GameObject TrackPanel;
        public BattleEnactment BattleAttackQueue; 
        [SerializeField]
        GameObject SliderPrefab;
        public bool Targeting; 
        public GameObject AbilityPanel;
        public GameObject AbilityButtonPrefab; 
        [SerializeField]
        BattleInstance Battle;
        public BattleEntityInstance Target, Attacker;
        public Attack CurrentAttackSelecting; 
        public List<GameObject> BattleSpots = new List<GameObject>(); 

        internal void ClearAbilityPanel()
        {
            for (int i = 0; i < AbilityPanel.transform.childCount; i++)
                Destroy(AbilityPanel.transform.GetChild(i).gameObject); 
        }

        public void MoveUp(int Speed)
        {
            Battle.MoveUp(Battle.UnitsAliveAndSummoned.IndexOf(Attacker), Speed); 
        }

        internal void AddAbilitiesToPanel(List<Attack> attacks)
        {
            for(int i = 0; i < attacks.Count; i++)
            {
                Button go = Instantiate(AbilityButtonPrefab, AbilityPanel.transform).GetComponent<Button>();
                TextMeshProUGUI Text = go.GetComponentInChildren<TextMeshProUGUI>();

                Text.text = attacks[i].name;
                go.onClick.AddListener(() => Targeting = true);  
            }
        }

        public BattleEntityInstance NextPlayerUnit()
        {
            if (Attacker.Entity.name == "A")
            {
                return Battle.UnitsAliveAndSummoned[1];
            }
            if (Attacker.Entity.name == "B")
            {
                return Battle.UnitsAliveAndSummoned[2];
            }

            if (Attacker.Entity.name == "C")
            {
                RunCommands(); 
                return null;
            }
            return null; 
        }

        private void RunCommands()
        {
            Debug.Log("Simple Attack Target");
            //TMP
            BattleAttackQueue += () => Attacker = Battle.UnitsAliveAndSummoned[0]; 
            BattleAttackQueue.Invoke(); 
        }

        private void Awake()
        {
            Player = GetComponent<Party>().PlayerParty; 
        }

        public void SetUpBattleData(OverWorldEnemy EnemyOW)
        {
            transform.GetChild(0).gameObject.SetActive(true); 
            Enemy = EnemyOW.EnemyParty;
            EnemyOW.OnEnemyHit(Enemy, Player);
        }

        public void StartBattle()
        {
            Battle = new BattleInstance(TrackPanel, Player, Enemy);
            Battle.CreateNewBattle(SliderPrefab);
            Attacker = Battle.UnitsAliveAndSummoned[0];
            ClearAbilityPanel();
            AddAbilitiesToPanel(Attacker.Entity.Attacks);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            for(int i = 0; i < BattleSpots.Count; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(BattleSpots[i].transform.position, 1); 
            }    
        }
#endif
    }
}

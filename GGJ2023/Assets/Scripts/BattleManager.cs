using UnityEngine;
using Battle;
using Player;
using Interactables;

namespace Managers
{
    public class BattleManager : MonoBehaviour
    {
        PlayerPartyEntity Player;
        EnemyPartyEntity Enemy;

        private void Awake()
        {
            Player = GetComponent<Party>().PlayerParty; 
        }

        public void StartBattle(OverWorldEnemy EnemyOW)
        {
            Enemy = EnemyOW.EnemyParty;
            EnemyOW.OnEnemyHit(Enemy, Player);
        }
    }
}

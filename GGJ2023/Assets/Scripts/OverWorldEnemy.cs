using UnityEngine;
using Battle;
using Managers;

namespace Interactables
{
    public class OverWorldEnemy : MonoBehaviour, IEnemy
    {
        public EnemyPartyEntity EnemyParty; 

        public void OnEnemyHit(EnemyPartyEntity e, PlayerPartyEntity p)
        {
            GameManager.Singleton.ToBattleScene(e, p); 
        }
    }
}

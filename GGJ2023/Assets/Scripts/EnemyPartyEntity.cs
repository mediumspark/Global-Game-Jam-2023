using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Battle
{
    [CreateAssetMenu(fileName ="new Battle Entity", menuName ="Battle Entity/Mob Party")]
    public class EnemyPartyEntity : ScriptableObject
    {
        public List<BattleEntity> Party = new List<BattleEntity>();
    }
}
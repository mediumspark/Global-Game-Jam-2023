using UnityEngine;
using System.Collections.Generic;
using System.Collections; 

namespace Battle
{
    [CreateAssetMenu(fileName ="new Battle Entity", menuName ="Battle Entity/PlayerParty")]
    public class PlayerPartyEntity : ScriptableObject
    {
        public List<BattleEntity> Party = new List<BattleEntity>();
    }
}
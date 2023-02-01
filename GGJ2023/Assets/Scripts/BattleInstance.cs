using UnityEngine;
using Battle;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

namespace Managers
{
    [System.Serializable]
    public class BattleInstance
    {
        [System.Serializable]
        public class TrackPosition
        {
            public bool inWeather; 
            public int CurrentPlace; //Where the Unit is on the track
            public int Lap; 
            public int Position; //What Place they are compared to others on the track
            public Slider Slider;
        }

        PlayerPartyEntity Player;
        EnemyPartyEntity Enemy;

        GameObject SliderParent;

        public List<BattleEntityInstance> UnitsAliveAndSummoned = new List<BattleEntityInstance>(); 

        //First int for summon position, 
        Dictionary<int, TrackPosition> Track = new Dictionary<int, TrackPosition>();

        public BattleInstance(GameObject SliderParent, PlayerPartyEntity Player, EnemyPartyEntity Enemy)
        {
            this.SliderParent = SliderParent; this.Player = Player; this.Enemy = Enemy; 
        }

        private void AddToBattle(List<BattleEntityScriptableObject> BattleList, GameObject SliderParent)
        {
            int spawnIndex = 3; 

            foreach (BattleEntityScriptableObject BE in BattleList) {

                BattleEntityInstance go = new GameObject().AddComponent<BattleEntityInstance>();
                go.Entity = BE;
                BattleManager BM = GameObject.FindObjectOfType<BattleManager>();
                GameObject go2;
                switch (go.Entity.name)
                {
                    case "A":
                        go.AddClickable(go2 = GameObject.Instantiate(go.Entity.Model, BM.BattleSpots[0].transform));
                        go2.tag = "Player";
                        break;
                    case "B":
                        go.AddClickable(go2 = GameObject.Instantiate(go.Entity.Model, BM.BattleSpots[1].transform));
                        go2.tag = "Player";
                        break;
                    case "C":
                        go.AddClickable(go2 = GameObject.Instantiate(go.Entity.Model, BM.BattleSpots[2].transform));
                        go2.tag = "Player";
                        break;
                    default:
                        go.AddClickable(go2 = GameObject.Instantiate(go.Entity.Model, BM.BattleSpots[spawnIndex].transform));
                        go2.tag = "Enemy";
                        spawnIndex++;
                        break;
                }

                UnitsAliveAndSummoned.Add(go);
            }
        }

        public void MoveUp(int Unit, int Speed)
        {
            TrackPosition UnitMoving = Track[Unit];

            UnitMoving.CurrentPlace += Speed;

            if (UnitMoving.CurrentPlace + Speed < Track[Unit].Slider.maxValue) {
                UnitMoving.Slider.value = UnitMoving.CurrentPlace;
            }
            else
            {
                UnitMoving.Slider.value = UnitMoving.CurrentPlace + Speed - UnitMoving.Slider.maxValue;
                UnitMoving.Lap++;
                UnitMoving.CurrentPlace = 0;
            }

            foreach (KeyValuePair<int, TrackPosition> KV in Track)
            {
                List<TrackPosition> AllLocationsOnTrack = new List<TrackPosition>(); 

                for(int i = 0; i < Track.Keys.Count; i++)
                {
                    AllLocationsOnTrack.Add(Track[i]); 
                }

                var ALOT = AllLocationsOnTrack.OrderBy(c => c.CurrentPlace).ThenBy(c => c.Lap);

                AllLocationsOnTrack.Clear(); 

                foreach(var A in ALOT)
                {
                    AllLocationsOnTrack.Add(A); 
                }

                KV.Value.Position = AllLocationsOnTrack.IndexOf(KV.Value); 
            }
        }

        public void CreateNewBattle(GameObject SliderPrefab)
        {
            AddToBattle(Player.Party, SliderParent);
            AddToBattle(Enemy.Party, SliderParent); 

            for(int i = 0; i < UnitsAliveAndSummoned.Count; i++)
            {
                //Adds all living units to the track
                Slider sl = GameObject.Instantiate(SliderPrefab, SliderParent.transform).GetComponent<Slider>();
                TrackPosition TP = new TrackPosition();
                TP.Slider = sl; 
                Track.Add(i, TP);
            }
        }
    }
}

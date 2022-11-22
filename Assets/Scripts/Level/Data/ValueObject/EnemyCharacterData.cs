using System;
using System.Collections;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class EnemyCharacterData 
    {
        public int Healt;
        public float Speed;
        public int EarnedGold;
        public int PercentOfDropGem;
        public int EarnGem;
        public int Damage;
        public EnemySpawnData enemySpawnData;
    }
}
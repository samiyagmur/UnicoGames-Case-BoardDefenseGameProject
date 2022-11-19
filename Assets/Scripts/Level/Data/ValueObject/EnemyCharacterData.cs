using System;
using System.Collections;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class EnemyCharacterData 
    {
        public int Healt;
        public int Speed;

        public EnemySpawnData enemySpawnData;
    }
}
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;

namespace Data.ValueObject
{
    [Serializable]
    public class LevelData
    {
        [BoxGroup("EnemyData",centerLabel:true)]
        public EnemyData EnemyData;
        [BoxGroup("DefanderData", centerLabel: true)]
        public DefanderData DefanderData;

        [BoxGroup("DefanderData", centerLabel: true)]
        public int FailAmount;


    }
}
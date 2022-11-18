using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class RankedScore 
    {
        public string name;

        public int score;
    }


    [Serializable]
    public class ScoreData
    {
        public List<RankedScore> RankedScore = new List<RankedScore>();

        public int LastDiamondScore;

        public int LastGoldScore;
    }
    
}
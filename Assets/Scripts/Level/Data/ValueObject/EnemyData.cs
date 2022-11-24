using System;
using Type;
using UnityEngine;
using UnityEngine.Rendering;

namespace Data.ValueObject
{
    [Serializable]
    public class EnemyData
    {
        [SerializeField]
        public SerializedDictionary<EnemyType, EnemyCharacterData> enemies;
    }
}
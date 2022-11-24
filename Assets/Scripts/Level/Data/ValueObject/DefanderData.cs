using System;
using Type;
using UnityEngine;
using UnityEngine.Rendering;

namespace Data.ValueObject
{
    [Serializable]
    public class DefanderData
    {
        [SerializeField]
        public SerializedDictionary<DefanderType, DefanderCharacterData> DefanderCharacterData;
    }
}
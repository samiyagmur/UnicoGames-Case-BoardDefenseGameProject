using System;
using System.Collections;
using System.Collections.Generic;
using Type;
using UnityEngine;
using UnityEngine.Rendering;

namespace Data.ValueObject
{
    [Serializable]
    public class DefanderData
    {
        public SerializedDictionary<DefanderType, DefanderCharacterData> DefanderCharacterData;


    }
}
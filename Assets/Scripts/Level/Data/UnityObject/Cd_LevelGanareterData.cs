using Data.ValueObject;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "Cd_LevelGanareterData", menuName = "Data/LevelGanareterData")]
    [InlineEditor]
    public class Cd_LevelGanareterData : ScriptableObject
    {
        public LevelGanarateData LevelGanarateData;
    }
}
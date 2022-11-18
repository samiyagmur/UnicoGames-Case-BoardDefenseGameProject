using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "Cd_CameraData", menuName = "Data/CameraData")]
    public class Cd_CameraData : ScriptableObject
    {
        public CameraData cameraData;
    }
}
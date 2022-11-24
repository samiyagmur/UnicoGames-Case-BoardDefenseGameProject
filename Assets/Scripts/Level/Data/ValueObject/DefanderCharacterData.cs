using Sirenix.OdinInspector;
using Status;
using System;
using Type;

namespace Data.ValueObject
{
    [Serializable]
    public class DefanderCharacterData
    {
        public RoteteStatus roteteStatus;
        public DefenderAtackDirections defenderMoveStatus;
        public BulletType bulletType;
        public int Damage;
        public float Range;
        public int Price;
        public float Interval;
        public DefenderSpawnData defenderSpawnData;

    }
}
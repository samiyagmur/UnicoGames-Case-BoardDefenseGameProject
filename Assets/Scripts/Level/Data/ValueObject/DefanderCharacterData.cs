using System;
using Type;

namespace Data.ValueObject
{
    [Serializable]
    public class DefanderCharacterData
    {
        public RoteteStatus roteteStatus;
        public BulletType bulletType;
        public int Damage;
        public float Range;
        public int Price;
        public float Interval;
        public DefenderSpawnData defenderSpawnData;
    }
}
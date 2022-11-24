using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class GridData
    {
        public int Width;
        public int Height;
        public float HorizontalOffset;
        public float VerticalOffset;
        public Vector3 scale;
    }
}
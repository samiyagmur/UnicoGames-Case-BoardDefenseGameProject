using Sirenix.OdinInspector;
using System;
using System.Collections;
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
    }
}
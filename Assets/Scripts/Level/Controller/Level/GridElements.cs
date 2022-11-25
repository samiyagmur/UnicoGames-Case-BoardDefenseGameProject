using System;
using Type;
using UnityEngine;

namespace Controller
{
    [Serializable]
    public class GridElements
    {
        public GameObject GridElement;
        public Material Material;

        //public float Height;
        //public float Width;
        public float TotalHeight;

        public float TotalWeight;
        public Vector3 Scale;
        public GridElementStatus GridElementStatus;
    }
}
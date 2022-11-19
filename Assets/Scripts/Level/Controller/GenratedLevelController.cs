using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class GenratedLevelController : MonoBehaviour
    {
        [SerializeField]
        private List<GridElement> newGrid=new List<GridElement>();

        public List<GridElement> NewGrid { get => newGrid; set => newGrid = value; }

        public int LevelID;

        private void OnEnable()
        {
            EnemySignals.Instance.onLevelInitilize?.Invoke(newGrid);
        }
    }
}
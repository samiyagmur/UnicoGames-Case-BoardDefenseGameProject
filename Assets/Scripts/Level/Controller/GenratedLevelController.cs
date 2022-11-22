using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class GenratedLevelController : MonoBehaviour
    {
        [SerializeField]
        private List<GridElements> newGrid=new List<GridElements>();

        public List<GridElements> NewGrid { get => newGrid; set => newGrid = value; }

        public int LevelID;


        private void Start()
        {
            InitFetures();
        }

        private void InitFetures()
        {
            EnemySignals.Instance.onLevelInitilize?.Invoke(NewGrid);

            SelectSignals.Instance.onSelectedGrid?.Invoke(NewGrid);
            

            for (int i = 0; i < NewGrid.Count; i++)
            {
                transform.GetChild(i).GetComponent<Renderer>().material = NewGrid[i].Material;

            }
                
            

        }
    }
}
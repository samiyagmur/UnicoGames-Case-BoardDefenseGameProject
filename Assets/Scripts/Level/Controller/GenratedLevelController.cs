using Data.ValueObject;
using Signals;
using System;
using System.Collections;
using System.Collections.Generic;
using Type;
using UnityEngine;

namespace Controller
{
    public class GenratedLevelController : MonoBehaviour
    {
        [SerializeField]
        private List<GridElements> newGrid=new List<GridElements>();

        public List<GridElements> NewGrid { get => newGrid; set => newGrid = value; }


        private LevelData _levelData;

        private void OnGetLevelData(LevelData levelData)
        {
            _levelData = levelData;
            InitFetures();
        }
        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {

            CoreGameSignals.Instance.onGetLevelData += OnGetLevelData;
        }


        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onGetLevelData -= OnGetLevelData;
        }

        private void OnDisable() => UnsubscribeEvents();

      
        private void InitFetures()
        {
            EnemySignals.Instance.onLevelInit?.Invoke(NewGrid);

            InitMaterial();

            InitSelectableGridElement();

        }

        private void InitSelectableGridElement()
        {
            for (int i = 0; i < NewGrid.Count; i++)
            {
                if (NewGrid[i].gridElementStatus != GridElementStatus.Selectable)
                {
                    NewGrid.Remove(NewGrid[i]);
                }
            }

            SelectSignals.Instance.onSelectedGrid?.Invoke(NewGrid);
        }


        private void InitMaterial()
        {
            for (int i = 0; i < NewGrid.Count; i++)
            {
                transform.GetChild(i).GetComponent<Renderer>().material = NewGrid[i].Material;

            }
        }
    }
}
//private void InitFetures()
//{
//    EnemySignals.Instance.onLevelInit?.Invoke(NewGrid);

//    SelectSignals.Instance.onSelectedGrid?.Invoke(NewGrid);


//    for (int i = 0; i < NewGrid.Count; i++)
//    {
//        transform.GetChild(i).GetComponent<Renderer>().material = NewGrid[i].Material;

//    }



//}
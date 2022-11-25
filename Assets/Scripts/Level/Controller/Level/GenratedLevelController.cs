using Interfaces;
using Signals;
using System.Collections.Generic;
using Type;
using UnityEngine;

namespace Controller
{
    public class GenratedLevelController : MonoBehaviour, IAddObject
    {
        [SerializeField]
        private List<GridElements> newGrid = new List<GridElements>();

        public List<GridElements> NewGrid { get => newGrid; set => newGrid = value; }

        private void OnLevelInitilize(int levelID)
        {
            AddToPool();
        }

        private void OnPlay()
        {
            InitFetures();
            InitMaterial();
        }

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitilize += OnLevelInitilize;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onPlay += OnPlay;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitilize -= OnLevelInitilize;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onPlay -= OnPlay;
        }

        private void OnDisable() => UnsubscribeEvents();

        private void InitFetures()
        {
            EnemySignals.Instance.onLevelInit?.Invoke(NewGrid);
        }

        private void InitMaterial()
        {
            for (int i = 0; i < NewGrid.Count; i++)
            {
                transform.GetChild(i).GetComponent<Renderer>().material = NewGrid[i].Material;
            }
        }

        private void OnReset()
        {
        }

        private void AddToPool()
        {
            AddToPool(PoolObjectType.Level, gameObject);
        }

        public void AddToPool(PoolObjectType poolObjectType, GameObject obj)
        {
            PoolSignals.Instance.onAddNewType(poolObjectType, obj);
        }
    }
}
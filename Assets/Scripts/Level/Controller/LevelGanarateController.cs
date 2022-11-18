using Data.ValueObject;
using Interfaces;
using Managers;
using Signals;
using Type;
using UnityEngine;

namespace Controller
{
    public class LevelGanarateController : MonoBehaviour, IPullObject, IPushObject, ICanRenderable
    {


        [SerializeField]
        private LevelManager levelManager;

        private Camera _camera;

        private LevelGanarateData _levelGanarateData;

   
        public void SetData(LevelGanarateData levelGanarateData)
        {
            _levelGanarateData = levelGanarateData;
        }

        private void Awake() => Init();

        private void Init() => _camera = Camera.main;

        public bool CanSeeOnCamera(GameObject wallGameObject)
        {
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);

            return GeometryUtility.TestPlanesAABB(planes, wallGameObject.GetComponent<Collider>().bounds);
        }

        public GameObject PullFromPool(PoolObjectType poolObjectType)
        {
            return PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolObjectType);
        }

        public void PushToPool(PoolObjectType poolObjectType, GameObject obj)
        {
            PoolSignals.Instance.onReleaseObjectFromPool?.Invoke(poolObjectType, obj);
        }

        internal void ResetGenareLevel()
        {
            
        }
    }
}
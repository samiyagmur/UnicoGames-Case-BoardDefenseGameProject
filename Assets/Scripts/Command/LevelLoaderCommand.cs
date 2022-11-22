using Interfaces;
using Signals;
using System.Collections;
using Type;
using UnityEngine;

namespace Command
{
    public class LevelLoaderCommand : MonoBehaviour//,IPullObject
    {
        public void InsitializeLevel(int _levelID, Transform levelHolder)
        {
     
            Instantiate(Resources.Load<GameObject>($"LevelPrefabs/level{_levelID}"), levelHolder);
        }

        //public GameObject PullFromPool(PoolObjectType poolObjectType)
        //{
        //   return PoolSignals.Instance.onGetObjectFromPool(poolObjectType);
        //}
    }
}
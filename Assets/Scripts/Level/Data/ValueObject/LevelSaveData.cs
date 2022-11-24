using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class LevelSaveData
    {
        [SerializeField]
        public int _levelID;

        private const string _key = "levelID";
        private string _dataPath = _key + "1917" + ".es3";

        public string GetDataPath()
        {
            return _dataPath;
        }

        public string GetKey()
        {
            return _key;
        }

        public int GetLevelID()
        {
            return _levelID;
        }

        public void SetLevelID(int levelID)
        {
            _levelID = levelID;
        }
    }
}
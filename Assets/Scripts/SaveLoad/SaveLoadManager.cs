using Command;
using Interfaces;
using Managers;
using Signals;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class SaveLoadManager:ISaver,ILoader
    {
   
        private const string _key = "levelID";
        private string _dataPath = _key + "1917" + ".es3";

        public void UpdateSave<T>(T value)
        {
            

            if (!ES3.FileExists(_dataPath))
            {
                ES3.Save(_key, value, _dataPath);
            }
            else
            {
                ES3.Save(_key, value, _dataPath);
                Debug.Log(value);
            }
           
        }
        public T UpdateLoad<T>()
        {
            Debug.Log("working");
            if (!ES3.FileExists(_dataPath)) return default(T);
          
            if (!ES3.KeyExists(_key, _dataPath)) return default(T);

            Debug.Log("working");

            T objectToReturn = ES3.Load<T>(_key, _dataPath);

            Debug.Log(objectToReturn);
            return objectToReturn;
        }
    }
}

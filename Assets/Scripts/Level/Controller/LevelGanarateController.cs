using Data.ValueObject;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using Data.UnityObject;
using Sirenix.OdinInspector.Editor;
using System;

namespace Controller
{
   
    [Serializable]
    public class GridElement
    {
        public GameObject gridElement;
        public int _height;
        public int _width;
    }

    [ExecuteInEditMode]
    public class LevelGanarater :OdinEditorWindow
    {
        [MenuItem("Tools/LevelGanarater")]
        private static void OpenWindow()
        {
            GetWindow<LevelGanarater>().Show();
        }

        //[SerializeField]
        //private List<GameObject> newGrid;

        private LevelGanarateData _levelGanarateData;


        [BoxGroup("LevelEditor",centerLabel:true)]
        [SerializeField,ChildGameObjectsOnly]
        private GameObject InstanceGameObject;

        [SerializeField, BoxGroup("LevelEditor")]
        private Cd_LevelGanareterData cdLeveGanaraterData;

        int counter;
        private float xOffset;
        private float yOffset;

        private GridElement gridElement;


        [Button(ButtonSizes.Medium)]
        private void GenarateNewLevel()
        {
            counter = 0;

            _levelGanarateData = cdLeveGanaraterData.LevelGanarateData;

            GanerateGrid();
        }

        private  void GanerateGrid()
        {
            GameObject levelPrefab = new GameObject();
        
            levelPrefab.name = $"Level0";

            GenratedLevelController genratedLevelController = levelPrefab.AddComponent<GenratedLevelController>();

            genratedLevelController.NewGrid = new List<GridElement>();

            for (int h = 0; h < _levelGanarateData.gridData.Height; h++)
            {
                for (int v = 0; v < _levelGanarateData.gridData.Width; v++)
                {
                    genratedLevelController.NewGrid.Add(new GridElement());

                    genratedLevelController.NewGrid[counter].gridElement = GameObject.CreatePrimitive(PrimitiveType.Cube);

                    genratedLevelController.NewGrid[counter].gridElement.transform.SetParent(levelPrefab.transform);

                    genratedLevelController.NewGrid[counter]._height = h;

                    genratedLevelController.NewGrid[counter]._width = v;

                    //We can't use pool becouse that line script to editor mod;

                    Vector3 gridPos = new Vector3(v - xOffset, 0,h - yOffset);

                    genratedLevelController.NewGrid[counter].gridElement.transform.position = gridPos;
                   
                    counter++;
                
                    xOffset += _levelGanarateData.gridData.HorizontalOffset;
                }

                xOffset = 0;

                yOffset += _levelGanarateData.gridData.VerticalOffset;
            }

       


            CreatePrefabs(levelPrefab);

        }

        private void CreatePrefabs(GameObject levelPrefab)
        {
            if (!Directory.Exists("Assets/Resources/LevelPrefabs"))
                AssetDatabase.CreateFolder("LevelPrefabs", "Level0");

            string localPath = "Assets/Resources/LevelPrefabs/" + levelPrefab.name + ".prefab";

            localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

            bool prefabSuccess;
            PrefabUtility.SaveAsPrefabAssetAndConnect(levelPrefab, localPath, InteractionMode.UserAction, out prefabSuccess);
            if (prefabSuccess == true)
                Debug.Log("Prefab was saved successfully");
            else
                Debug.Log("Prefab failed to save" + prefabSuccess);

            DestroyImmediate(levelPrefab);
        }
    }
}

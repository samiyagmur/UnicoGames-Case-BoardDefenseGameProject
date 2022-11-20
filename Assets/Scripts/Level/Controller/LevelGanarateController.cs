using Data.ValueObject;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using Data.UnityObject;
using Sirenix.OdinInspector.Editor;
using System;
using Type;

namespace Controller
{
   
    [Serializable]
    public class GridElements
    {
        public GameObject _gridElement;
        public Material Material;
        public int Height;
        public int Width;
        public int TotalHeight;
        public int TotalWeight;
        public GridElementStatus gridElementStatus;

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

        [SerializeField, BoxGroup("LevelEditor")]
        public Material SelectedMaterial;

        [SerializeField, BoxGroup("LevelEditor")]
        private int _levelID;

        int counter;
        private float xOffset;
        private float yOffset;

        

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
        
            levelPrefab.name = $"Level{_levelID}";

            GenratedLevelController genratedLevelController = levelPrefab.AddComponent<GenratedLevelController>();

            genratedLevelController.NewGrid = new List<GridElements>();

            for (int h = 0; h < _levelGanarateData.gridData.Height; h++)
            {
                for (int v = 0; v < _levelGanarateData.gridData.Width; v++)
                {
                    genratedLevelController.NewGrid.Add(new GridElements());

                    genratedLevelController.NewGrid[counter]._gridElement = GameObject.CreatePrimitive(PrimitiveType.Cube);

                    genratedLevelController.NewGrid[counter]._gridElement.transform.SetParent(levelPrefab.transform);

                    genratedLevelController.NewGrid[counter].Height = h;

                    genratedLevelController.NewGrid[counter].Width = v;

                    genratedLevelController.NewGrid[counter].TotalHeight = _levelGanarateData.gridData.Height;

                    genratedLevelController.NewGrid[counter].TotalWeight = _levelGanarateData.gridData.Width;

                    genratedLevelController.NewGrid[counter].Material = SelectedMaterial;

                    if (counter < (_levelGanarateData.gridData.Height* _levelGanarateData.gridData.Width)/2)
                    {
                        genratedLevelController.NewGrid[counter].gridElementStatus = GridElementStatus.Selectable;
                    }
                    else
                    {
                        genratedLevelController.NewGrid[counter].gridElementStatus = GridElementStatus.UnSelectable;
                    }


                    //We can't use pool becouse that line script to editor mod;

                    Vector3 gridPos = new Vector3(v - xOffset, 0,h - yOffset);

                    genratedLevelController.NewGrid[counter]._gridElement.transform.position = gridPos;

                    counter++;
                
                    xOffset += _levelGanarateData.gridData.HorizontalOffset;
                }

                xOffset = 0;

                yOffset += _levelGanarateData.gridData.VerticalOffset;
            }

            CreatePrefabs(levelPrefab);
            _levelID++;
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

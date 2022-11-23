﻿using Data.ValueObject;
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
        public float Height;
        public float Width;
        public float TotalHeight;
        public float TotalWeight;
        public Vector3 Scale;
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
        private float _totalHeigh;
        private float _totalWeigt;
        private float _scaleX;
        private float _scaleZ;

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

            GenratedLevelController generatedLevelController = levelPrefab.AddComponent<GenratedLevelController>();

            generatedLevelController.NewGrid = new List<GridElements>();

            _scaleX = _levelGanarateData.gridData.scale.x;
            _scaleZ = _levelGanarateData.gridData.scale.z;

            _totalWeigt = _levelGanarateData.gridData.Width * _scaleX;
            _totalHeigh = _levelGanarateData.gridData.Height * _scaleZ;

            

            for (float h = 0; h < _totalHeigh; h += _scaleZ)
            {
                for (float v = 0; v < _totalWeigt; v += _scaleX)
                {
                    generatedLevelController.NewGrid.Add(new GridElements());

                    generatedLevelController.NewGrid[counter]._gridElement = GameObject.CreatePrimitive(PrimitiveType.Cube);

                    generatedLevelController.NewGrid[counter]._gridElement.transform.SetParent(levelPrefab.transform);

                    generatedLevelController.NewGrid[counter]._gridElement.transform.localScale = new Vector3(_scaleX,1, _scaleZ);

                    generatedLevelController.NewGrid[counter].Scale = new Vector3(_scaleX, 1, _scaleZ);

                    generatedLevelController.NewGrid[counter]._gridElement.tag = "GridElement";

                    generatedLevelController.NewGrid[counter]._gridElement.layer =LayerMask.NameToLayer("GridElement");

                    generatedLevelController.NewGrid[counter].Height = h;

                    generatedLevelController.NewGrid[counter].Width = v;

                    generatedLevelController.NewGrid[counter].TotalHeight = _totalHeigh;

                    generatedLevelController.NewGrid[counter].TotalWeight = _totalWeigt;

                    generatedLevelController.NewGrid[counter].Material = SelectedMaterial;

                    if (counter < (_levelGanarateData.gridData.Height* _levelGanarateData.gridData.Width)/2)
                    {
                        generatedLevelController.NewGrid[counter].gridElementStatus = GridElementStatus.Selectable;
                    }
                    else
                    {
                        generatedLevelController.NewGrid[counter].gridElementStatus = GridElementStatus.UnSelectable;
                    }


                    //We can't use pool becouse that line script to editor mod;

                    Vector3 gridPos = new Vector3(v - xOffset- (_totalWeigt / 2)+0.5f, 0,h - yOffset);

                    generatedLevelController.NewGrid[counter]._gridElement.transform.position = gridPos;

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

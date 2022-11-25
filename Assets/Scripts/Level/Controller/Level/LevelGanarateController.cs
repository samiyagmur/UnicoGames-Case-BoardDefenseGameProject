using Data.UnityObject;
using Data.ValueObject;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Controller
{
    [ExecuteInEditMode]
    public class LevelGanarater : OdinEditorWindow
    {
        [MenuItem("Tools/LevelGanarater")]
        private static void OpenWindow()
        {
            GetWindow<LevelGanarater>().Show();
        }

        private GridData _gridData;

        [BoxGroup("LevelEditor", centerLabel: true)]
        [SerializeField, ChildGameObjectsOnly]
        private GameObject InstanceGameObject;

        [SerializeField, BoxGroup("LevelEditor")]
        private Cd_LevelGanareterData cdLeveGanaraterData;

        [SerializeField, BoxGroup("LevelEditor")]
        public Material SelectedMaterial;

        [SerializeField, BoxGroup("LevelEditor")]
        private int _levelID;

        private int counter;
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

            _gridData = cdLeveGanaraterData.LevelGanarateData.gridData;

            GanerateGrid();
        }

        private void GanerateGrid()
        {
            GameObject levelPrefab = new GameObject();

            levelPrefab.name = $"Level{_levelID}";

            GenratedLevelController generatedLevelController = levelPrefab.AddComponent<GenratedLevelController>();

            generatedLevelController.NewGrid = new List<GridElements>();

            _scaleX = _gridData.scale.x;
            _scaleZ = _gridData.scale.z;

            _totalWeigt = _gridData.Width * _scaleX;
            _totalHeigh = _gridData.Height * _scaleZ;

            for (float h = 0; h < _totalHeigh; h += _scaleZ)
            {
                for (float v = 0; v < _totalWeigt; v += _scaleX)
                {
                    generatedLevelController.NewGrid.Add(new GridElements());

                    generatedLevelController.NewGrid[counter].GridElement = GameObject.CreatePrimitive(PrimitiveType.Cube);

                    generatedLevelController.NewGrid[counter].GridElement.transform.SetParent(levelPrefab.transform);

                    generatedLevelController.NewGrid[counter].GridElement.transform.localScale = new Vector3(_scaleX, 1, _scaleZ);

                    generatedLevelController.NewGrid[counter].Scale = new Vector3(_scaleX, 1, _scaleZ);

                    generatedLevelController.NewGrid[counter].GridElement.tag = "GridElement";

                    generatedLevelController.NewGrid[counter].GridElement.layer = LayerMask.NameToLayer("GridElement");

                    //generatedLevelController.NewGrid[counter].Height = h;

                    //generatedLevelController.NewGrid[counter].Width = v;

                    generatedLevelController.NewGrid[counter].TotalHeight = _totalHeigh;

                    generatedLevelController.NewGrid[counter].TotalWeight = _totalWeigt;

                    generatedLevelController.NewGrid[counter].Material = SelectedMaterial;

                    if (counter < (_gridData.Height * _gridData.Width) / 2)
                    {
                        generatedLevelController.NewGrid[counter].GridElement.tag = "GridElement";
                    }
                    else
                    {
                        generatedLevelController.NewGrid[counter].GridElement.tag = "GridUnSelectable";
                    }

                    //We can't use pool becouse that line script to editor mod;

                    Vector3 gridPos = new Vector3(v - xOffset - (_totalWeigt / 2) + 0.5f, 0, h - yOffset);

                    generatedLevelController.NewGrid[counter].GridElement.transform.position = gridPos;

                    counter++;

                    xOffset += _gridData.HorizontalOffset;
                }

                xOffset = 0;

                yOffset += _gridData.VerticalOffset;
            }

            Debug.Log(generatedLevelController.NewGrid.Count);

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
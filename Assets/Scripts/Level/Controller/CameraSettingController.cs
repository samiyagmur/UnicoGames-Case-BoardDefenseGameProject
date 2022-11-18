using Cinemachine;
using Data.ValueObject;
using Manager;
using System;
using System.Collections;
using UnityEngine;

namespace Controller
{
    public class CameraSettingController : MonoBehaviour
    {
        [SerializeField]
        private CharacterManager caracterManager;

        [SerializeField]
        private GameObject testobject;
        [SerializeField]
        Bounds bounds;

        private CameraSettingData _cameraSettingData;

        private CinemachineStateDrivenCamera _stateDrivenCamera;
        internal void SetData(CameraSettingData cameraSettingData, CinemachineStateDrivenCamera stateDrivenCamera)
        {
            _cameraSettingData = cameraSettingData;
            _stateDrivenCamera = stateDrivenCamera;
        }

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            //_stateDrivenCamera.Follow = caracterManager.transform;
        }
        private void Update()
        {
            var centerAtFront = new Vector3(bounds.center.x, bounds.center.y, bounds.max.z);
            var centerAtFrontTop = new Vector3(bounds.center.x, bounds.max.y, bounds.max.z);
            var centerToTopDist = (centerAtFrontTop - centerAtFront).magnitude;
            var minDistance = (centerToTopDist * 30) / 
                Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);

            Camera.main.transform.position = new Vector3(bounds.center.x, bounds.center.y, -minDistance);
            Camera.main.transform.LookAt(bounds.center);
        }

    }
}
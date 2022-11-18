using System;
using System.Collections;
using Type;
using UnityEngine;

namespace Controller
{
    public class CameraStateController : MonoBehaviour
    {
        [SerializeField]
        private Animator camAnimator;
        internal void WhenPressPlay() => ChangeCameraState(CameraStateType.Level);

        internal void WhenCharacterFail() => ChangeCameraState(CameraStateType.Fail);

        internal void WhenCharacterLevelComplated() => ChangeCameraState(CameraStateType.Finish);

        internal void WhenEnterNextLevel() => ChangeCameraState(CameraStateType.Level);

        internal void WhenPressReset() => ChangeCameraState(CameraStateType.Start);

        public void ChangeCameraState(CameraStateType cameraStateType) => camAnimator.Play(cameraStateType.ToString());


    }
}
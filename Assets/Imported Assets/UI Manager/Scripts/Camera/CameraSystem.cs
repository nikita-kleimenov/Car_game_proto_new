using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

namespace BG.UI.Camera
{
    public enum CameraState { Start, Process, Win, Fail}

    public class CameraSystem : MonoBehaviour
    {        
        #region Singleton
        private static CameraSystem _default;
        public static CameraSystem Default => _default;
        #endregion

        [Header("Cameras")]
        [SerializeField] private CinemachineVirtualCamera _startCamera;
        [SerializeField] private CinemachineVirtualCamera _processCamera;
        [SerializeField] private CinemachineVirtualCamera _winCamera;
        [SerializeField] private CinemachineVirtualCamera _failCamera;

        private Dictionary<CameraState, CinemachineVirtualCamera> _stateToCamera;
        private CameraState _curentState;

        public Action<CameraState, CameraState> OnStateChanged;
        public CameraState CurentState
        {
            get => _curentState;
            set
            {
                if (_curentState != value)
                {
                    _stateToCamera[value].Priority = 1;
                    _stateToCamera[_curentState].Priority = 0;
                    OnStateChanged?.Invoke(_curentState, value);
                    _curentState = value;
                }
            }
        }

        private void Awake()
        {
            _default = this;

            _stateToCamera = new Dictionary<CameraState, CinemachineVirtualCamera>();
            _stateToCamera.Add(CameraState.Start, _startCamera);
            _stateToCamera.Add(CameraState.Process, _processCamera);
            _stateToCamera.Add(CameraState.Win, _winCamera);
            _stateToCamera.Add(CameraState.Fail, _failCamera);
        }
    }
}
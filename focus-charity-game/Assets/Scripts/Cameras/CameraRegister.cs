using System;
using UnityEngine;
using Unity.Cinemachine;

public class CameraRegister : MonoBehaviour
{
    public void OnEnable()
    {
        CameraManager.AddCamera(GetComponent<CinemachineCamera>());
    }
    
    public void OnDisable()
    {
        CameraManager.RemoveCamera(GetComponent<CinemachineCamera>());
    }
}

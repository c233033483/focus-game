using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class CameraManager : MonoBehaviour
{
    private static readonly List<CinemachineCamera> Cameras = new();
    private static CinemachineCamera activeCamera;

    public static void SwitchCamera(CinemachineCamera newCam)
    {
        if (newCam == null) return;
        
        newCam.Priority = 10;
        activeCamera = newCam;

        foreach (CinemachineCamera cam in Cameras)
        {
            if (cam != activeCamera)
            {
                cam.Priority = 0;
            }
        }
    }

    public static void AddCamera(CinemachineCamera cam)
    {
        Cameras.Add(cam);
    }

    public static void RemoveCamera(CinemachineCamera cam)
    {
        Cameras.Remove(cam);
    }
}

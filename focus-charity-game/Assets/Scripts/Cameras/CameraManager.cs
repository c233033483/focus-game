using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class CameraManager : MonoBehaviour
{
    static List<CinemachineCamera> cameras = new List<CinemachineCamera>();
    
    public static CinemachineCamera ActiveCamera;

    // Helper method to check if the cam we pass in is the active camera or not
    public static bool IsActiveCamera(CinemachineCamera cam)
    {
        return cam == ActiveCamera;
    }

    public static void SwitchCamera(CinemachineCamera newCam)
    {
        newCam.Priority = 10;
        ActiveCamera = newCam;

        foreach (CinemachineCamera cam in cameras)
        {
            if (cam != ActiveCamera)
            {
                cam.Priority = 0;
            }
        }
    }
    
    
    // Keeping these methods static so we can access them from outside of the script
    public static void AddCamera(CinemachineCamera cam)
    {
        cameras.Add(cam);
    }

    public static void RemoveCamera(CinemachineCamera cam)
    {
        cameras.Remove(cam);
    }
}

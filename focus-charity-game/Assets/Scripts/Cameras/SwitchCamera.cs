using UnityEngine;
using Unity.Cinemachine;


public class SwitchCamera : MonoBehaviour
{
    public InteractionRay interactionRay;
    
    public CinemachineCamera orderCam;
    public CinemachineCamera sandwichCam;
    public CinemachineCamera coffeeCam;

    public GameObject cookingUiPanel;
    public GameObject orderingUiPanel;
    
    private void Start()
    {
        SwitchActiveCamera(1);
    }
    
    public void SwitchActiveCamera(int cam)
    {
        switch (cam)
        {
            case 1:
                CameraManager.SwitchCamera(orderCam);
                orderingUiPanel.SetActive(true);
                cookingUiPanel.SetActive(false);
                interactionRay.SetInteractionEnabled(false);
                break;
            case 2:
                CameraManager.SwitchCamera(sandwichCam);
                cookingUiPanel.SetActive(true);
                orderingUiPanel.SetActive(false);
                interactionRay.SetInteractionEnabled(true);
                break;
            case 3:
                CameraManager.SwitchCamera(coffeeCam);
                cookingUiPanel.SetActive(true);
                orderingUiPanel.SetActive(false);
                interactionRay.SetInteractionEnabled(true);
                break;
        }
    }
}

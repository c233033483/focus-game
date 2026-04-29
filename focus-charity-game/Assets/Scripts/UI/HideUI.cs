using UnityEngine;

public class HideUI : MonoBehaviour
{
    [SerializeField] private GameObject[] ui;

    public void HideAllUI()
    {
        foreach (GameObject i in ui)
        {
            i.SetActive(false);
        }
    }

    public void ShowAllUI()
    {
        foreach (GameObject i in ui)
        {
            i.SetActive(true);
        }
    }
}

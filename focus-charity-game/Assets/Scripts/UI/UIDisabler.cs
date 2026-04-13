using UnityEngine;

public class UIDisabler : MonoBehaviour
{
    public GameObject[] ui;

    void Start()
    {
        foreach (GameObject obj in ui)
        {
            obj.SetActive(false);
        }
    }
}

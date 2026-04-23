using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("Main Scene");
    }

    public void OpenFocusWebsite()
    {
        Application.OpenURL("http://www.focusireland.ie");
    }
}

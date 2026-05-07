using UnityEngine;

public class TipJar : MonoBehaviour, IClickable
{
    public void Interact()
    {
        print("interacted with tip jar");
        Application.OpenURL("https://mydonation.focusireland.ie/donate/~my-donation");
    }
}

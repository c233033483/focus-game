using UnityEngine;

public class TipJar : MonoBehaviour, IClickable
{
    public void Interact()
    {
        Application.OpenURL("https://mydonation.focusireland.ie/donate/~my-donation");
    }
}

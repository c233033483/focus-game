using UnityEngine;

[CreateAssetMenu(fileName = "CustomerSO", menuName = "Scriptable Objects/CustomerSO")]
public class CustomerSO : ScriptableObject
{
    public string customerName;
    public Sprite customerSprite;

    public float trustLevel;

    //orders per day
}

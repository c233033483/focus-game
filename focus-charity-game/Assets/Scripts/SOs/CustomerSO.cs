using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DailyOrder
{
    public int day;
    public List<Ingredients> sandwichIngredients;
}
[System.Serializable]
public enum Ingredients
{
    Cheese,
    Ham,
    Tomato,
    Lettuce
}

[System.Serializable]
public class DailyDialogue
{
    public int day;
    public DialogueAssetSO introDialogue;
    public DialogueAssetSO correctOrderDialogue;
    public DialogueAssetSO incorrectOrderDialogue;
}


[CreateAssetMenu(fileName = "CustomerSO", menuName = "Scriptable Objects/CustomerSO")]
public class CustomerSO : ScriptableObject
{
    public string customerName;
    public Texture customerImage;

    public float trustLevel;
    
    public List<DailyOrder> dailyOrders;
    public List<DailyDialogue> dailyDialogue;
    
    public DailyOrder GetOrderForToday(int day)
    {
        return dailyOrders.Find(x => x.day == day);
    }

    public void IncreaseTrust(int amount)
    {
        trustLevel += amount;
    }
}
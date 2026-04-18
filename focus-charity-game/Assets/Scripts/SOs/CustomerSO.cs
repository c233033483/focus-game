using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DailyOrder
{
    public int day;
    public List<Ingredients> sandwichIngredients;
    public List<Ingredients> coffeeIngredients;
}
[System.Serializable]
public enum Ingredients
{
    Cheese,
    Ham,
    Tomato,
    Lettuce,
    Milk,
    Coffee,
    Water
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
    public Sprite idleExpression;
    public Sprite sadExpression;
    public Sprite happyExpression;
    public int customerIndex;

    public int trustLevel;
    public Services serviceRequired;
    
    public List<DailyOrder> dailyOrders;
    public List<DailyDialogue> dailyDialogue;

    public DailyDialogue endgameDialogue;
    
    public DailyOrder GetOrderForToday(int day)
    {
        return dailyOrders.Find(x => x.day == day);
    }

    public void IncreaseTrust(int amount)
    {
        trustLevel += amount;
    }
}
using System;
using UnityEngine;

public static class IngredientPlacingEventChannel
{
    public static event Action<string> OnIngredientPlacingEvent;

    public static void PlacingEvent(string ingredientName)
    {
        Debug.Log("Event fired: " + ingredientName);
        OnIngredientPlacingEvent?.Invoke(ingredientName);
    }
}

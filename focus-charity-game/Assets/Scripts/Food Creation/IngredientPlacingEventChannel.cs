using System;
using UnityEngine;

public static class IngredientPlacingEventChannel
{
    public static event Action<Ingredients> OnIngredientPlacingEvent;

    public static void PlacingEvent(Ingredients ingredientName)
    {
        Debug.Log("Event fired: " + ingredientName);
        OnIngredientPlacingEvent?.Invoke(ingredientName);
    }
}

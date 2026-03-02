using UnityEngine;
using System;
using System.Collections.Generic;

public static class OrderingEventChannel
{
    public static event Action<List<Ingredients>> OrderSubmitted;

    public static void OnOrderSubmitted(List<Ingredients> orderIngredients)
    {
        Debug.Log("Order submitted");
        OrderSubmitted?.Invoke(orderIngredients);
    }
}

using UnityEngine;
using System;
using System.Collections.Generic;

public static class OrderingEventChannel
{
    public static event Action<List<Ingredients>, List<Ingredients>> OrderSubmitted;

    public static void OnOrderSubmitted(List<Ingredients> sandwichOrderIngredients, List<Ingredients> coffeeOrderIngredients)
    {
        Debug.Log("Order submitted");
        OrderSubmitted?.Invoke(sandwichOrderIngredients, coffeeOrderIngredients);
    }
}

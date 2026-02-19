using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum DishType
{
    Sandwich,
    Dessert,
    Coffee
}


public class Dish : MonoBehaviour
{
    public DishType dishType;
    public List<string> currentIngredients;
    
    public TMP_Text ingredientsText;

    private void Start()
    {
        ingredientsText = FindFirstObjectByType<TMP_Text>();
    }

    private void OnEnable()
    {
        IngredientPlacingEventChannel.OnIngredientPlacingEvent += IngredientPlaced;
    }

    private void OnDisable()
    {
        IngredientPlacingEventChannel.OnIngredientPlacingEvent -= IngredientPlaced;
    }
    
    /// <summary>
    ///  This method is triggered when any ingredient is placed in a PlaceZone
    ///  Allows this script to get the placed ingredient without connecting the two scripts directly
    /// </summary>
    /// <param name="ingredient"></param>
    private void IngredientPlaced(string ingredient)
    {
        currentIngredients.Add(ingredient.ToLower());
        ingredientsText.text += ingredient + ", ";
    }
}

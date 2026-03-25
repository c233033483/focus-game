using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum DishType
{
    Sandwich,
    Dessert,
    Coffee
}


public class Dish : MonoBehaviour
{
    public DishType dishType;
    public List<Ingredients> currentIngredients;
    
    public TMP_Text ingredientsText;
    public Button submitOrderButton;

    private void OnEnable()
    {
        IngredientPlacingEventChannel.OnIngredientPlacingEvent += IngredientPlaced;
    }

    private void OnDisable()
    {
        IngredientPlacingEventChannel.OnIngredientPlacingEvent -= IngredientPlaced;
    }

    public void Start()
    {
        submitOrderButton.gameObject.SetActive(false);
    }

    /// <summary>
    ///     This method is triggered when any ingredient is placed in a PlaceZone
    ///     Allows this script to get the placed ingredient without connecting the two scripts directly
    ///     The dish gameobject can be updated here in the future
    /// </summary>
    /// <param name="ingredient"></param>
    private void IngredientPlaced(Ingredients ingredient)
    {
        submitOrderButton.gameObject.SetActive(true);
        currentIngredients.Add(ingredient);
        ingredientsText.text += ingredient + ", "; //placeholder for dish gameobject to be updated later
    }

    public void SubmitOrder()
    {
        OrderingEventChannel.OnOrderSubmitted(currentIngredients);
        currentIngredients.Clear();
        ingredientsText.text = "Current ingredients: ";
        submitOrderButton.gameObject.SetActive(false);
    }
}
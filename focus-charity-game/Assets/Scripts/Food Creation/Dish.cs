using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum DishType
{
    Sandwich,
    Coffee
}
public class Dish : MonoBehaviour
{
    public DishType dishType;
    public List<Ingredients> currentCoffeeIngredients;
    public List<Ingredients> currentSandwichIngredients;
    
    public Button submitSandwichOrderButton;
    public Button submitCoffeeOrderButton;
    
    public Button exitSandwichButton, exitCoffeeButton;

    //sandwichvisualiser
    public SandwichVisualiser sandwichVisualiserScipt;

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
        submitSandwichOrderButton.gameObject.SetActive(false);
        submitCoffeeOrderButton.gameObject.SetActive(false);
    }
    

    public void SetDishType(DishType type)
    {
        dishType = type;

        if (dishType == DishType.Sandwich)
        {
            //sandwichvisualiser
            sandwichVisualiserScipt.SandwichStart();
            exitSandwichButton.gameObject.SetActive(true);
        }
        else
        {
            exitCoffeeButton.gameObject.SetActive(true);
        }
    }
    

    /// <summary>
    ///     This method is triggered when any ingredient is placed in a PlaceZone or button pressed
    ///     Allows this script to get the placed ingredient without connecting the two scripts directly
    ///     The dish gameobject can be updated here in the future
    /// </summary>
    /// <param name="ingredient"></param>
    private void IngredientPlaced(Ingredients ingredient)
    {
        submitSandwichOrderButton.gameObject.SetActive(true);
        submitCoffeeOrderButton.gameObject.SetActive(true);
        if (dishType == DishType.Sandwich) currentSandwichIngredients.Add(ingredient);
        else if (dishType == DishType.Coffee) currentCoffeeIngredients.Add(ingredient);
        
        print("ingredient placed: " + ingredient);
        
        //sandwichvisuals
        if (ingredient == Ingredients.Tomato)
        {
            sandwichVisualiserScipt.Tomato();
        }
        else if (ingredient == Ingredients.Cheese)
        {
            sandwichVisualiserScipt.Cheese();
        }
        else if(ingredient == Ingredients.Ham)
        {
            sandwichVisualiserScipt.Ham();
        }
    }

    public void FinishSandwichCreation()
    {
        //sandwichvisualiser
        sandwichVisualiserScipt.SandwichFinish();
    }

    public void FinishCoffeeCreation()
    {
        //put lid on coffee
    }

    public void SubmitOrder()
    {
        OrderingEventChannel.OnOrderSubmitted(currentSandwichIngredients, currentCoffeeIngredients);
        
        ScrapOrder();
    }

    public void ScrapOrder()
    {
        currentSandwichIngredients.Clear();
        currentCoffeeIngredients.Clear();
        
        sandwichVisualiserScipt.SandwichReset();
        
        exitSandwichButton.gameObject.SetActive(false);
        exitCoffeeButton.gameObject.SetActive(false);
    }
}
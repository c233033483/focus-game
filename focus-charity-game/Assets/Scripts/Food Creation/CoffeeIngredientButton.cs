using UnityEngine;

public class CoffeeIngredientButton : MonoBehaviour, IClickable
{
    [SerializeField] private Ingredients ingredient;
    [SerializeField] private Animator coffeeCupAnimator;
    
    public void Interact()
    {
        switch (ingredient)
        {
            case Ingredients.Milk:
                coffeeCupAnimator?.SetTrigger("AddMilk");
                IngredientPlacingEventChannel.PlacingEvent(ingredient);
                print("milk added");
                break;
            case Ingredients.Coffee:
                coffeeCupAnimator?.SetTrigger("AddCoffee");
                IngredientPlacingEventChannel.PlacingEvent(ingredient);
                print("coffee added");
                break;
            case Ingredients.Water:
                coffeeCupAnimator?.SetTrigger("AddWater");
                IngredientPlacingEventChannel.PlacingEvent(ingredient);
                print("water added");
                break;
            default:
                break;
        }
    }
}

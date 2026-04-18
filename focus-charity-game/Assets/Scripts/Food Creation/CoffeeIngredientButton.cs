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
                coffeeCupAnimator.SetTrigger("AddMilk");
                IngredientPlacingEventChannel.PlacingEvent(ingredient);
                break;
            case Ingredients.Coffee:
                coffeeCupAnimator.SetTrigger("AddCoffee");
                IngredientPlacingEventChannel.PlacingEvent(ingredient);
                break;
            case Ingredients.Water:
                coffeeCupAnimator.SetTrigger("AddWater");
                IngredientPlacingEventChannel.PlacingEvent(ingredient);
                break;
            default:
                break;
        }
    }
}

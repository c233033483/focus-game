using UnityEngine;

public class DishSelector : MonoBehaviour
{
    public DishType dishType;
    public Dish dishScript;

    public void SetDish()
    {
        dishScript.SetDishType(dishType);
    }
}

using UnityEngine;

public class IngredientSelector : MonoBehaviour
{
    [SerializeField] private GameObject ingredient2D;

    private bool ingredientActive; //can an ingredient object be spawned?
    
    public DraggableIngredient SpawnIngredient(Vector3 spawnPosition)
    {
        if (ingredientActive) return null;

        GameObject newIngredient = Instantiate(ingredient2D, spawnPosition, Quaternion.identity);
        DraggableIngredient ingredient = newIngredient.GetComponent<DraggableIngredient>();
        ingredient.OnIngredientDestroyed += DeactivateIngredient;
        ingredientActive = true;
        return ingredient;
    }
    
    
    /* Removed to enable dragging from the bowl.
    /// <summary>
    /// This method is called when the player clicks or presses on an ingredient object.
    /// Its purpose is to activate the game object of the ingredient, which implements IDraggable
    /// </summary>
    public void Interact()
    {
        if (ingredientActive) return;
        
        Vector3 spawnPosition = spawnPoint.transform.position + (Vector3.up / 2);
        GameObject newIngredient = Instantiate(ingredient2D, spawnPosition, Quaternion.identity);
        DraggableIngredient ingredient = newIngredient.GetComponent<DraggableIngredient>();
        ingredient.OnIngredientDestroyed += DeactivateIngredient;
        ingredientActive = true;
    }*/

    /// <summary>
    /// Is called when the current instance of the ingredient is destroyed so a new ingredient can be spawned
    /// </summary>
    private void DeactivateIngredient()
    {
        ingredientActive = false;
    }
}

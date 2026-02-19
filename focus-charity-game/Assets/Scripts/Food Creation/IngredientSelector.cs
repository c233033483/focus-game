using UnityEngine;

public class IngredientSelector : MonoBehaviour, IClickable
{
    public GameObject ingredient2D;
    public GameObject spawnPoint;

    private bool ingredientActive; //can an ingredient object be spawned?
    
    /// <summary>
    /// This method is called when the player clicks or presses on an ingredient object.
    /// Its purpose is to activate the game object of the ingredient, which implements IDraggable
    /// </summary>
    public void Interact()
    {
        print("Ingredient was selected");
        
        Vector3 spawnPosition = spawnPoint.transform.position + (Vector3.up / 2);

        GameObject newIngredient = Instantiate(ingredient2D, spawnPosition, Quaternion.identity);
        DraggableIngredient ingredient = newIngredient.GetComponent<DraggableIngredient>();
        ingredientActive = true;
    }

    /// <summary>
    /// Is called by the DraggableIngredient script so that a new ingredient can be instantiated
    /// </summary>
    public void DeactivateIngredient()
    {
        ingredientActive = false;
    }
}

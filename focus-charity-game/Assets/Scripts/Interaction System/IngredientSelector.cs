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

        Instantiate(ingredient2D, spawnPosition, Quaternion.identity);
        ingredientActive = true;
    }

    /// <summary>
    /// Is called by the IngredientObject script so that a new object can be instantiated
    /// </summary>
    public void DeactivateIngredient()
    {
        ingredientActive = false;
    }
}

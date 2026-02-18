using UnityEngine;

public class IngredientSelector : MonoBehaviour, IClickable
{
    public GameObject ingredient2D;
    public GameObject spawnPoint;
    
    public void Interact()
    {
        print("Ingredient was selected");
        
        Instantiate(ingredient2D, spawnPoint.transform.position, Quaternion.identity);
    }
}

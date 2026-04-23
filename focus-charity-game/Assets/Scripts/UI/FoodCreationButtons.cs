using UnityEngine;
using UnityEngine.UI;

public class FoodCreationButtons : MonoBehaviour
{
    public static FoodCreationButtons Instance { get; private set; }

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    [SerializeField] private Button sandwichMadeButton, coffeeMadeButton, serveOrderButton;
    [SerializeField] private GameObject makeOrderButtons;
    [SerializeField] private Button exitSandwichButton, exitCoffeeButton;
    
    public void SetButtonsOnStartOrder()
    {
        makeOrderButtons.SetActive(true);
        
        serveOrderButton.interactable = false;
        serveOrderButton.gameObject.SetActive(true);
        
        exitCoffeeButton.gameObject.SetActive(true);
        exitSandwichButton.gameObject.SetActive(true);
    }

    public void EnableServing()
    {
        serveOrderButton.interactable = true;
    }

    public void OnOrderServed()
    {
        sandwichMadeButton.interactable = true;
        coffeeMadeButton.interactable = true;
        
        serveOrderButton.gameObject.SetActive(false);
        makeOrderButtons.SetActive(false);
    }
}

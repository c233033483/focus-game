using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderingSystem : MonoBehaviour
{
    public static OrderingSystem Instance { get; private set; }

    [SerializeField] private GameObject trustPanel;
    [SerializeField] private TMP_Text trustText;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private CustomerSO currentCustomer;
    private DailyOrder currentOrder;
    private TMP_Text orderText;

    private void Start()
    {
        trustPanel.SetActive(false);
    }

    private void OnEnable()
    {
        OrderingEventChannel.OrderSubmitted += ReceiveOrder;
    }

    private void OnDisable()
    {
        OrderingEventChannel.OrderSubmitted -= ReceiveOrder;
    }
    
    public void ShowOrder(int day, CustomerSO customer)
    {
        currentOrder = customer.GetOrderForToday(day);
        currentCustomer = customer;
        
        if (currentOrder == null)
        {
            Debug.LogWarning($"No order found for {customer.customerName} on day {day}");
        }
    }
    
    /// <summary>
    ///     When the order is received, the trust is increased here.
    ///     The trust UI is handled here, may need to be moved when it's not just text.
    /// </summary>
    private void ReceiveOrder(List<Ingredients> sandwichServed, List<Ingredients> coffeeServed)
    {
        bool wasOrderCorrect = (CheckOrder(currentOrder.sandwichIngredients, sandwichServed) && 
                                CheckOrder(currentOrder.coffeeIngredients, coffeeServed));

        if (wasOrderCorrect)
        {
            IndexBookManager.Instance.IncreaseCustomerTrust(currentCustomer);
        }
        
        //trustText.text = "Trust: " + currentCustomer.trustLevel;
        trustPanel.SetActive(true); //trigger animation here
        
        StartCoroutine(NextCustomerRoutine(wasOrderCorrect));
    }

    private bool CheckOrder(List<Ingredients> order, List<Ingredients> dish)
    {
        HashSet<Ingredients> orderedDish = new HashSet<Ingredients>(order);
        HashSet<Ingredients> servedDish = new HashSet<Ingredients>(dish);
        return orderedDish.SetEquals(servedDish);
    }

    private IEnumerator NextCustomerRoutine(bool wasOrderCorrect)
    {
        yield return new WaitForSeconds(1f);
        DialogueController.Instance.StartOutroDialogue(wasOrderCorrect);
        GameplayManager.Instance.UpdateExpression(wasOrderCorrect);
    }
}
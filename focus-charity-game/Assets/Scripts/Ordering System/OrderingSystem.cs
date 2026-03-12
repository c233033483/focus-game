using System;
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
        
        trustPanel.SetActive(false);
    }

    private CustomerSO currentCustomer;
    private DailyOrder currentOrder;
    private TMP_Text orderText;
    
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
    /// <param name="dishProvided"></param>
    private void ReceiveOrder(List<Ingredients> dishProvided)
    {
        bool wasOrderCorrect = CheckOrder(currentOrder.sandwichIngredients, dishProvided);

        if (wasOrderCorrect)
        {
            currentCustomer.IncreaseTrust(1);
        }

        trustPanel.SetActive(true);
        trustText.text = "Trust: " + currentCustomer.trustLevel;
        
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
    }
}
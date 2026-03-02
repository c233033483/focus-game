using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderingSystem : MonoBehaviour
{
    public static OrderingSystem Instance { get; private set; }

    public TMP_Text dialogueText;

    private bool wasOrderRight;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private CustomerSO currentCustomer;
    public DailyOrder currentOrder;
    public TMP_Text orderText;
    
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
    }
    
    private void ReceiveOrder(List<Ingredients> dishProvided)
    {
        if (CheckOrder(currentOrder.sandwichIngredients, dishProvided))
        {
            dialogueText.text = "Thank you! This is perfect";
            wasOrderRight = true;
            currentCustomer.IncreaseTrust(1);
        }
        else
        {
            dialogueText.text = "Oh... thanks...";
            wasOrderRight = false;
        }

        StartCoroutine(NextCustomer());
    }

    public bool CheckOrder(List<Ingredients> order, List<Ingredients> dish)
    {
        HashSet<Ingredients> orderedDish = new HashSet<Ingredients>();
        HashSet<Ingredients> servedDish = new HashSet<Ingredients>();
        return orderedDish.SetEquals(servedDish);
    }

    public IEnumerator NextCustomer()
    {
        yield return new WaitForSeconds(1f);
        DialogueController.Instance.StartOutroDialogue(wasOrderRight);
    }
}
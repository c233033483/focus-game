using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DailyQueue
{
    public int day;
    public List<CustomerSO> customersInOrder;
}

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance { get; private set; }

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
    
    
    private int dayIndex; // 1-5
    private int customerIndex;
    private int customersToday; // 1-however many customers in the game
    
    public DailyQueue[] dailyQueues;
    private CustomerSO currentCustomer;

    public RawImage characterPlaceholder;

    public GameObject dayStartPanel;
    public TMP_Text dayText;

    public void Start()
    {
        dayIndex = 1;
        dayText.text = "Day " + dayIndex;
    }
    
    public void DayStart()
    {
        customerIndex = 0;
        
        NextCustomer();
    }

    public void NextCustomer()
    {
        var currentQueue = dailyQueues[dayIndex - 1];
        
        // Find the next customer for the current day
        if (customerIndex < currentQueue.customersInOrder.Count)
        {
            currentCustomer = currentQueue.customersInOrder[customerIndex];
            print(currentCustomer.name);
            characterPlaceholder.texture = currentCustomer.customerImage;
            customerIndex++;
            
            OrderingSystem.Instance.ShowOrder(dayIndex, currentCustomer);
            DialogueController.Instance.StartIntroDialogue(currentCustomer, dayIndex);
        }
        else
        {
            DayEnd();
        }
    }

    public void ClearCustomers()
    {
        currentCustomer = null;
        characterPlaceholder.texture = null;
    }

    public void DayEnd()
    {
        dayStartPanel.SetActive(true);   
    }
}

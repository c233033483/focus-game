using System.Collections;
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
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    
    private int dayIndex; // 1-5
    private int customerIndex;
    
    public DailyQueue[] dailyQueues;
    private CustomerSO currentCustomer;

    public Image characterPlaceholder;

    public GameObject dayStartPanel;
    public TMP_Text dayText;

    public GameObject trustPanel;

    private void Start()
    {
        dayStartPanel.SetActive(true);
        dayText.text = "Day 1";
    }
    
    private void DayStart()
    {
        dayIndex++;
        dayText.text = $"Day {dayIndex}";
        customerIndex = 0;
        
        NextCustomer();
    }

    public void NextCustomer()
    {
        ClearCustomers();
        var currentQueue = dailyQueues[dayIndex - 1];
        
        // Find the next customer for the current day
        if (customerIndex < currentQueue.customersInOrder.Count)
        {
            StartCoroutine(NextCustomerRoutine(currentQueue));
        }
        else
        {
            DayEnd();
        }
        
        trustPanel.SetActive(false);
    }

    IEnumerator NextCustomerRoutine(DailyQueue currentQueue)
    {
        yield return new WaitForSeconds(1f);
        characterPlaceholder.gameObject.SetActive(true);
        
        currentCustomer = currentQueue.customersInOrder[customerIndex];
        characterPlaceholder.sprite = currentCustomer.idleExpression;
        customerIndex++;
            
        OrderingSystem.Instance.ShowOrder(dayIndex, currentCustomer);
        DialogueController.Instance.StartIntroDialogue(currentCustomer, dayIndex);
    }

    public void ClearCustomers()
    {
        currentCustomer = null;
        characterPlaceholder.gameObject.SetActive(false);
    }

    public void UpdateExpression(bool wasOrderCorrect)
    {
        if (wasOrderCorrect)
            characterPlaceholder.sprite = currentCustomer.happyExpression;
        else
            characterPlaceholder.sprite = currentCustomer.sadExpression;
            
    }

    private void DayEnd()
    {
        if (dayIndex == 5)
        {
            print("Game over!");
        }
        else
        {
            dayStartPanel.SetActive(true);
            DayStart();
        }
    }
}

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
    
    
    [SerializeField] private int dayIndex; // 1-5
    private int customerIndex;
    
    public DailyQueue[] dailyQueues;
    private CustomerSO currentCustomer;

    public Image characterPlaceholder;

    public GameObject dayStartPanel;
    public TMP_Text dayText;

    public GameObject trustPanel;
    
    private bool gameStarted = false;
    
    public int customersHelped;
    [SerializeField] private GameObject endGamePanelGood;
    [SerializeField] private GameObject endGamePanelBad;
    [SerializeField] private GameObject endGamePanelFailed;
    
    public HideUI hideUI;

    public GameObject tutorialPanel;

    public randomsound chimes; //AUDIO

    private void Start()
    {
        endGamePanelBad.SetActive(false);
        endGamePanelGood.SetActive(false);
        
        dayStartPanel.SetActive(true);
        dayIndex = 1;
        dayText.text = "Day 1";
        
        foreach (var customer in dailyQueues[0].customersInOrder)
            customer.ResetData();

    }
    
    public void DayStart()
    {
        if (!gameStarted)
        {
            gameStarted = true;
            hideUI.HideAllUI();
            tutorialPanel.SetActive(true);
            ClearCustomers();
        }
        else
        { 
            customerIndex = 0;
            NextCustomer();
        }
    }

    public void NextCustomer()
    {
        ClearCustomers();
        var currentQueue = dailyQueues[dayIndex - 1];
        
        // Find the next customer for the current day
        if (customerIndex < currentQueue.customersInOrder.Count)
        {
            if (!currentQueue.customersInOrder[customerIndex].activeCustomer)
            {
                customerIndex++;
                NextCustomer();
                return;
            }
            StartCoroutine(NextCustomerRoutine(currentQueue));
        }
        else if (AllCustomersFailed())
        {
            endGamePanelFailed.SetActive(true);
            return;
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
            
        OrderingSystem.Instance.ShowOrder(dayIndex, currentCustomer);
        DialogueController.Instance.StartIntroDialogue(currentCustomer, dayIndex);
        customerIndex++;

        chimes.RandomAudio();
        
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
            if (customersHelped == 4)
            {
                endGamePanelGood.SetActive(true);
            }
            else
            {
                endGamePanelBad.SetActive(true);
            }
        }
        else
        {
            dayIndex++;
            dayText.text = $"Day {dayIndex}";
            dayStartPanel.SetActive(true);
        }
    }
    
    private bool AllCustomersFailed()
    {
        foreach (var queue in dailyQueues)
        foreach (var customer in queue.customersInOrder)
            if (customer.activeCustomer) return false;
        return true;
    }
}

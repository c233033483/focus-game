using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IndexBookManager : MonoBehaviour
{
    public static IndexBookManager Instance { get; private set; }

    [SerializeField] private GameObject[] indexSlots;
    
    [SerializeField] private GameObject notificationImg;
    public bool firstCorrectOrder = false;
    public bool firstCustomerHelped = false;

    [SerializeField] private GameObject[] childHearts;
    [SerializeField] private GameObject[] manHearts;
    [SerializeField] private GameObject[] grandmaHearts;
    [SerializeField] private GameObject[] motherHearts;
    private GameObject[][] allCustomerHearts;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        allCustomerHearts = new GameObject[][] { childHearts, motherHearts, manHearts, grandmaHearts };

        foreach (var hearts in allCustomerHearts)
        {
            foreach (var h in hearts)
            {
                h.SetActive(false);
            }
        }
    }

    [SerializeField] private GameObject indexBookObject;

    private void Start()
    {
        foreach (GameObject indexSlot in indexSlots)
        {
            var button = indexSlot.transform.GetComponentInChildren<Button>();
            button.interactable = false;
        }
        indexBookObject.SetActive(false);
    }


    public void IncreaseCustomerTrust(CustomerSO customer)
    {
        Debug.Log($"Increasing trust for {customer.customerName} at index {customer.customerIndex}");
        customer.IncreaseTrust(1);
        if (!firstCorrectOrder)
        {
            firstCorrectOrder = false;
            notificationImg.SetActive(true);
        }

        int fillIndex = customer.trustLevel - 1;
        var hearts = allCustomerHearts[customer.customerIndex];
        if (fillIndex >= 0 && fillIndex < hearts.Length)
        {
            hearts[fillIndex].SetActive(true);
        }
        
        if (customer.trustLevel >= 3)
        {
            customer.activeCustomer = false;

            if (!firstCustomerHelped)
            {
                firstCustomerHelped = false;
                notificationImg.SetActive(true);
            }
        }
    }

    public void EnableHelp(CustomerSO customer)
    {
        Debug.Log($"Enabling help for {customer.customerName} at index {customer.customerIndex}");
        var button = indexSlots[customer.customerIndex].transform.GetComponentInChildren<Button>();
        button.interactable = true;
        Debug.Log("Enable Help for " + customer.name + " at button " + button);
    }
}

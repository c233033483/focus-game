using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IndexBookManager : MonoBehaviour
{
    public static IndexBookManager Instance { get; private set; }

    [SerializeField] private GameObject[] indexSlots;
    
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

    [SerializeField] private GameObject indexBookObject;

    private void Start()
    {
        foreach (GameObject indexSlot in indexSlots)
        {
            var button = indexSlot.transform.GetComponentInChildren<Button>();
            button.enabled = false;
        }
        indexBookObject.SetActive(false);
    }


    public void IncreaseCustomerTrust(CustomerSO customer)
    {
        Debug.Log($"Increasing trust for {customer.customerName} at index {customer.customerIndex}");
        customer.IncreaseTrust(1);
        var slot = indexSlots[customer.customerIndex].transform.Find("Trust Level Text").GetComponent<TMP_Text>();
        slot.text = "Trust: " + customer.trustLevel + "/4";
    }

    public void EnableHelp(CustomerSO customer)
    {
        Debug.Log($"Enabling help for {customer.customerName} at index {customer.customerIndex}");
        var button = indexSlots[customer.customerIndex].transform.GetComponentInChildren<Button>();
        button.enabled = true;
        Debug.Log("Enable Help for " + customer.name + " at button " + button);
    }
}

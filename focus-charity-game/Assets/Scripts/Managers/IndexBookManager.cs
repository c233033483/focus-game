using System;
using TMPro;
using UnityEngine;

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
        indexBookObject.SetActive(false);
    }


    public void IncreaseCustomerTrust(CustomerSO customer)
    {
        customer.IncreaseTrust(1);
        var slot = indexSlots[customer.customerIndex].transform.Find("Trust Level Text").GetComponent<TMP_Text>();
        slot.text = "Trust: " + customer.trustLevel + "/4";
    }
}

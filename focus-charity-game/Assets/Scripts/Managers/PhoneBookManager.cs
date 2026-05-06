using UnityEngine;

public class PhoneBookManager : MonoBehaviour
{
    public static PhoneBookManager Instance { get; private set; }
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
    
    void Start()
    {
        ResetVariables();
    }

    public void SetCustomer(CustomerSO customer)
    {
        currentCustomer = customer;
        Debug.Log(currentCustomer.name);
    }

    public void ServiceSelected(Services service)
    {
        if (service == currentCustomer.serviceRequired)
        {
            print("Service right");
            DialogueController.Instance.StartHelpOutroDialogue(true);
        }
        else
        {
            print("service wrong");
            DialogueController.Instance.StartHelpOutroDialogue(false);
        }
        
        Debug.Log(service + " selected for " + currentCustomer.name);
        
        GameplayManager.Instance.customersHelped++;
    }

    private void ResetVariables()
    {
        currentCustomer = null;
    }
}

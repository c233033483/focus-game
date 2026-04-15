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
            DialogueController.Instance.StartHelpOutroDialogue(true);
        }
        else
        {
            DialogueController.Instance.StartHelpOutroDialogue(false);
            GameplayManager.Instance.UpdateExpression(false);
        }
        
        Debug.Log(service + " selected for " + currentCustomer.name);
    }

    private void ResetVariables()
    {
        currentCustomer = null;
    }
}

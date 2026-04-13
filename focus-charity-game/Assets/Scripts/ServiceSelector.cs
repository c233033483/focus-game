using UnityEngine;

public enum Services
{
    Housing,
    Legal,
    StateServices,
    SleepingRoughSupport,
    ChildSupport,
    FinancialSupport,
}

public class ServiceSelector : MonoBehaviour
{
    [SerializeField] private Services service;

    public void SelectService()
    {
        PhoneBookManager.Instance.ServiceSelected(service);
    }
}

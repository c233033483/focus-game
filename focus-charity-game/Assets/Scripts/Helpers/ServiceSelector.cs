using UnityEngine;

public enum Services
{
    Housing,
    MentalHealth,
    JobCentre,
    PhysicalHealth,
    Childcare,
    BudgetingServices,
    FoodBank
}

public class ServiceSelector : MonoBehaviour
{
    [SerializeField] private Services service;

    public void SelectService()
    {
        PhoneBookManager.Instance.ServiceSelected(service);
    }
}

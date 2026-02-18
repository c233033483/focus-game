using UnityEngine;

[System.Serializable]
public class CustomersDailyOrder
{
    public CustomerSO firstCustomer;
    public CustomerSO secondCustomer;
    public CustomerSO thirdCustomer;
    public CustomerSO fourthCustomer;
}

public class GameplayManager : MonoBehaviour
{
    private int dayIndex; // 1-5
    private int customersToday; // 1-however many customers in the game
    
    public CustomersDailyOrder[] customersEachDayOrder;
    private CustomerSO currentCustomer;


    private void DayStart()
    {
        dayIndex++;
    }

    private void NextCustomer()
    {
        //currentCustomer = customers[??]; have a list for each day and which customers appear at what order, then can call from the index of the corresponding day
    }
}

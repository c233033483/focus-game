using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    private int dayIndex; // 1-5
    private int customersToday; // 1-however many customers in the game
    
    public CustomerSO[] customers;
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

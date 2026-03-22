using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public static DialogueController Instance { get; private set; }    
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
    
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private GameObject dialogueBox;

    private CustomerSO currentCustomer;
    private int dayIndex;
    private bool skipLineTriggered;

    private void Start()
    {
        dialogueBox.SetActive(false);
    }

    // Called by GameplayManager.cs
    public void StartIntroDialogue(CustomerSO customer, int day)
    {
        currentCustomer = customer;
        dayIndex = day;
        
        nameText.text = customer.name;
        DailyDialogue todaysDialogue = GetTodaysDialogue(currentCustomer);
        
        StopAllCoroutines();

        StartCoroutine(RunDialogue(todaysDialogue.introDialogue, false));
    }

    // Called by OrderingSystem.cs 
    public void StartOutroDialogue(bool wasOrderCorrect)
    {
        DailyDialogue todaysDialogue = GetTodaysDialogue(currentCustomer);
        
        DialogueAssetSO asset = wasOrderCorrect ? 
            todaysDialogue.correctOrderDialogue : todaysDialogue.incorrectOrderDialogue;
        StartCoroutine(RunDialogue(asset, true));
    }

    private DailyDialogue GetTodaysDialogue(CustomerSO customer)
    {
        return customer.dailyDialogue.Find(d => d.day == dayIndex);
    }

    IEnumerator RunDialogue(DialogueAssetSO dialogueAsset, bool nextCustomerOnEnd)
    {
        skipLineTriggered = false;
        dialogueBox.SetActive(true);
        
        for (int i = 0; i < dialogueAsset.dialogue.Length; i++)
        {
            dialogueText.text = dialogueAsset.dialogue[i];
            while (!skipLineTriggered)
                yield return null;
            skipLineTriggered = false;
        }
        
        //if last dialogue, turn on start order button
        
        dialogueBox.SetActive(false);

        if (nextCustomerOnEnd)
        {
            GameplayManager.Instance.NextCustomer();
        }
    }

    public void SkipLine()
    {
        skipLineTriggered = true;
    }
}

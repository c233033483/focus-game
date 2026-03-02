using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public static DialogueController Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
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
        
        DailyDialogue todaysDialogue = customer.dailyDialogue.Find(d => d.day == dayIndex);
        
        
        StopAllCoroutines();

        StartCoroutine(RunDialogue(todaysDialogue.introDialogue, false));
    }

    // Called by OrderingSystem.cs 
    public void StartOutroDialogue(bool wasOrderCorrect)
    {
        DailyDialogue todaysDialogue = currentCustomer.dailyDialogue.Find(d => d.day == dayIndex);
        
        if (wasOrderCorrect)
        {
            StartCoroutine(RunDialogue(todaysDialogue.correctOrderDialogue, true));
        }
        else
        {
            StartCoroutine(RunDialogue(todaysDialogue.incorrectOrderDialogue, true));
        }
    }
    

    IEnumerator RunDialogue(DialogueAssetSO dialogueAsset, bool nextCustomerOnEnd)
    {
        yield return new WaitForSeconds(0.6f);
        
        skipLineTriggered = false;
        dialogueBox.SetActive(true);
        
        for (int i = 0; i < dialogueAsset.dialogue.Length; i++)
        {
            dialogueText.text = dialogueAsset.dialogue[i];
            while (skipLineTriggered == false)
            {
                yield return null;
            }
            skipLineTriggered = false;
        }
        
        dialogueBox.SetActive(false);
        
        if (nextCustomerOnEnd)
            GameplayManager.Instance.NextCustomer();
        else ()
            
    }

    public void SkipLine()
    {
        skipLineTriggered = true;
    }
}

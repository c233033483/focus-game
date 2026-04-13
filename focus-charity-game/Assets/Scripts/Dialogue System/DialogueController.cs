using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private Button starOrderButton;

    private CustomerSO currentCustomer;
    private int dayIndex;
    private bool skipLineTriggered;

    private void Start()
    {
        dialogueBox.SetActive(false);
        starOrderButton.gameObject.SetActive(false);
    }

    // Called by GameplayManager.cs
    public void StartIntroDialogue(CustomerSO customer, int day)
    {
        currentCustomer = customer;
        dayIndex = day;
        nameText.text = customer.customerName;
        DailyDialogue todaysDialogue = GetTodaysDialogue(currentCustomer);
        StopAllCoroutines();
        StartCoroutine(RunDialogue(todaysDialogue.introDialogue));
    }

    // Called by OrderingSystem.cs 
    public void StartOutroDialogue(bool wasOrderCorrect)
    {
        DailyDialogue todaysDialogue = GetTodaysDialogue(currentCustomer);
        DialogueAssetSO asset = wasOrderCorrect ? 
            todaysDialogue.correctOrderDialogue : todaysDialogue.incorrectOrderDialogue;
        
        StartCoroutine(RunDialogue(asset, () =>
        {
            if (currentCustomer.trustLevel >= 4)
            {
                PhoneBookManager.Instance.SetCustomer(currentCustomer);
                IndexBookManager.Instance.EnableHelp(currentCustomer);
                StartHelpDialogue();
            }
            else
            {
                GameplayManager.Instance.NextCustomer();
            }
        }));
    }

    private void StartHelpDialogue()
    {
        DailyDialogue todaysDialogue = currentCustomer.endgameDialogue;
        StartCoroutine(RunDialogue(todaysDialogue.introDialogue));
    }

    public void StartHelpOutroDialogue(bool wasServiceCorrect)
    {
        DailyDialogue todaysDialogue = currentCustomer.endgameDialogue;
        DialogueAssetSO asset = wasServiceCorrect ? 
            todaysDialogue.correctOrderDialogue : todaysDialogue.incorrectOrderDialogue;
        StartCoroutine(RunDialogue(asset, () => GameplayManager.Instance.NextCustomer()));
    }

    private DailyDialogue GetTodaysDialogue(CustomerSO customer)
    {
        return customer.dailyDialogue.Find(d => d.day == dayIndex);
    }

    private IEnumerator RunDialogue(DialogueAssetSO dialogueAsset, Action onEnd = null)
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

        if (dialogueAsset.unlockOrderButton)
            starOrderButton.gameObject.SetActive(true);
            
        dialogueBox.SetActive(false);
        onEnd?.Invoke();
    }

    public void SkipLine()
    {
        skipLineTriggered = true;
    }

    public void TurnOffStartOrderButton()
    {
        starOrderButton.gameObject.SetActive(false);
    }
}
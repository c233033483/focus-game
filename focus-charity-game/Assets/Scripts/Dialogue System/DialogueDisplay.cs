using TMPro;
using UnityEngine;

public class DialogueDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] GameObject dialogueBox;

    public void DisplayDialogue(string dialogue, string name)
    {
        nameText.text = name;
        dialogueText.text = dialogue;
        dialogueBox.SetActive(true);
    }

    public void HideDialogueBox()
    {
        nameText.text = "";
        dialogueText.text = "";
        dialogueBox.SetActive(false);
    }
}

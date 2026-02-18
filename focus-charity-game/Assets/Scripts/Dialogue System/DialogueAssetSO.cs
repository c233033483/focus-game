using UnityEngine;

[CreateAssetMenu(fileName = "DialogueAssetSO", menuName = "Scriptable Objects/DialogueAssetSO")]
public class DialogueAssetSO : ScriptableObject
{
    [TextArea]
    public string[] dialogue;
}

using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue System/Dialogue")]
public class DialogueData : ScriptableObject
{
    [System.Serializable]
    public class DialogueLine
    {
        public string characterName;
        [TextArea(3, 10)]
        public string line;
        public TMP_FontAsset font;
    }

    public DialogueLine[] dialogueLines;
}

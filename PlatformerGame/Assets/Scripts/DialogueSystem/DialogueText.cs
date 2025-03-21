using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueText : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI characterName;
    public GameObject dialogueUI;
    public float speedChar = 0.05f;
    public float detectionRange = 3f;
    public KeyCode dialogueKey = KeyCode.E;

    private DialogueData currentDialogue;
    private Transform player;
    private int index;
    private bool isTalking = false;
    private DialogueTrigger currentSpeaker;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        dialogueUI.SetActive(false); // Cache le dialogue au début
    }

    void Update()
    {
        if (isTalking)
        {
            if (Input.GetKeyDown(dialogueKey))
            {
                if (textComponent.text == currentDialogue.dialogueLines[index].line)
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    textComponent.text = currentDialogue.dialogueLines[index].line;
                }
            }
        }
        else
        {
            CheckForSpeaker();
        }
    }

    void CheckForSpeaker()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.position, detectionRange);
        foreach (Collider2D collider in colliders)
        {
            DialogueTrigger trigger = collider.GetComponent<DialogueTrigger>();
            if (trigger != null)
            {
                currentSpeaker = trigger;

                if (Input.GetKeyDown(dialogueKey))
                {
                    StartDialogue(trigger.dialogueData);
                }
                return;
            }
        }
        currentSpeaker = null;
    }

    void StartDialogue(DialogueData dialogueData)
    {
        currentDialogue = dialogueData;
        characterName.text = dialogueData.dialogueLines[index].characterName;
        textComponent.text = "";
        index = 0;
        isTalking = true;
        dialogueUI.SetActive(true);
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        if (currentDialogue.dialogueLines[index].font != null)
        {
            textComponent.font = currentDialogue.dialogueLines[index].font;
            characterName.font = currentDialogue.dialogueLines[index].font;
        }

        characterName.text = currentDialogue.dialogueLines[index].characterName;


        foreach (char c in currentDialogue.dialogueLines[index].line.ToCharArray())
        {
            textComponent.text += c;
            AudioManager.instance.PlaySFX(1);
            yield return new WaitForSeconds(speedChar);
        }
    }

    void NextLine()
    {
        if (index < currentDialogue.dialogueLines.Length - 1)
        {
            index++;
            textComponent.text = "";
            characterName.text = "";
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialogueUI.SetActive(false);
        isTalking = false;
    }
}

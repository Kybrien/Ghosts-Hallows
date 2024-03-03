using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private List<DialogueString> dialogueStrings = new List<DialogueString>(); //Dialogue to trigger
    [SerializeField] private Transform NPC_Transform; //Transform of the NPC

    /*private bool hasSpoken = false; //If the player has already spoken to the NPC*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<DialogueManager>().DialogueStart(dialogueStrings, NPC_Transform);
/*            hasSpoken = true;*/
        }
    }
}

[System.Serializable]
public class DialogueString
{
    public string text; //Text of the npc
    public bool isEnd; //Si la ligne est la dernière de la conversation

    [Header("Branch")]
    public bool isQuestion; //Si la ligne est une question
    public string answer1; //Réponse 1
    public string answer2; //Réponse 2

    public int option1_IndexJump; //Dialogue suivant si réponse 1
    public int option2_IndexJump; //Dialogue suivant si réponse 1

    [Header("Triggered Event")]
    public UnityEvent startDialogueEvent; //Event triggered when the dialogue starts
    public UnityEvent endDialogueEvent; //Event triggered when the dialogue starts

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueParent; //Dialogue box
    [SerializeField] private TMP_Text dialogueText; //Text of the dialogue
    [SerializeField] private Button option1Button; //Button for the first answer
    [SerializeField] private Button option2Button; //Button for the second answer

    [SerializeField] private float typingSpeed = 0.05f; //Speed of the text typing
    [SerializeField] private float turnSpeed = 2f; //Speed of turning the NPC

    private List<DialogueString> dialogueList;

    [Header("Player")]
    //On cree une reference au joueu pour l'empecher de bouger
    [SerializeField] private GameObject player;
    private Transform playerCamera;

    private int currentDialogueIndex = 0; //Index of the current dialogue


    private void Start()
    {
        dialogueParent.SetActive(false);
        playerCamera = Camera.main.transform;
    }

    public void DialogueStart(List<DialogueString> textToPrint, Transform NPC)
    {
        dialogueParent.SetActive(true);
        //on desactive le joueur
        player.GetComponent<PlayerController>().enabled = false;

        //on desactive le curseur et on laisse le joueur 1 selectionner une reponse a la souris
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //on desactive le joueur 2 et on le laisse selectionner une reponse avec son joystick droit 
        //mettre le code ici

        StartCoroutine(TurnCameraTowardsNPC(NPC));

        dialogueList = textToPrint;
        currentDialogueIndex = 0;

        DisableButtons();

        StartCoroutine(PrintDialogue());
    }

    private void DisableButtons()
    {
        /*option1Button.gameObject.SetActive(false);
        option2Button.gameObject.SetActive(false);*/
        
        option1Button.interactable = false;
        option2Button.interactable = false;

        option1Button.GetComponentInChildren<TMP_Text>().text = "";
        option2Button.GetComponentInChildren<TMP_Text>().text = "";
        
    }

    private IEnumerator TurnCameraTowardsNPC(Transform NPC)
    {
        Quaternion startRotation = playerCamera.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(NPC.position - playerCamera.position);
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            playerCamera.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime);
            elapsedTime += Time.deltaTime * turnSpeed;
            yield return null;
        }

        playerCamera.rotation = targetRotation;

        //IA
        /*Quaternion targetRotation = Quaternion.LookRotation(NPC.position - playerCamera.position);
        while (Quaternion.Angle(playerCamera.rotation, targetRotation) > 0.1f)
        {
            playerCamera.rotation = Quaternion.Slerp(playerCamera.rotation, targetRotation, turnSpeed * Time.deltaTime);
            yield return null;
        }*/
    }

    private bool optionSelected = false;

    private IEnumerator PrintDialogue()
    {
        while(currentDialogueIndex < dialogueList.Count)
        {
            DialogueString line = dialogueList[currentDialogueIndex];
            line.startDialogueEvent?.Invoke();

            if (line.isQuestion)
            {
                yield return StartCoroutine(TypeText(line.text));

                option1Button.interactable = true;
                option2Button.interactable = true;

                option1Button.GetComponentInChildren<TMP_Text>().text = line.answer1;
                option2Button.GetComponentInChildren<TMP_Text>().text = line.answer2;

                option1Button.onClick.AddListener(() => HandleOptionSelected(line.option1_IndexJump));
                option2Button.onClick.AddListener(() => HandleOptionSelected(line.option2_IndexJump));

                yield return new WaitUntil(() => optionSelected);
            }
            else
            {
                yield return StartCoroutine(TypeText(line.text));
            }

            line.endDialogueEvent?.Invoke();

            optionSelected = false;
        }

        DialogueStop();
    }

    private void HandleOptionSelected(int indexJump)
    {
        optionSelected = true;
        DisableButtons();

        currentDialogueIndex = indexJump;
    }

    private IEnumerator TypeText(string text)
    {
        dialogueText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        if (dialogueList[currentDialogueIndex].isQuestion)
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }

        if (dialogueList[currentDialogueIndex].isEnd)
        {
            DialogueStop();
        }

        currentDialogueIndex++;
    }

    private void DialogueStop()
    {
        StopAllCoroutines();
        dialogueText.text = "";
        dialogueParent.SetActive(false);

        //on reactive le joueur
        player.GetComponent<PlayerController>().enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }
}

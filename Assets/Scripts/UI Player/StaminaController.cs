using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StaminaController : MonoBehaviour
{
    [Header("Stamina Main Parameters")]
    public float playerStamina = 100.0f;
    [SerializeField] private float maxStamina = 100.0f;
    [SerializeField] private float jumpCost = 20.0f;
    [HideInInspector] public bool hasRegenerated = true;
    [HideInInspector] public bool isSprinting = false;

    [Header("Stamina Regeneration Parameters")]
    [Range(0, 50)] [SerializeField] private float staminaDrain = 0.5f;
    [Range(0, 50)][SerializeField] private float staminaRegeneration = 0.5f;

    [Header("Stamina Speed Parameters")]
    [SerializeField] private int slowerRunSpeed = 4;
    [SerializeField] private int normalRunSpeed = 8;

    [Header("Stamina UI Elements")]
    [SerializeField] private Image staminaProgressUI = null;
    [SerializeField] private CanvasGroup sliderCanvaGroup = null;

    private PlayerController playerController = null;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (!isSprinting)
        {
            if(playerStamina <= maxStamina - 0.01)
            {
                playerStamina += staminaRegeneration * Time.deltaTime;
                UpdateStamina(1);

                if (playerStamina >= maxStamina)
                {
                    //set to normal speed
                    sliderCanvaGroup.alpha = 0;
                    hasRegenerated = true;
                }
            }
        }
    }

    public void Sprinting()
    {
        if(hasRegenerated)
        {
            isSprinting = true;
            playerStamina -= staminaDrain * Time.deltaTime;
            UpdateStamina(1);

            if (playerStamina <= 0)
            {
                hasRegenerated = false;
                //Slow the player
                sliderCanvaGroup.alpha = 0;
            }
        }
    }


    public void StaminaJump()
    {
        if(playerStamina >= (maxStamina * jumpCost / maxStamina))
        {
            playerStamina -= jumpCost;

            UpdateStamina(1);
        }
    }

    void UpdateStamina (int value)
    {
        staminaProgressUI.fillAmount = playerStamina / maxStamina;

        if (value == 0)
        {
            sliderCanvaGroup.alpha = 0;
        }
        else
        {
            sliderCanvaGroup.alpha = 1;
        }
    }


}

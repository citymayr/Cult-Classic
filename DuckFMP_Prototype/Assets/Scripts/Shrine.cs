using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shrine : MonoBehaviour
{
    [Header("Script References")]
    [SerializeField] PlayerMovement playerMovementScript;
    [SerializeField] PlayerStats playerStats;

    [Header("UI Elements")]
    [SerializeField] TextMeshProUGUI shrinePrompt;
    [SerializeField] GameObject shrineDescription;

    public bool shrineUsed;

    private void Awake()
    {
        shrinePrompt.enabled = false;
        shrineDescription.SetActive(false);
    }

    //************************************************//
    //*              SHRINE INTERACTIONS             *//
    //************************************************//
    private void Update()
    {
        //Check if the shrine has been used before allowing the player to interact with it,
        //When interacted with, load up shrine HUD and stop the player from moving
        if (Input.GetKeyDown(KeyCode.E) & !shrineUsed)
        {
            shrinePrompt.enabled = false;

            shrineDescription.SetActive(true);

            playerMovementScript.canMove = false;
        }
    }

    //Upon clicking the Use Shrine button, get rid of the HUD, make the shrine unusable again and allow the player to move again
    public void UseShrine()
    {
        shrineDescription.SetActive(false);

        playerMovementScript.canMove = true;

        shrineUsed = true;
    }

    //Exit shrine screen with no effects applied + can move again
    public void DoNotUseShrine()
    {
        shrineDescription.SetActive(false);

        playerMovementScript.canMove = true;
    }

    //************************************************//
    //*          CHECK FOR PLAYER COLLISION          *//
    //************************************************//

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player" && !shrineUsed)
        {
            shrinePrompt.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            shrinePrompt.enabled = false;
        }
    }
}

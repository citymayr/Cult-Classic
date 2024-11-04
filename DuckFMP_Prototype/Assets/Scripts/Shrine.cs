using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shrine : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] TextMeshProUGUI shrineToggle;
    [SerializeField] GameObject shrineDescriptors;

    [Space]
    [Header("Scripts")]
    [SerializeField] PlayerStats playerStats;

    private bool shrineUsed;

    private void Awake()
    {
        shrineToggle.enabled = false;
        shrineDescriptors.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            shrineToggle.enabled = false;
            shrineDescriptors.SetActive(true);
        }

        if (shrineUsed)
        {
            while (playerStats.playerHealth < playerStats.maxHealth)
            {
                playerStats.playerHealth += Time.deltaTime;
                playerStats.healthBar.fillAmount = Mathf.Lerp(playerStats.playerHealth, playerStats.maxHealth, Time.deltaTime / 3);
            }
        }
    }

    public void UseShrine()
    {
        shrineDescriptors.SetActive(false);
        shrineUsed = true;
    }
    public void DoNotUseShrine()
    {
        shrineDescriptors.SetActive(false);
    }

    //************************************************//
    //*          CHECK FOR PLAYER COLLISION          *//
    //************************************************//
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            shrineToggle.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            shrineToggle.enabled = false;
        }
    }
}

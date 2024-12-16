using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [Header("Script References")]
    [SerializeField] Shrine shrineScript;

    [Space]
    [Header("UI Elements")]
    public Image healthBarF;
    public Image healthBarB;
    public Image inspoBarF;
    public Image inspoBarB;
    [Space]
    public float playerHealth;
    public float maxHealth = 100f;
    [Space]
    public float playerInspo;
    public float maxInspo = 100f;

    private float lerpTimer;
    [SerializeField] private float chipSpeed;
    private float chipDelay;

    private void Update()
    {
        UpdateHealthUI();
        playerHealth = Mathf.Clamp(playerHealth, 0, maxHealth);

        UpdateInspoUI();

        if (shrineScript.shrineUsed)
            ShrineUsed();

        if (Input.GetKeyDown(KeyCode.G))
        {
            playerHealth += 0.05f;
            lerpTimer = 0f;
            chipDelay = 0f;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            playerHealth -= 0.05f;
            lerpTimer = 0f;
            chipDelay = 0f;
        }
    }
    private void UpdateHealthUI()
    {
        float fillFront = healthBarF.fillAmount;
        float fillBack = healthBarB.fillAmount;
        float healthFraction = playerHealth / maxHealth;
        chipDelay += Time.deltaTime;

        if (fillBack > healthFraction)
        {
            healthBarF.fillAmount = healthFraction;
            if (chipDelay > .5f)
            {
                //healthBarB.color = new Color(86, 0, 51, 255);
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / chipSpeed;
                healthBarB.fillAmount = Mathf.Lerp(fillBack, healthFraction, percentComplete);
            }
        }
        if (fillFront < healthFraction)
        {
            healthBarB.fillAmount = healthFraction;
            if (chipDelay > .5f)
            {
                //healthBarB.color = new Color(86, 118, 51, 255);
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / (chipSpeed / healthFraction);
                healthBarF.fillAmount = Mathf.Lerp(fillFront, healthFraction, percentComplete);
            }
        }
    }

    private void UpdateInspoUI()
    {
        float fillFront = inspoBarF.fillAmount;
        float fillBack = inspoBarB.fillAmount;
        float inspoFraction = playerInspo / maxInspo;
        chipDelay += Time.deltaTime;

        if (fillBack > inspoFraction)
        {
            inspoBarF.fillAmount = inspoFraction;
            if (chipDelay > .5f)
            {
                //inspoBarB.color = Color.red;
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / chipSpeed;
                inspoBarB.fillAmount = Mathf.Lerp(fillBack, inspoFraction, percentComplete);
            }
        }

        if (fillFront < inspoFraction)
        {
            healthBarB.fillAmount = inspoFraction;
            if (chipDelay > .5f)
            {
                //healthBarB.color = Color.green;
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / chipSpeed;
                healthBarF.fillAmount = Mathf.Lerp(fillFront, inspoFraction, percentComplete);
            }
        }
    }

    private void ShrineUsed()
    {
        playerHealth = 1f;
        lerpTimer = 0f;
        chipDelay = 0f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [Header("UI Elements")]
    public Image healthBar;
    public Image inspoBar;

    public float playerHealth;
    public float maxHealth;

    public float playerInspo;
    public float maxInspo;

    private void FixedUpdate()
    {
        //healthBar.fillAmount = playerHealth;
    }
}

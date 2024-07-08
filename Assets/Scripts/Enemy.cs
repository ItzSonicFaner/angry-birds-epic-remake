using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public int currentHealth;
    public int maxHealth;

    [Header("StatsDisplay")]
    public Image healthFill;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        healthFill.fillAmount = (float) currentHealth / (float) maxHealth;
    }
}

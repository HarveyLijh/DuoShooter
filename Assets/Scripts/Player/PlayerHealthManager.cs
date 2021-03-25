﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    [SerializeField] private float playerHealth;
    private float currentHealth;

    [SerializeField] private float hurtFlashLength;
    private float flashCountDown;

    private Renderer rend;
    private Color origColor;

    [SerializeField]
    private HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = playerHealth;
        rend = GetComponent<Renderer>();
        origColor = rend.material.GetColor("_Color");
        healthBar.SetMaxHealth(playerHealth);
    }

    // Update is called once per frame
    void Update()
    {

        if (flashCountDown <= 0)
        {
            rend.material.SetColor("_Color", origColor);
        }
        else
        {
            flashCountDown -= Time.deltaTime;
        }
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void HurtPlayer(float damageAmount)
    {
        flashCountDown = hurtFlashLength;
        rend.material.SetColor("_Color", Color.red);
        currentHealth -= damageAmount;
        healthBar.SetHealth(currentHealth);
    }
}

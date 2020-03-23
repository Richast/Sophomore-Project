﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class playerHealth : MonoBehaviour
{
    [SerializeField]
    private float invincibilityDurationSeconds;
    [SerializeField]
    private float delayBetweenInvincibilityFlashes;

    public int health;
    public int maxHealth;
    public int EnemyDamage;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    private CharacterController2D player;
    private bool isInvincible = false;

    void Start()
    {
        health = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>();
    }
 
    void Update()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        if (health <= 0)
        {
            WeaponController weapon = player.GetComponent<WeaponController>();
            weapon.RefillAmmo(100);
            transform.position = player.respawnPoint;
            health = maxHealth;
        }
        
    }

    private IEnumerator coroutine;

    private void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.tag == "Enemy" || obj.tag == "Spikes")
        {
            damagePlayer(1);
        }
        
    }

    public void damagePlayer(int dmg)
    {
        if (isInvincible) return;

        health -= dmg;

        StartCoroutine(BecomeTemporarilyInvincible());
    }

    private IEnumerator BecomeTemporarilyInvincible()
    {
        Debug.Log("Player turned invincible!");
        isInvincible = true;

        // Flash on and off for roughly invincibilityDurationSeconds seconds
        for (float i = 0; i < invincibilityDurationSeconds; i += delayBetweenInvincibilityFlashes)
        {
            player.GetComponent<SpriteRenderer>().enabled = !(player.GetComponent<SpriteRenderer>().enabled);
            yield return new WaitForSeconds(delayBetweenInvincibilityFlashes);
        }

        player.GetComponent<SpriteRenderer>().enabled = true;

        Debug.Log("Player is no longer invincible!");
        isInvincible = false;
    }


}

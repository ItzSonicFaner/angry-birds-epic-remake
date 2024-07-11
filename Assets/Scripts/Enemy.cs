using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public int currentHealth;
    public int maxHealth;
    public int damage;
    public int turnsLeft;
    public int maxTurnsLeft;
    public bool needToWaitForAttack;

    [Header("StatsDisplay")]
    public Image healthFill;
    private List<Player> Players = new List<Player>();
    private List<Player> PlayersCantAttack = new List<Player>();
    private bool startedAttack = false;

    void Awake()
    {
        currentHealth = maxHealth;
        turnsLeft = maxTurnsLeft;
    }

    void Update()
    {
        healthFill.fillAmount = (float) currentHealth / (float) maxHealth;

        GameObject[] playersArray = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if(playersArray.Length != Players.Count)
            {
                Players.Add(player.GetComponent<Player>());
            }
        }

        for (int i = 0; i < Players.Count; i++)
        {
            if (Players[i] != null && !Players[i].GetComponent<Player>().canAttack && PlayersCantAttack.Count < Players.Count && !PlayersCantAttack.Contains(Players[i]))
            {
                PlayersCantAttack.Add(Players[i]);
            }
        }

        if (PlayersCantAttack.Count == Players.Count && !startedAttack)
        {
            startedAttack = true;
            Invoke("Attack", 2.0f);
        }
    }

    void Attack()
    {
        if (needToWaitForAttack)
        {
            if(turnsLeft > 0)
            {
                turnsLeft--;

                for (int a = 0; a < PlayersCantAttack.Count; a++)
                {
                    PlayersCantAttack[a].canAttack = true;

                    PlayersCantAttack[a].circle.SetActive(true);
                    PlayersCantAttack[a].GetComponent<LineRenderer>().enabled = true;
                }

                if(turnsLeft == 0)
                {
                    turnsLeft = maxTurnsLeft;

                    startAttack();
                    return;
                }

                PlayersCantAttack.Clear();

                resetSAttack();
            }
        }
        else if (!needToWaitForAttack)
        {
            startAttack();
        }
    }

    void startAttack()
    {
        int i = UnityEngine.Random.Range(0, PlayersCantAttack.Count);

        for (int a = 0; a < PlayersCantAttack.Count; a++)
        {
            PlayersCantAttack[a].canAttack = true;

            PlayersCantAttack[a].circle.SetActive(true);
            PlayersCantAttack[a].GetComponent<LineRenderer>().enabled = true;
        }

        for (int a = 0; a < PlayersCantAttack.Count; a++)
        {
            for (int b = 0; b < PlayersCantAttack[a].skills.Length; b++)
            {
                if (PlayersCantAttack[a].skills[b].isCurrentSkill && PlayersCantAttack[a].skills[b].currentryUsing
                    && PlayersCantAttack[a].skills[b].type == skillsArray.skillType.shield)
                {
                    float attackDmg = (float)damage - ((float)damage * ((float)PlayersCantAttack[a].skills[b].defendPercentage / 100.0f));

                    PlayersCantAttack[i].currentHealth -= (int)attackDmg;

                    PlayersCantAttack.Clear();

                    resetSAttack();
                    break;
                }
                else if (PlayersCantAttack[a].skills[b].isCurrentSkill && !PlayersCantAttack[a].skills[b].currentryUsing)
                {
                    PlayersCantAttack[i].currentHealth -= damage;

                    Players.Clear();
                    PlayersCantAttack.Clear();

                    resetSAttack();
                    break;
                }
            }
        }
    }

    void resetSAttack()
    {
        startedAttack = false;
    }
}

using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public int damage;
    public int currentHealth;
    public int maxHealth;
    public int amountOfEnemiesToAttack;
    public int reflectionPer;
    public bool hasShield;
    public bool massiveAttack;

    [Header("Enemies")]
    public List<GameObject> enemiesArray = new List<GameObject>();

    [Header("Player")]
    public GameObject circle;
    public GameObject selectingCircle;
    public Image healthFill;

    GameObject currentCharacter;
    LineRenderer lineRenderer;
    bool isDragging = false;
    bool isCompleted = false;

    void Awake()
    {
        currentHealth = maxHealth;

        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        currentHealth = Math.Clamp(currentHealth, 0, maxHealth);
        reflectionPer = Math.Clamp(reflectionPer, 0, 100);

        healthFill.fillAmount = (float) currentHealth / (float) maxHealth;

        if (isDragging)
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0.0f;

            bool enemyHit = false;

            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                {
                    Vector2 enemyVec2 = new Vector2(enemy.transform.localPosition.x, enemy.transform.localPosition.y);
                    Vector2 playerVec2 = new Vector2(transform.localPosition.x, transform.localPosition.y);
                    Vector2 enemyScale = new Vector2(enemy.transform.localScale.x, enemy.transform.localScale.y);
                    Vector2 playerScale = new Vector2(transform.localScale.x, transform.localScale.y);

                    lineRenderer.SetPosition(0, transform.localPosition);

                    Collider2D collider = enemy.GetComponent<Collider2D>();
                    Collider2D colliderPlr = GetComponent<Collider2D>();
                    Collider2D colliderAPlr = player.GetComponent<Collider2D>();
                    if (collider != null && collider.OverlapPoint(mouseWorldPosition))
                    {
                        enemyHit = true;

                        circle.transform.localPosition = new Vector3(enemy.transform.localPosition.x, enemy.transform.localPosition.y - 0.8f, 0.0f);
                        lineRenderer.SetPosition(1, new Vector3(enemy.transform.localPosition.x, enemy.transform.localPosition.y - 0.8f, 0.0f));
                        lineRenderer.startColor = new Color32(210, 5, 5, 255);
                        lineRenderer.endColor = new Color32(210, 5, 5, 255);

                        selectingCircle.SetActive(true);

                        Color32 redClr = new Color32(210, 5, 5, 255);

                        circle.GetComponent<Renderer>().material.SetColor("_TargetColor", redClr);
                        selectingCircle.GetComponent<Renderer>().material.SetColor("_TargetColor", redClr);

                        if (!enemiesArray.Contains(enemy))
                        {
                            enemiesArray.Add(enemy);

                            if (massiveAttack && amountOfEnemiesToAttack > 0)
                            {
                                if (enemiesArray.Count < amountOfEnemiesToAttack)
                                {
                                    List<GameObject> curEnemies = new List<GameObject>();

                                    foreach (GameObject curEnemy in GameObject.FindGameObjectsWithTag("Enemy"))
                                    {
                                        if (enemy != curEnemy)
                                        {
                                            curEnemies.Add(curEnemy);
                                        }
                                    }

                                    int id = UnityEngine.Random.Range(0, curEnemies.Count);

                                    enemiesArray.Add(curEnemies[id].gameObject);
                                }
                            }
                            else if (massiveAttack && amountOfEnemiesToAttack == 0)
                            {
                                foreach (GameObject curEnemy in GameObject.FindGameObjectsWithTag("Enemy"))
                                {
                                    if (enemy != curEnemy)
                                    {
                                        enemiesArray.Add(curEnemy);
                                    }
                                }
                            }
                        }
                        break;
                    }
                    else if (colliderPlr != null && colliderPlr.OverlapPoint(mouseWorldPosition))
                    {
                        circle.transform.localPosition = new Vector3(transform.localPosition.x + 0.05f, transform.localPosition.y - 0.55f, 0.0f);
                        lineRenderer.SetPosition(1, new Vector3(transform.localPosition.x + 0.05f, transform.localPosition.y - 0.55f, 0.0f));
                        lineRenderer.startColor = new Color32(110, 220, 0, 255);
                        lineRenderer.endColor = new Color32(110, 220, 0, 255);

                        selectingCircle.SetActive(false);

                        Color32 greenClr = new Color32(110, 220, 0, 255);

                        circle.GetComponent<Renderer>().material.SetColor("_TargetColor", greenClr);
                        selectingCircle.GetComponent<Renderer>().material.SetColor("_TargetColor", greenClr);
                    }
                    else if (colliderAPlr != null && colliderAPlr.OverlapPoint(mouseWorldPosition) && player != gameObject)
                    {
                        circle.transform.localPosition = new Vector3(transform.localPosition.x + 0.05f, transform.localPosition.y - 0.55f, 0.0f);
                        lineRenderer.SetPosition(1, new Vector3(transform.localPosition.x + 0.05f, transform.localPosition.y - 0.55f, 0.0f));
                        lineRenderer.startColor = new Color32(110, 220, 0, 255);
                        lineRenderer.endColor = new Color32(110, 220, 0, 255);

                        selectingCircle.SetActive(true);

                        Color32 greenClr = new Color32(110, 220, 0, 255);

                        circle.GetComponent<Renderer>().material.SetColor("_TargetColor", greenClr);
                        selectingCircle.GetComponent<Renderer>().material.SetColor("_TargetColor", greenClr);
                    }
                    else
                    {
                        circle.transform.localPosition = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, 0.0f);

                        Color32 lBlueClr = new Color32(68, 187, 255, 255);

                        lineRenderer.startColor = new Color32(68, 187, 255, 255);
                        lineRenderer.endColor = new Color32(68, 187, 255, 255);
                        circle.GetComponent<SpriteRenderer>().color = new Color32(68, 187, 255, 255);

                        selectingCircle.SetActive(true);

                        circle.GetComponent<Renderer>().material.SetColor("_TargetColor", lBlueClr);
                        selectingCircle.GetComponent<Renderer>().material.SetColor("_TargetColor", lBlueClr);

                        lineRenderer.SetPosition(1, new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, 0.0f));
                    }
                }
            }

            if (enemyHit)
            {
                isCompleted = true;
            }
            else
            {
                isCompleted = false;
                enemiesArray.Clear();
            }
        }
    }

    void OnMouseDown()
    {
        if (gameObject.CompareTag("Player"))
        {
            currentCharacter = gameObject;
            isDragging = true;
        }
    }

    void OnMouseUpAsButton()
    {
        hasShield = true;
    }

    void OnMouseUp()
    {
        if (currentCharacter != null && enemiesArray.Count > 0)
        {
            Attack();
        }

        isCompleted = false;
        currentCharacter = null;
        isDragging = false;

        selectingCircle.SetActive(false );

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0.0f;

        circle.transform.localPosition = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, 0.0f);

        Color32 lBlueClr = new Color32(68, 187, 255, 255);

        lineRenderer.startColor = new Color32(68, 187, 255, 255);
        lineRenderer.endColor = new Color32(68, 187, 255, 255);
        circle.GetComponent<SpriteRenderer>().color = new Color32(68, 187, 255, 255);

        circle.GetComponent<Renderer>().material.SetColor("_TargetColor", lBlueClr);

        lineRenderer.SetPosition(1, new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, 0.0f));

        enemiesArray.Clear();

        lineRenderer.SetPosition(1, new Vector3(transform.position.x, transform.position.y, 0.0f));
        circle.transform.localPosition = new Vector3(-2.4f, -1.25f, 0.0f);
    }

    void Attack()
    {
        for(int i = 0; i < enemiesArray.Count; i++)
        {
            enemiesArray[i].GetComponent<Enemy>().currentHealth -= damage;
        }
    }
}
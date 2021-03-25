using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    [SerializeField] private float health;
    private float currentHealth;

    [SerializeField] private float hurtFlashLength;
    private float flashCountDown;

    [SerializeField]
    private HealthBar healthBar;

    private Renderer rend;
    private List<Renderer> rendChildren;
    private int numOfChildren;
    private Color origColor;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        rend = GetComponent<Renderer>();
        rendChildren = new List<Renderer>();
        for (int i = 0; i < transform.childCount; i++)
        {

            GameObject child = transform.GetChild(i).gameObject;
            if (child.tag != "healthBar")
            {
                rendChildren.Add(child.GetComponent<Renderer>());
                numOfChildren++;
            }
        }
        origColor = rend.material.GetColor("_Color");
        healthBar.SetMaxHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        if (flashCountDown <= 0)
        {
            rend.material.SetColor("_Color", origColor);
            //set color for hands and other children parts
            for (int i = 0; i < numOfChildren; i++)
            {
                rendChildren[i].material.SetColor("_Color", origColor);
            }
        }
        else
        {
            flashCountDown -= Time.deltaTime;
        }
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

    }

    public void HurtEnemy(float damage)
    {
        flashCountDown = hurtFlashLength;
        rend.material.SetColor("_Color", Color.red);
        //set color for hands and other children parts
        for (int i = 0; i < numOfChildren; i++)
        {
            rendChildren[i].material.SetColor("_Color", Color.red);
        }
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}

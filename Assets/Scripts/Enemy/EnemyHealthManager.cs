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
    private GameObject healthbarObject;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        rend = GetComponent<Renderer>();
        rendChildren = new List<Renderer>();
        for (int i = 0; i < transform.childCount; i++)
        {

            GameObject child = transform.GetChild(i).gameObject;
            //Debug.Log(child.tag);
            if (child.tag != "healthBar")
            {
                rendChildren.Add(child.GetComponent<Renderer>());
                numOfChildren++;
            }
            else
            {
                healthbarObject = child;
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
            Destroy(healthbarObject);
            transform.LookAt(new Vector3(transform.position.x, 3, transform.position.z));
            //gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, gameObject.GetComponent<Rigidbody>().velocity.y, 0);
            gameObject.GetComponent<EnemyController>().isNumb = true;
            rend.material.SetColor("_Color", Color.grey);
            for (int i = 0; i < numOfChildren; i++)
            {
                rendChildren[i].material.SetColor("_Color", Color.grey);
            }
            if(gameObject.GetComponentInChildren<HurtPlayer>()!= null)
            {
                gameObject.GetComponentInChildren<HurtPlayer>().gameObject.SetActive(false);
            }
            Destroy(gameObject,5);
        }

    }

    public void HurtEnemy(float damage)
    {
        //Debug.Log("hurt");
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

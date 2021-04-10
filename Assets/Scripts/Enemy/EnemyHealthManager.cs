using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    [SerializeField] private float health;
    private float currentHealth;

    [SerializeField] private float hurtFlashLength;
    private float flashCountDown;

    //[SerializeField]
    //private UIGradientBar healthBar;// currently disabled 

    private Renderer rend;
    private List<Renderer> rendChildren;
    private int numOfChildren;
    private Color origColor;
    //private GameObject healthbarObject;
    private EnemyController enemyControl;


    private bool deadExcuted;// whether dead is already excuted
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        enemyControl = gameObject.GetComponent<EnemyController>();
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
                //healthbarObject = child;
            }
        }
        origColor = rend.material.GetColor("_Color");
        //healthBar.SetMaxBarVal(health);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Health update");
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
        if (currentHealth <= 0 && !deadExcuted)
        {

            deadExcuted = true;
            //Destroy(healthbarObject);
            //gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, gameObject.GetComponent<Rigidbody>().velocity.y, 0);
            rend.material.SetColor("_Color", Color.grey);
            for (int i = 0; i < numOfChildren; i++)
            {
                rendChildren[i].material.SetColor("_Color", Color.grey);
            }
            if (gameObject.GetComponentInChildren<HurtPlayer>() != null)
            {
                gameObject.GetComponentInChildren<HurtPlayer>().gameObject.SetActive(false);
            }
            enabled = false;
            enemyControl.Dead();
        }

        // fall off map, died
        if (transform.position.y <= -1)
        {
            currentHealth -= health;
        }

    }

    public void HurtEnemy(float damage)
    {
        if (enabled)
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
            //healthBar.SetValue(currentHealth);
        }
    }
}

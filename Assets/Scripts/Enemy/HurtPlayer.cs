using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    [SerializeField] float damage;
    private bool stillHurting;

    private PlayerHealthManager theHealthManager;

    private float nextHurtCountDown;
    [SerializeField] float nextHurtInterval;
    //// Start is called before the first frame update
    void Start()
    {
        nextHurtCountDown = nextHurtInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (stillHurting)
        {
            if (nextHurtCountDown <= 0)
            {
                theHealthManager.HurtPlayer(damage);
                nextHurtCountDown = nextHurtInterval;

            }
            else
            {
                nextHurtCountDown -= Time.deltaTime;
            }
        }
        else
        {
            nextHurtCountDown = nextHurtInterval;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            stillHurting = true;
            theHealthManager = other.gameObject.GetComponent<PlayerHealthManager>();
            theHealthManager.HurtPlayer(damage);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            stillHurting = false;
        }
    }
}

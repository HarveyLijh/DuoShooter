using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody enemyBody;
    public float moveSpeed;
    public bool isNumb;
    private Player thePlayer;
    public bool isDead;
    //private bool detachedFromEnv = true;
    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody>();
        thePlayer = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (thePlayer == null || !thePlayer.isActiveAndEnabled)
        {
            transform.LookAt(transform.position);
            isNumb = true;

        }
    }
    private void FixedUpdate()
    {
        if (!isNumb && thePlayer != null)
        {
            transform.LookAt(new Vector3(thePlayer.transform.position.x, 1.2f, thePlayer.transform.position.z));
            enemyBody.velocity = new Vector3(transform.forward.x * moveSpeed, enemyBody.velocity.y, transform.forward.z * moveSpeed);

        }


    }

    public void Dead()
    {

        goNumb();
        //gain energy for player
        thePlayer.energyManager.GainEnergy(1);
        thePlayer.xpManager.GainXP(10);
        enabled = false;
        isDead = true;
        // gain experience for player

        // enemy dead
        Destroy(gameObject, 5);
    }
    public void goNumb()
    {
        isNumb = true;
        //detachedFromEnv = true;
        transform.LookAt(new Vector3(transform.position.x, 1000, transform.position.z));
    }

    private void disableNumb()
    {
        isNumb = false;
        //detachedFromEnv = false;
        transform.LookAt(new Vector3(thePlayer.transform.position.x, 1.2f, thePlayer.transform.position.z));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (thePlayer != null && enabled)
        {
            if (collision.gameObject.tag != "Enemy")
            {
                disableNumb();

            }
            else
            {
                if (transform.position.y < 1)
                {
                    disableNumb();
                }
            }
        }
    }
}

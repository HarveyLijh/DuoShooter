using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody enemyBody;
    public float moveSpeed;
    public bool isDead;
    private Player thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody>();
        thePlayer = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= 1.11)
        {
            isDead = true;
            Debug.Log("high");
        }
        else
        {
            isDead = false;
            transform.LookAt(new Vector3(thePlayer.transform.position.x, 1.2f, thePlayer.transform.position.z));
        }
    }
    private void FixedUpdate()
    {
        if (!isDead)
        {
            enemyBody.velocity = (transform.forward * moveSpeed);
        }


    }
}

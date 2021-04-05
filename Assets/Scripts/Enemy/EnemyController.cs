using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody enemyBody;
    public float moveSpeed;
    public bool isNumb;
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
            goNumb();
        }
        else
        {
            disableNumb();
        }
    }
    private void FixedUpdate()
    {
        if (!isNumb)
        {
            enemyBody.velocity = (transform.forward * moveSpeed);
        }


    }

    public void goNumb()
    {
        isNumb = true;
        transform.LookAt(new Vector3(transform.position.x, 4f, transform.position.z));
    }

    private void disableNumb()
    {
        isNumb = false;
        transform.LookAt(new Vector3(thePlayer.transform.position.x, 1.2f, thePlayer.transform.position.z));
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Environment")
    //    {
    //        disableNumb();
    //    }
    //}
}

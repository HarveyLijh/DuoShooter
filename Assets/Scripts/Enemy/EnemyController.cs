using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody enemyBody;
    [SerializeField] float moveSpeed;

    public Player thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody>();
        thePlayer = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(thePlayer.transform.position.x,1.2f, thePlayer.transform.position.z));
    }

    private void FixedUpdate()
    {
        enemyBody.velocity = (transform.forward * moveSpeed);


    }
}

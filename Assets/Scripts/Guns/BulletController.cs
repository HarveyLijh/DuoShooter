using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    private float rangeCountDown;
    public float range;
    public float damage;
    public bool isPenetrable;
    public Vector3 spreadDirection;
    private Vector3 bulletVanishPoint;

    // Start is called before the first frame update
    void Start()
    {
        rangeCountDown = range / speed;
        getDifference(spreadDirection.x, spreadDirection.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (rangeCountDown <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            //transform.Translate(spreadDirection * speed * Time.deltaTime);
            float step = speed * Time.deltaTime;
            //Debug.DrawLine(transform.position, spreadDirection, Color.red);
            //Debug.DrawLine(transform.position, bulletVanishPoint, Color.blue);
            transform.position = Vector3.MoveTowards(transform.position, bulletVanishPoint, step);

            rangeCountDown -= Time.deltaTime;

        }
    }
    // calculate the difference between the bulletVanishPoint and the spreadDirection point
    private void getDifference(float origX, float origZ)
    {
        float differenceX = (spreadDirection.x - transform.position.x) * range;
        float differenceZ = (spreadDirection.z - transform.position.z) * range;

        bulletVanishPoint = new Vector3(spreadDirection.x+ differenceX, spreadDirection.y, spreadDirection.z + differenceZ);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(damage);
        }
        if (!isPenetrable)
        {
            Destroy(gameObject);
        }
    }

}

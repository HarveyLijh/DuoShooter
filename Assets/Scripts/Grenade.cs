using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    public float radius = 6f;
    public GameObject explosionEffect;
    public float force;
    private float countDown;
    private bool hasExploded = false;
    [SerializeField]
    private float damage = 30f;
    [SerializeField]
    private AudioSource ExplodeSound;

    // Start is called before the first frame update
    void Start()
    {
        countDown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        if (countDown <= 0 && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }


    private void Explode()
    {
        GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
        gameObject.GetComponent<Renderer>().enabled = false;
        ExplodeSound.Play();

        CameraShaker.Instance.ShakeOnce(8f,8f,.1f,1f);

        Collider[] affectedObjects = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in affectedObjects)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null && nearbyObject.gameObject.tag!="Grenade")
            {
                if (nearbyObject.gameObject.tag == "Enemy")
                {
                    nearbyObject.gameObject.GetComponent<EnemyController>().goNumb();
                    rb.AddExplosionForce(force, transform.position, radius, 10f, ForceMode.Impulse);
                    nearbyObject.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(damage);
                }
                else if(nearbyObject.gameObject.tag == "Player")
                {
                    rb.AddExplosionForce(force*1.5f, transform.position, radius, 10f, ForceMode.Impulse);
                    nearbyObject.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damage/3);
                }
                else
                {
                    rb.AddExplosionForce(force, transform.position, radius, 10f, ForceMode.Impulse);
                }
            }
        }

        Destroy(gameObject, 1f);
        Destroy(explosion, 1f);

    }

}

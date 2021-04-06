using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AmmoSupply : MonoBehaviour
{
    private List<Transform> children;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 50 * Time.deltaTime, 0); //rotates 50 degrees per second around z axis
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GunController currentGun = other.gameObject.GetComponent<Player>().currentGun.GetComponent<GunController>();
            //Debug.Log(currentGun.totalAmmo);
            if (currentGun.reloading)
            {
                currentGun.totalAmmo = currentGun.maxTotalAmmo + currentGun.maxTotalAmmo - currentGun.currentAmmoInClip;
            }
            else
            {
                currentGun.totalAmmo = currentGun.maxTotalAmmo;
            }
            currentGun.setupBulletInfo();
            other.gameObject.GetComponent<Player>().TakeBulletSupply();
            Destroy(gameObject);
        }
    }
}

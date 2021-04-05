using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSupply : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 50 * Time.deltaTime, 0); //rotates 50 degrees per second around z axis
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GunController currentGun = collision.gameObject.GetComponent<Player>().currentGun.GetComponent<GunController>();
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

            Destroy(gameObject);
        }
    }
}

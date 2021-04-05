using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public bool isFiring;
    [SerializeField] private BulletController bullet;
    [SerializeField] private float bulletSpeed;

    [SerializeField] private float bulletRange;

    [SerializeField] private int bulletDamage;

    [SerializeField] private float timeBtwShots_Auto;
    // count time between shots for auto gun
    private float shotCountDown_Auto;

    [SerializeField] private bool bulletIsPenetrable;

    [SerializeField] private float timeBtwShots_Manual;

    // count time between shots for manual gun
    private float shotCountDown_Manual;

    // if gun is automatic or manual
    [SerializeField] private bool GunAuto;
    // record last status of the isFiring to decide shot for manual guns
    private bool lastStatus;

    //if gun need reload
    [SerializeField] private bool reloadable;
    [SerializeField] private int AmmoPerClip;
    [SerializeField] private float reloadTime;
    public int currentAmmoInClip;
    private float reloadCountDown;
    public bool reloading;

    // spot where bullet is fired
    [SerializeField] private Transform fireSpot;

    // audio sources
    [SerializeField]
    private AudioSource shootingSound;
    private AudioSource reloadingSound;
    private AudioSource noAmmoSound;

    //recoil
    [SerializeField] float recoilLevel;
    private Vector3 upRecoil;
    private Vector3 origRotation;
    private bool recoiled_manual;
    private float recoilCountDown_manual;

    public UI_GunInfo theGunInfo;
    private bool haveStarted;

    //spread
    [SerializeField] private float spreadX;
    [SerializeField] private float spreadY;

    public int totalAmmo;
    public int maxTotalAmmo;
    // Start is called before the first frame update
    void Start()
    {
        //shootingSound = GetComponent<AudioSource>();
        upRecoil = new Vector3(recoilLevel * 3, 0, 0);
        recoilCountDown_manual = recoilLevel * 0.9f;
        origRotation = transform.localEulerAngles;
        // run after player's start
        //Invoke("setupBulletInfo", 0.05f);
        if (reloadable)
        {
            currentAmmoInClip = AmmoPerClip;
            reloadCountDown = reloadTime;
            maxTotalAmmo = totalAmmo;
        }
        setupBulletInfo();
        //Debug.Log("ss "+gameObject.name);
        haveStarted = true;
    }

    void OnEnable()
    {
        //Debug.Log("ee " + gameObject.name);
        if (haveStarted)
        {
            setupBulletInfo();
        }
    }
    void OnDisable()
    {
        //Debug.Log("dd " + gameObject.name);
        // clear reloading time when shift gun
        if (reloading)
        {
            currentAmmoInClip = 0;
            reloading = false;
        }

    }

    public void setupBulletInfo()
    {
        theGunInfo.showGunName(gameObject.name);
        if (reloadable)
        {
            theGunInfo.showBulletNum(currentAmmoInClip, totalAmmo);
        }
        else
        {
            theGunInfo.showBulletNum(-100, -100);
        }
    }

    //private void AddRecoil_Auto()
    //{
    //    transform.localEulerAngles += upRecoil;
    //}

    //private void AddRecoil_Manual()
    //{
    //    if (recoiled_manual)
    //    {
    //        return;
    //    }
    //    // separate the whole recoil into steps in order to slowly feed in the animation
    //    int steps = (int)(recoilCountDown_manual / Time.deltaTime);
    //    //if()
    //    transform.localEulerAngles += upRecoil;
    //    recoiled_manual = true;
    //}

    //private void StopRecoil()
    //{
    //    transform.localEulerAngles = origRotation;
    //}

    // Update is called once per frame
    void Update()
    {
        if (reloading)
        {
            reloadCountDown -= Time.deltaTime;
            Reload();
        }
        else
        {

            if (isFiring)
            {
                //Spread
                float spreadAxis_x = Random.Range(-spreadX / 10, spreadX / 10);
                float spreadAxis_y = Random.Range(-spreadY / 10, spreadY / 10);

                //Calculate Direction with Spread
                Vector3 direction = fireSpot.transform.forward + new Vector3(spreadAxis_x, spreadAxis_y, 0);

                // if no ammon
                if (reloadable && currentAmmoInClip <= 0)
                {
                    //play empty ammo sound and return
                    return;
                }
                // automatic guns
                if (GunAuto)
                {
                    lastStatus = true;
                    shotCountDown_Auto -= Time.deltaTime;
                    if (shotCountDown_Auto <= 0)
                    {
                        shotCountDown_Auto = timeBtwShots_Auto;
                        BulletController newBullet = Instantiate(bullet, fireSpot.position, fireSpot.rotation) as BulletController;
                        newBullet.speed = bulletSpeed;
                        newBullet.spreadDirection = fireSpot.position + direction;
                        shootingSound.Play();
                        newBullet.range = bulletRange;
                        newBullet.damage = bulletDamage;
                        newBullet.isPenetrable = bulletIsPenetrable;
                        if (reloadable)
                        {
                            currentAmmoInClip--;
                            theGunInfo.showBulletNum(currentAmmoInClip, totalAmmo);

                        }

                    }
                }
                // manual guns
                else
                {
                    //add recoil
                    //AddRecoil_Manual();
                    if (!lastStatus)
                    {
                        if (shotCountDown_Manual <= 0)
                        {

                            shotCountDown_Manual = timeBtwShots_Manual;
                            BulletController newBullet = Instantiate(bullet, fireSpot.position, fireSpot.rotation) as BulletController;
                            newBullet.speed = bulletSpeed;
                            newBullet.spreadDirection = fireSpot.position + direction;
                            shootingSound.Play();
                            newBullet.range = bulletRange;
                            newBullet.damage = bulletDamage;
                            newBullet.isPenetrable = bulletIsPenetrable;
                            if (reloadable)
                            {
                                currentAmmoInClip--;
                                theGunInfo.showBulletNum(currentAmmoInClip, totalAmmo);
                            }
                        }
                    }
                    // don't shot if counter is not reached
                    else
                    {
                        return;
                    }
                }
            }
            else
            {
                //stop recoil
                //StopRecoil();
                shotCountDown_Auto = 0;

                // Manual gun countdown when not firing
                if (!GunAuto)
                {
                    lastStatus = false;
                    shotCountDown_Manual -= Time.deltaTime;
                }
            }
        }


    }

    public void Reload()
    {
        //Debug.Log(reloadCountDown);
        //Debug.Log(totalAmmo);
        if (totalAmmo == 0)
        {
            //Debug.Log("returned");
            return;
        }
        if (reloadable && reloadCountDown <= 0)
        {
            //play reload sound and return


            // check if total ammo is empty, stop reload
           

            // check if ammo remain in clip
            if (currentAmmoInClip > 0)
            {
                // store remain ammo to total and clean the clip
                totalAmmo += currentAmmoInClip;
                currentAmmoInClip = 0;
            }

            if (totalAmmo <= AmmoPerClip)
            {
                currentAmmoInClip = totalAmmo;
                totalAmmo = 0;
            }
            else
            {
                currentAmmoInClip = AmmoPerClip;
                totalAmmo -= currentAmmoInClip;
            }
            theGunInfo.showBulletNum(currentAmmoInClip, totalAmmo);
            reloading = false;
            reloadCountDown = reloadTime;
        }
        else
        {
            reloading = true;
            theGunInfo.showBulletNum(-200, -200);
        }
    }
}

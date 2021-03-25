using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_GunInfo : MonoBehaviour
{
    //public int ammoPerClip;
    private int currentAmmoInClip;
    public Text bulletNum;
    public Text gunName;
    //public int totalAmmo;
    // Start is called before the first frame update
    void Start()
    {
        //currentAmmoInClip = ammoPerClip;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void showBulletNum(int bulletNumInt)
    {
        if (bulletNumInt == -100)
        {
            bulletNum.text = "∞";
        }
        else if(bulletNumInt == -200)
        {
            bulletNum.text = "Reloading...";
        }
        else
        {
            bulletNum.text = bulletNumInt.ToString();
        }
    }

    public void showGunName(string gunNameString)
    {
        gunName.text = gunNameString;
    }
}

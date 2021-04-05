using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_GunInfo : MonoBehaviour
{
    public Text bulletInfo;
    public Text gunName;
    // Start is called before the first frame update
    void Start()
    {
        //currentAmmoInClip = ammoPerClip;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void showBulletNum(int bulletNumInt, int totalBulletLeft)
    {
        if (bulletNumInt == -100)
        {
            bulletInfo.text = "∞/∞";
        }
        else if(bulletNumInt == -200)
        {
            bulletInfo.text = "Reloading...";
        }
        else
        {
            bulletInfo.text = bulletNumInt.ToString() + "/ " + totalBulletLeft;
        }
    }

    public void showGunName(string gunNameString)
    {
        gunName.text = gunNameString;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergyManager : MonoBehaviour
{
    [SerializeField] private float playerEnergy;
    public float currentEnergy;

    [SerializeField]
    private UIGradientBar energyBar;
    // Start is called before the first frame update
    void Start()
    {
        currentEnergy = playerEnergy;
        energyBar.SetMaxBarVal(playerEnergy);
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public void UseEnergy(float usageAmount)
    {
        currentEnergy -= usageAmount;
        energyBar.SetValue(currentEnergy);
    }

    public void GainEnergy(float usageAmount)
    {
        currentEnergy += usageAmount;
        if(currentEnergy > playerEnergy)
        {
            currentEnergy = playerEnergy;
        }
        energyBar.SetValue(currentEnergy);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{

    [SerializeField]
    private List<EnemySpawn> spawns;

    private int ID;
    private int currentSpawnNum;
    // Use this for initialization
    void Start()
    {
        foreach (EnemySpawn spawn in spawns)
        {
            spawn.spawnID = ID;
            ID++;
            currentSpawnNum++;
        }
        //Debug.Log(currentSpawnNum);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSpawnNum == 0)
        {
            enabled = false;
            GameOverMenu.isWin = true;
        }
    }

    public void spawnDead(int spawnID)
    {
        //Debug.Log("ID");
        //Debug.Log(spawnID);
        //Debug.Log(currentSpawnNum);
        currentSpawnNum--;
    }
}

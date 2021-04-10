using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    // type of enemy
    [SerializeField] private EnemyController enemyType;
    [SerializeField] private int numberToSpawn;
    [SerializeField] private float timeNeedToSpawnAll;
    private int currentNumber;
    private float nextSpawnCountDown;
    private float spawnInterval;

    private List<EnemyController> enemyList;
    public int spawnID = -1;
    public GameObject level;
    // Start is called before the first frame update
    void Start()
    {
        spawnInterval = timeNeedToSpawnAll / numberToSpawn;
        enemyList = new List<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentNumber);
        //Debug.Log(numberToSpawn);
        if (nextSpawnCountDown <= 0 && currentNumber < numberToSpawn)
        {
            EnemyController newEnemy = Instantiate(enemyType, transform.position, transform.rotation) as EnemyController;
            enemyList.Add(newEnemy);
            currentNumber++;
            nextSpawnCountDown = spawnInterval;
        }
        else
        if (currentNumber == numberToSpawn)
        {
            if (enemyList.Count == 0)
            {
                enabled = false;
                level.GetComponent<Level1>().spawnDead(spawnID);
            }
            else
            {
                foreach (EnemyController enemy in enemyList.ToArray())
                {
                    if (enemy.isDead)
                    {
                        enemyList.Remove(enemy);
                    }
                }
            }

        }
        else
        {
            nextSpawnCountDown -= Time.deltaTime;
        }


    }

    // draw a gizmo for the camera
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}

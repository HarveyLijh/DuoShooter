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
    // Start is called before the first frame update
    void Start()
    {
        spawnInterval = timeNeedToSpawnAll / numberToSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        if (nextSpawnCountDown<=0 && currentNumber < numberToSpawn)
        {
            EnemyController newEnemy = Instantiate(enemyType, transform.position, transform.rotation) as EnemyController;
            currentNumber++;
            nextSpawnCountDown = spawnInterval;
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

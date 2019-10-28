using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    private EnemyScript[] enemyArrayA;
    private EnemyShooterScript[] enemyArrayB;

    private EnemyProjectiles[] enemyProjectiles;
    [SerializeField]
    private GameObject toSpawn;

    public bool shouldSpawnProjectiles;

    private Transform spawnLocation;

    // Start is called before the first frame update
    void Start()
    {
        enemyArrayA = Object.FindObjectsOfType<EnemyScript>();
        enemyArrayB = Object.FindObjectsOfType<EnemyShooterScript>();

        if (shouldSpawnProjectiles)
        {

            enemyProjectiles = new EnemyProjectiles[150];

            spawnLocation = transform;

            spawnLocation.position += new Vector3(2000.0f, 2000.0f, 0.0f);

            GameObject temp;

            for (int i = 0; i < 150; i++)
            {
                temp = Instantiate(toSpawn, spawnLocation);
                temp.SetActive(false);
                enemyProjectiles[i] = temp.GetComponent<EnemyProjectiles>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public EnemyProjectiles GetProjectileToShoot()
    {
        for (int i = 0; i < 150; i++)
        {
            if (!enemyProjectiles[i].IsActive())
            {
                return enemyProjectiles[i];
            }
        }
        return null;
    }

    public void ResetAllEnemies()
    {
        foreach (EnemyScript enemy in enemyArrayA)
        {
            enemy.ResetObject();
        }

        foreach (EnemyShooterScript enemy in enemyArrayB)
        {
            enemy.ResetObject();
        }
    }
}

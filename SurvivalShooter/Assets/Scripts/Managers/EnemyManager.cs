using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;  //reference to player health.Spawn enemy if player has certain health
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;   //  No of spawnpoints for enemies to spawn.


    void Start ()
    {
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }


    void Spawn ()
    {
        if(playerHealth.currentHealth <= 0f)
        {
            return;
        }

        // to select any random spawnpoint
        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

        // create instance(which enemy to spawn,position of spawned enemy,rotation of  spawned enemy)
        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}

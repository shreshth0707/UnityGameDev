using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;                           // what is the enemy gonno follow.They need to find player so no public 
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    NavMeshAgent nav;     // navmesh


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;      
        //finding the player with the tag "Player" by going through every game object.
                                                                             
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <NavMeshAgent> ();
    }



    // use update with nacmesh
    void Update ()
    {
        // check if both enemy and the player are alive. if not alive no sense to  detect path to player.
        if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            nav.SetDestination (player.position);    // just specify position where the enemy needs to  go. ie:player position. 
        }
        else
        {
            nav.enabled = false;
        }
    }
}

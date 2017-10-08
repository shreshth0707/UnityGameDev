using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f; 
    public float range = 100f;


    float timer;    //keep  everything in sync ie:check time
    Ray shootRay;  // what is that we have hit .Is  it the enemy or something else.
    RaycastHit shootHit;  // Returns to us what we have hit
    int shootableMask;    // shootable mask ie:we have shootable things
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;  // how long effects prevails before ending


    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");  // get what is shootable
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
    }


    // control wheather or not is it time to shoot
    void Update ()
    {
        timer += Time.deltaTime; // increases as time progresses

        // fire1 is left mouse click by default 
        // if player has clicked LMB and it is time to shoot
		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot ();
        }

        // it we fired and time has been elapsed then disable the effects
        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        }
    }


    public void DisableEffects ()
    {
        // disable line and light as sufficient time has occured
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot ()
    {
        timer = 0f; // firing now so again calculate

        gunAudio.Play ();

        gunLight.enabled = true;

        // stop if particles are playing and then start
        gunParticles.Stop ();
        gunParticles.Play ();

        // line renderer is actual visual elemnt of bullet
        gunLine.enabled = true;

        // line has 2 position first is barrel of gun 
        gunLine.SetPosition (0, transform.position);

        shootRay.origin = transform.position;

        // forward in positive z axis as forward .Shoot in this direction
        shootRay.direction = transform.forward;

        // shootHit tell us what we have hit.Range till which bullet can reach.
        // if it hits something
        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
            // give me your enemy health script
            //if we have hit objects that are in shootable mask but are not enemies ie:wall,car etc
            // then those component will not have EnemyHealth script with them.
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();

            // enemy health script is there ie: we have hit an enemy.
            if (enemyHealth != null)
            {
                // make enemy to take damage
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);
            }
            // shootHit.point will tell us where we have hit .
            // this point is second end of ray
            gunLine.SetPosition (1, shootHit.point);
        }
        else // if we dont hit something ,make 2nd point of ray as a point outside the range deliberately so nothing happens.
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }
}

using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;  // starting health 
    public int currentHealth;           // current health
    public float sinkSpeed = 2.5f;  // when enemies die make them sink through floor after this time.
    public int scoreValue = 10;   // score get by killing it
    public AudioClip deathClip;   


    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;     
    CapsuleCollider capsuleCollider;
    bool isDead;    // check if dead
    bool isSinking; // check if sinking.


    // just set references in awake.
    void Awake ()
    {
        // GetComponent is used to get reference to the particular object.
        anim = GetComponent <Animator> ();   
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();

        currentHealth = startingHealth;
    }


    void Update ()
    {
        if(isSinking)
        {
            // move enemy down ie: sink effect 
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    // public fuction so can be called form another script
    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        // already dead do nothing
        if(isDead)
            return;

        // hurt sound
        enemyAudio.Play ();

        // lose some health
        currentHealth -= amount;
            
        // find the hit particle
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        // after taking damage if it is dead
        if(currentHealth <= 0)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;


        capsuleCollider.isTrigger = true;

        // dead animation
        anim.SetTrigger ("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }

    // it is public becoz

    public void StartSinking ()
    {
        GetComponent <NavMeshAgent> ().enabled = false;

        // when 
        GetComponent <Rigidbody> ().isKinematic = true;
        isSinking = true;
        
        // static variable so no use of GetComponent<>() .Just have className.component name 
        ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f); // destroy after 2 sec.
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;  // how much health player has in the starting
    public int currentHealth;         //  what is health after damage
    public Slider healthSlider;       // slider gameobject which is reference to slider UI. 
    public Image damageImage;         // reference to damage image      
    public AudioClip deathClip;         // death clip one shot sound
    public float flashSpeed = 5f;     //  how quickly damage image appears on screen
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);   // when damage image is flashed what will be color of screen.
    // the properties corresponds to red color and transparent.


    Animator anim;   // reference to animator
    AudioSource playerAudio;
    PlayerMovement playerMovement;   // reference to PlayerMovement script 
    PlayerShooting playerShooting;   // reference to PlayerShooting script
    bool isDead;       // determine if player is dead
    bool damaged;


    // called when game starts
    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> (); //PlayerShooting script is in the child of player game object and 
        // not the game object itself so use GetComponentInChildren<>() instead of GetComponent<>()
        currentHealth = startingHealth;   // starting health=100
    }


    //jab bhi kuch(koi bhi variable,state,anything)  update hoga.
    void Update ()
    {
        // damage taken so flash red
        if(damaged)
        {
            damageImage.color = flashColour;
        }
        else   // fade red image from 10%opacity to fully transparent
        {
            // lerp from the given color to the desired color at this speed.
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false; // ready to take more damage.
    }

    
    public void TakeDamage (int amount)
    {
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;  // slider will have new value.

        playerAudio.Play ();
        
        //check if player is dead or not
        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }

     
    //  called when player dies
    void Death ()
    {
        isDead = true;

        playerShooting.DisableEffects ();

        // play the death animation by setting the die trigger. 
        anim.SetTrigger ("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play ();

        playerMovement.enabled = false;  //  no more movement .player will stop wont move.
        playerShooting.enabled = false;
    }


    public void RestartLevel ()
    {
        Application.LoadLevel (Application.loadedLevel);
    }
}

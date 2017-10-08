using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    public float speed;
    private int count;
    private Rigidbody rb;
    public Text counttext;

    public Text wintext;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        count = 0;
        string s = count.ToString();
        counttext.text = "Count : "+ s;

	}
	
	// Update is called once per frame
	void FixedUpdate () {    
        float movehorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(movehorizontal,0.0f,moveVertical);

        rb.AddForce(movement*speed);
	}

    // when we pick up the objects
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            counttext.text = "Count : " + count.ToString();  
        }
        if (count == 18)
            wintext.text = "You win"; 
            
    }

}

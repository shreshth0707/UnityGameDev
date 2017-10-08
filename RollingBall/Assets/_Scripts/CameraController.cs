using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;
    // Use this for initialization
    void Start () {
        // what is the diff between the player and the camera in starting.
        offset = transform.position - player.transform.position; 
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
        // get new position of camera by adding offset to new position of player	

        transform.position = player.transform.position + offset;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Makes the object invisible upon starting the game.
//Useful for objects we want to see in the editor but dont want the player seeing.
public class InvisibleOnStart : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}

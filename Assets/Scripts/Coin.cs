using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    private GameManager gameManager;

    // Use this for initialization
    void Start ()
    {
       gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
    }
    

// Update is called once per frame
    void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            gameManager.coinCollected();
        }
    }

}

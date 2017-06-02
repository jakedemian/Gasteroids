using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour {




    private void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject);
    }



    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (!Utilities.isObjectOnScreen(transform))
        {
            // TODO use object pooling here
            Destroy(gameObject);
        }
    }
}

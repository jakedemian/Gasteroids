using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject asteroidPrefab;


	// Use this for initialization
	void Start () {
        Instantiate(asteroidPrefab, new Vector2(0, 0), Quaternion.identity);
            
     }
	
	// Update is called once per frame
	void Update () {
        GameObject[] allAsteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        if (allAsteroids.Length == 0) {
            SceneManager.LoadScene("main");
        }
	}
}

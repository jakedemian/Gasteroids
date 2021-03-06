﻿using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour {
    public enum AsteroidSize { Large, Medium, Small };

    public GameObject asteroidPrefab;

    private Rigidbody2D rb;

    public List<Sprite> asteroidSprites;

    private float minTurnSpeed = 5f;
    private float maxTurnSpeed = 8f;

    private float minSpeed = 0.5f;
    private float maxSpeed = 1f;

    private const int ASTEROIDS_TO_SPAWN = 2;

    [HideInInspector]
    public AsteroidSize size = AsteroidSize.Small;


    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        int spriteIndex = Random.Range(0, asteroidSprites.Count);
        GetComponent<SpriteRenderer>().sprite = asteroidSprites[spriteIndex];

        if(gameObject.GetComponent<PolygonCollider2D>() != null) {
            Destroy(GetComponent<PolygonCollider2D>());
        }

        gameObject.AddComponent<PolygonCollider2D>();
        AsteroidRotation();
        AsteroidMovement();
    }

    // Update is called once per frame
    void Update() {

        if(size == AsteroidSize.Large) {
            transform.localScale = new Vector2(1f, 1f);
        } else if(size == AsteroidSize.Medium) {
            transform.localScale = new Vector2(0.6f, 0.6f);
        } else {
            transform.localScale = new Vector2(0.4f, 0.4f);
        }

        handleOffScreen();

    }


    void AsteroidRotation() {
        float spin = Random.Range(minTurnSpeed, maxTurnSpeed);
        int isNegative = Random.Range(0, 2);

        if(isNegative == 1) {
            spin = -spin;
        }

        rb.angularVelocity = spin;
    }


    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "PlayerShot") {

            Vector2 myPos = transform.position;
            if(size == AsteroidSize.Large) {
                spawnAsteroids(AsteroidSize.Medium);
                Destroy(gameObject);
            }
            else if(size == AsteroidSize.Medium) {
                spawnAsteroids(AsteroidSize.Small);
                Destroy(gameObject);
            } else {
                Destroy(gameObject);
            }
        }
    }

    private void spawnAsteroids(AsteroidSize newAsteroidSize) {
        for(int i = 0; i < ASTEROIDS_TO_SPAWN; i++) {
            GameObject a = Instantiate(asteroidPrefab, transform.position, Quaternion.identity);
            a.GetComponent<AsteroidController>().size = newAsteroidSize;
        }
    }
    

    void AsteroidMovement() {

        float speed = Random.Range(minSpeed, maxSpeed);
        Vector3 randomUnitOnSphere = Random.onUnitSphere;

        rb.velocity = randomUnitOnSphere * speed;
    }

    private void handleOffScreen() {
        Vector2 dir = rb.velocity;
        Vector2 centerToPos = transform.position;
        bool isMovingAwayFromCenter = Vector2.Dot(dir, centerToPos) > 0f;

        if (!Utilities.isObjectOnScreen(transform) && isMovingAwayFromCenter) {
            Vector2 currentPos = Camera.main.WorldToViewportPoint(transform.position);

            // trim the position so that it isn't beyond the limits of the screen
            currentPos = Utilities.trimViewportVector(currentPos);

            // move the vector to the opposite side of the viewport
            currentPos = Utilities.translateViewportVectorToOppositeSide(currentPos);

            Vector2 newPos = Camera.main.ViewportToWorldPoint(currentPos);
            transform.position = newPos;
        }
    }


    
}

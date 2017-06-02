using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour {
    private Rigidbody2D rb;

    public List<Sprite> asteroidSprites;

    private float minTurnSpeed = 5f;
    private float maxTurnSpeed = 8f;

    private float minSpeed = 0.5f;
    private float maxSpeed = 1f;


    void AsteroidRotation() {
        float spin = Random.Range(minTurnSpeed, maxTurnSpeed);
        int isNegative = Random.Range(0, 2);

        if(isNegative == 1) {
            spin = -spin;
        }

        rb.angularVelocity = spin;
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


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        int spriteIndex = Random.Range(0, asteroidSprites.Count);
        GetComponent<SpriteRenderer>().sprite = asteroidSprites[spriteIndex];
        gameObject.AddComponent<PolygonCollider2D>();
        AsteroidRotation();
        AsteroidMovement();
    }
	
	// Update is called once per frame
	void Update () {
        handleOffScreen();

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject shot;

    private Rigidbody2D rb;

    
    private const float ACCELERATION = 320f;
    private const float TURN_SPEED = 40f;

    private const float MAX_TURN_SPEED = 200f;
    private const float MAX_SPEED = 6f;

    private const float SHOT_SPEED = 10f;


    void Start () {
		rb = GetComponent <Rigidbody2D>();
	}

    void Update() {
        UpdatePlayerMovement();
        UpdatePlayerTurn();

        // player shoot control
        if(Input.GetButtonDown("Shoot")) {
            GameObject go = Instantiate(shot, transform.position, Quaternion.identity);
            go.transform.position = transform.position + (transform.up * transform.localScale.x);
            go.transform.rotation = transform.rotation;
            go.GetComponent<Rigidbody2D>().velocity = transform.up * SHOT_SPEED;
        }

        // when the player goes off screen
        handlePlayerOffScreen();
    }


    void UpdatePlayerTurn(){
		if (Input.GetAxisRaw("Horizontal") != 0){
			float dir = Input.GetAxisRaw("Horizontal");

			rb.AddTorque(TURN_SPEED * -dir * Time.deltaTime);
		}
		if (Mathf.Abs(rb.angularVelocity) > MAX_TURN_SPEED){
			if(rb.angularVelocity > 0){
				rb.angularVelocity = MAX_TURN_SPEED;
			} else {
				rb.angularVelocity = -MAX_TURN_SPEED;
			}
		}
	}


	void UpdatePlayerMovement () {
		if (Input.GetAxisRaw("Vertical") > 0){
			rb.AddForce(transform.up * ACCELERATION * Time.deltaTime);
            //(new Vector2(0f,accel * Time.deltaTime)jakeisgay
        }

        if (rb.velocity.magnitude > MAX_SPEED){
            rb.velocity = rb.velocity.normalized * MAX_SPEED;
		}
	}

    private void handlePlayerOffScreen() {
        Vector2 playerDir = rb.velocity;
        Vector2 centerToPlayer = transform.position;
        bool isPlayerMovingAwayFromCenter = Vector2.Dot(playerDir, centerToPlayer) > 0f;

        if(!Utilities.isObjectOnScreen(transform) && isPlayerMovingAwayFromCenter) {
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

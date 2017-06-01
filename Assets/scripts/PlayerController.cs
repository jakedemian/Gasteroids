using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject shot;
    private float shotSpeed = 10f;
	private float maxTurnSpeed = 100f;
	private float maxSpeed = 175f;
	private float accel = 100f;
	private float turnSpeed = 100f;
	private Rigidbody2D rb; 



	// Use this for initialization
	void Start () {
		rb = GetComponent <Rigidbody2D>();
	}


	void UpdatePlayerTurn(){
		if (Input.GetAxisRaw("Horizontal") != 0){
			float dir = Input.GetAxisRaw("Horizontal");

			rb.AddTorque(turnSpeed * -dir * Time.deltaTime);
		}
		if (Mathf.Abs(rb.angularVelocity) > maxTurnSpeed){
			if(rb.angularVelocity > 0){
				rb.angularVelocity = maxTurnSpeed;
			} else {
				rb.angularVelocity = -maxTurnSpeed;
			}
		}
	}


	void UpdatePlayerMovement () {
		if (Input.GetAxisRaw("Vertical") > 0){
			rb.AddForce(transform.up * accel * Time.deltaTime);
            //(new Vector2(0f,accel * Time.deltaTime)jakeisgay
        }

        if (rb.velocity.magnitude > maxSpeed){
            rb.velocity = rb.velocity.normalized * maxSpeed;
		}
	}


	// Update is called once per frame
	void Update () {
		UpdatePlayerMovement();
		UpdatePlayerTurn();
        if (Input.GetButtonDown("Shoot"))
        {
            GameObject go = Instantiate(shot, transform.position, Quaternion.identity);
            go.transform.position = transform.position + transform.up;
            go.transform.rotation = transform.rotation;
            go.GetComponent<Rigidbody2D>().velocity = transform.up * shotSpeed;
        }

        Vector2 playerDir = rb.velocity;
        Vector2 centerToPlayer = transform.position;
        bool isPlayerMovingAwayFromCenter = Vector2.Dot(playerDir, centerToPlayer) > 0f;

        if (!Utilities.isObjectOnScreen(transform) && isPlayerMovingAwayFromCenter)
        {
            Vector2 currentPos = Camera.main.WorldToViewportPoint(transform.position);
            Debug.Log("before:  " + currentPos);
            if(currentPos.x < 0)
            {
                currentPos = new Vector2(1, currentPos.y);
            } else if (currentPos.x > 1)
            {
                currentPos = new Vector2(0, currentPos.y);
            } else if(currentPos.y < 0)
            {
                currentPos = new Vector2(currentPos.x, 1);
            }
            else if (currentPos.y > 1)
            {
                currentPos = new Vector2(currentPos.x, 0);
            }

            Debug.Log("after:  " + currentPos);
            transform.position = Camera.main.ViewportToWorldPoint(currentPos);


        }

	}
}

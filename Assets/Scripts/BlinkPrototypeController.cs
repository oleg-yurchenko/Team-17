using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkPrototypeController : MonoBehaviour
{
	// public float blink_activeLimit; // changed to cooldown
	public float blinkDuration; // Adjust duration as needed
	public float blinkCooldown; // Cooldown period between blinks
	private float blinkAlpha = 0.5f; // Adjust transparency as needed
	private float blink_activeTimer;
	private bool isBlinking;
	private bool canBlink = true;
	private float lastBlinkTime;
	private Collider2D collider;
	private SpriteRenderer renderer;
	private Color originalColor;
	
	
	
	// Start is called before the first frame update
	void Start()
	{
		isBlinking = false;
		canBlink = true;
		collider = GetComponent<Collider2D>();
		renderer = GetComponent<SpriteRenderer>();
        originalColor = renderer.color;
		blink_activeTimer = 0.0f;
		lastBlinkTime = -blinkCooldown; // Allow blinking immediately at the start
	}

	// Update is called once per frame
	void Update()
	{
		// Color updatedColor;
		// if (isBlinking)
		// 	updatedColor = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0.5f);
		// else
		// 	updatedColor = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1.0f);
		// renderer.color = updatedColor;

		if (isBlinking)
        {
            blink_activeTimer += Time.deltaTime;
            // float t = blink_activeTimer / blinkDuration;
            // Color updatedColor = originalColor;
            // updatedColor.a = Mathf.Lerp(originalColor.a, blinkAlpha, t);
            // renderer.color = updatedColor;

			// Checks if the blink duration has exceeded and if the object is not colliding with anything, then ends the blink.
            if (blink_activeTimer >= blinkDuration && ((collider.OverlapCollider(new ContactFilter2D().NoFilter(), new Collider2D[1]) == 0))) // if Blink has blinked more than the duration time, it ends
            {
                EndBlink();
            }
			else
            {
                renderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, blinkAlpha); // Set color to blink color
            }
        }
	}

	// FixedUpdate used for physics calculations
	void FixedUpdate()
	{
		// //print("Can Blink: "+canBlink.ToString());
		// //print("Is Blinking: "+isBlinking.ToString());
		// //print("Active Timer: "+blink_activeTimer.ToString());
		// //print("delta time: " + Time.fixedDeltaTime);

		// if (isBlinking && !canBlink)
		// 	// blink_activeTimer += Time.fixedDeltaTime;
		// 	blink_activeTimer += Time.deltaTime;


		// if (Input.GetKey(KeyCode.LeftControl) && canBlink)
		// {
		// 	isBlinking = true;
		// 	canBlink = false;
		// }

		// //print(collider.OverlapCollider(new ContactFilter2D().NoFilter(), new Collider2D[1]).ToString());

		// // I was a bit stupid here -- It works, but the problem is it can only detect collisions when it is 'enabled' so the latter part is basically redundant (for now)
		// // Potential fix? I think having a separate collider, but this could prove taxing
		// // What is the latter part trying to fix? We don't want the player to get stuck inside some structure while they're blinking if the player collider becomes enabled while inside another collider.
		// if ((blink_activeTimer > blink_activeLimit) && (collider.OverlapCollider(new ContactFilter2D().NoFilter(), new Collider2D[1]) == 0))
		// {
		// 	isBlinking = false;
		// 	canBlink = true;
		// 	blink_activeTimer = 0.0f;
		// 	collider.enabled = true;
		// }

		if (isBlinking)
            return;

        if (Input.GetKey(KeyCode.LeftControl) && canBlink)
        {
            if (Time.time - lastBlinkTime >= blinkCooldown) // if the cooldown is over
            {
                StartBlink();
                lastBlinkTime = Time.time;
            }
        }



	}

	void StartBlink()
    {
        isBlinking = true;
        canBlink = false;
        blink_activeTimer = 0.0f;
    }

	void EndBlink()
    {
        isBlinking = false;
        canBlink = true;
        collider.enabled = true;
        renderer.color = originalColor; // restore the color
    }

	void OnCollisionEnter2D(Collision2D col) {
		if (isBlinking && (col.gameObject.tag != "Respawn")) {
			collider.enabled = false;
		}
	}
}

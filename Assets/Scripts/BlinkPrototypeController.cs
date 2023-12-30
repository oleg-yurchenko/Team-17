using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkPrototypeController : MonoBehaviour
{
	public float blink_activeLimit;
	private float blink_activeTimer;
	private bool isBlinking;
	private bool canBlink;
	private Collider2D collider;
	private SpriteRenderer renderer;
	// Start is called before the first frame update
	void Start()
	{
		isBlinking = false;
		canBlink = true;
		collider = GetComponent<Collider2D>();
		renderer = GetComponent<SpriteRenderer>();
		blink_activeTimer = 0.0f;
	}

	// Update is called once per frame
	void Update()
	{
		Color updatedColor;
		if (isBlinking)
			updatedColor = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0.5f);
		else
			updatedColor = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1.0f);
		renderer.color = updatedColor;
	}

	// FixedUpdate used for physics calculations
	void FixedUpdate()
	{
		//print("Can Blink: "+canBlink.ToString());
		//print("Is Blinking: "+isBlinking.ToString());
		//print("Active Timer: "+blink_activeTimer.ToString());
		//print("delta time: " + Time.fixedDeltaTime);

		if (isBlinking && !canBlink)
			blink_activeTimer += Time.fixedDeltaTime;


		if (Input.GetKey(KeyCode.LeftControl) && canBlink)
		{
			isBlinking = true;
			canBlink = false;
			collider.enabled = false;
		}

		//print(collider.OverlapCollider(new ContactFilter2D().NoFilter(), new Collider2D[1]).ToString());

		// I was a bit stupid here -- It works, but the problem is it can only detect collisions when it is 'enabled' so the latter part is basically redundant (for now)
		// Potential fix? I think having a separate collider, but this could prove taxing
		// What is the latter part trying to fix? We don't want the player to get stuck inside some structure while they're blinking if the player collider becomes enabled while inside another collider.
		if ((blink_activeTimer > blink_activeLimit) && (collider.OverlapCollider(new ContactFilter2D().NoFilter(), new Collider2D[1]) == 0))
		{
			isBlinking = false;
			canBlink = true;
			blink_activeTimer = 0.0f;
			collider.enabled = true;
		}

	}
}

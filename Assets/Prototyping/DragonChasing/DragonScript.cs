using UnityEngine;

public class DragonScript : MonoBehaviour
{
	[SerializeField] float forwardSpeed;
	[SerializeField] Transform playerPos;
	private float originSpeed;

	private void Start()
	{
		originSpeed = forwardSpeed;
	}

	void FixedUpdate()
	{
		Vector3 distToPlayer = playerPos.position - transform.position;
		
		// This condition speeds up the dragon so it can catches the player when the player is too far
		if (distToPlayer.x > 27) 
		{
			forwardSpeed *= 3;
		} 
		else 
		{
			forwardSpeed = originSpeed;
		}
		// Calculate forward movement for the body
		// NOTE: because of the dragon's default rotation 180, right and left is reversed
		Vector3 forwardMovement = -transform.right * forwardSpeed * Time.deltaTime;

		// Apply forward movement to the body
		transform.position += forwardMovement;
	}
}
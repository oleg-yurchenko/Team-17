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
		Debug.Log(distToPlayer);
		
		// This condition speeds up the dragon so it can catches the player when the player is too far
		if (distToPlayer.x > 27) 
		{
			forwardSpeed *= 3;
		}
		else if (distToPlayer.x < 13)
		{
			forwardSpeed = 0;
		}
		else 
		{
			forwardSpeed = originSpeed;
		}
		// Calculate forward movement for the body
		// NOTE: because of the dragon's default rotation 180, right and left is reversed
		//Vector3 forwardMovement = -transform.right * forwardSpeed * Time.deltaTime;
		Vector3 forwardMovement = distToPlayer.normalized * forwardSpeed * Time.deltaTime;

		// Apply forward movement to the body
		transform.position += forwardMovement;
	}
}
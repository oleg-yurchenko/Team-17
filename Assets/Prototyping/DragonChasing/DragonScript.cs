using UnityEngine;

public class DragonScript : MonoBehaviour
{
		[SerializeField] float forwardSpeed;


		void FixedUpdate()
		{
				// Calculate forward movement for the body
				Vector3 forwardMovement = transform.right * forwardSpeed * Time.deltaTime;

				// Apply forward movement to the body
				transform.position += forwardMovement;
		}
}
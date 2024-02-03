using UnityEngine;

public class ChargeJumpScript : MonoBehaviour
{
	[SerializeField] private Transform checkGroundSphere;
	[SerializeField] private LayerMask groundMask;
	[SerializeField] private float jumpPower;
	[SerializeField] private float chargePower;
	[SerializeField] private Vector2 jumpDirection = Vector2.up;

	private float initialChargePower;
	private Rigidbody2D rb;
	private MovementPrototypeController movement;
	private bool jumpNow = false;
	public bool c_isCharging = false;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		movement = GetComponent<MovementPrototypeController>();
		initialChargePower = chargePower;
	}


	// Update is called once per frame
	private void Update()
	{
		ChargeJumpMultiplier();
	}

	private void FixedUpdate()
	{
		if (jumpNow)
		{
			rb.AddForce(jumpDirection * chargePower * jumpPower, ForceMode2D.Impulse);
			jumpNow = false;
			chargePower = initialChargePower;
		}
	}

	private void ChargeJumpMultiplier()
	{
		bool checkGround = Physics2D.CircleCast(checkGroundSphere.position, .5f, Vector2.down, .5f, groundMask);
		if (checkGround)
		{
			if (Input.GetKey(KeyCode.Space))
			{
				chargePower += Time.deltaTime * 1.5f;
				c_isCharging = true;

				if (chargePower > jumpPower)
				{
					chargePower = jumpPower;
				}
			}

			if (Input.GetKeyUp(KeyCode.Space))
			{
				jumpNow = true;
				c_isCharging = false;
			}
		}
	}
}

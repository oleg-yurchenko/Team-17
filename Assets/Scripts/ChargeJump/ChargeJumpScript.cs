using System;
using Unity.VisualScripting;
using UnityEngine;

public class ChargeJumpScript : MonoBehaviour
{
	[SerializeField] private Transform checkGroundSphere;
	[SerializeField] private LayerMask groundMask;
	[SerializeField] private float maxJumpPower = 3.0f;
	public float c_MaxJumpPower 
	{
		get { return maxJumpPower; }
		set { maxJumpPower = value; }
	}
	[SerializeField] public float chargePower = 1.0f;
	public float c_ChargePower 
	{
		get { return chargePower; }
		set { chargePower = value; }
	}
	[SerializeField] private Vector2 jumpDirection = Vector2.up;
	[SerializeField] private ChargeJumpMeter jumpMeter;

	private float initialChargePower;
	private Rigidbody2D rb;
	private bool jumpNow = false;
	public bool c_isCharging = false;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		
		initialChargePower = chargePower;
		
		// initializing the charge jump meter
		jumpMeter.SetMinCharge(chargePower);
		jumpMeter.SetMaxCharge(maxJumpPower);
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
			rb.AddForce(jumpDirection * chargePower * maxJumpPower, ForceMode2D.Impulse);
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
				
				jumpMeter.SetCharge(chargePower);
				
				c_isCharging = true;

				if (chargePower > maxJumpPower)
				{
					chargePower = maxJumpPower;
				}
			}
			else if (Input.GetKeyUp(KeyCode.Space))
			{
				jumpNow = true;
				c_isCharging = false;
			}
		}
	}
}

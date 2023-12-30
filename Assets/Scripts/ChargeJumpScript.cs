using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class ChargeJumpScript : MonoBehaviour
{
	[SerializeField] private Transform d_checkGroundSphere;
	[SerializeField] private LayerMask d_groundMask;
	[SerializeField] private float d_jumpPower;
	[SerializeField] private float d_chargePower;
	[SerializeField] private Vector2 d_jumpDirection = Vector2.up;

	private float initialChargePower;
	private Rigidbody2D rb;
	private bool jumpNow = false;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		initialChargePower = d_chargePower;
	}


	// Update is called once per frame
	private void Update()
	{
		bool checkGround = Physics2D.CircleCast(d_checkGroundSphere.position, .5f, Vector2.down, .5f, d_groundMask);
		if (checkGround)
		{
			if (Input.GetKey(KeyCode.Space))
			{
				d_chargePower += Time.deltaTime * 1.5f;

				if (d_chargePower > d_jumpPower * 1.5f)
				{
					d_chargePower = d_jumpPower * 1.5f;
				}
			}

			if (Input.GetKeyUp(KeyCode.Space))
			{
				jumpNow = true;
			}
		}
	}

	private void FixedUpdate()
	{
		if (jumpNow)
		{
			rb.AddForce(d_jumpDirection * d_chargePower * d_jumpPower, ForceMode2D.Impulse);
			jumpNow = false;
			d_chargePower = initialChargePower;
		}
	}
}

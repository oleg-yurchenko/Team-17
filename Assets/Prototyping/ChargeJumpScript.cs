using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChargeJumpScript : MonoBehaviour
{
    [SerializeField] private Transform CheckGroundSphere;
    [SerializeField] private LayerMask GroundMask;
    [SerializeField] private float JumpPower;
    [SerializeField] private float ChargePower;
    [SerializeField] private Vector2 jumpDirection = Vector2.up;

    private float initialChargePower;
    private Rigidbody2D rb;
    private bool jumpNow = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialChargePower = ChargePower;
    }

    // Update is called once per frame
    private void Update()
    {
        bool checkGround = Physics2D.CircleCast(CheckGroundSphere.position, .5f, Vector2.down, .5f, GroundMask);
        if (checkGround)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                ChargePower += Time.deltaTime * 2;
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
            rb.AddForce(jumpDirection * ChargePower * JumpPower, ForceMode2D.Force);
            jumpNow = false;
            ChargePower = initialChargePower;
        }
    }
}

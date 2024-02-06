using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPrototypeController : MonoBehaviour
{
    public float dash_force;
    public float dash_duration;
    public float dash_cooldown;
    private float cooldown_timer;
    private SpriteRenderer renderer;
    private bool is_dashing;
    private Color originalColor;
    private float dash_timer; // Keeps track of the time elapsed during a dash
    private Rigidbody2D body;
    private bool dash_direction; // if T = right, F = left

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        is_dashing = false;
        dash_timer = 0.0f;
        originalColor = renderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        Color updatedColor;
        if(is_dashing){
            updatedColor = Color.red;
            // originalColor = renderer.color;
        }else{
            updatedColor = originalColor;
        } 
        renderer.color = updatedColor;

    }

    // FixedUpdate used for physics calculations
    void FixedUpdate()
    {

        // Debug.Log(is_dashing.ToString());
        // Check for dash input
        // dash when the player hits the key Q
        if (Input.GetKey("q") && !is_dashing)
        {
            if((Input.GetKey("a") && Input.GetKey("d"))){
                return;
            }else if(Input.GetKey("a")){
                dash_direction = false;
                StartDash();
            }else if(Input.GetKey("d")){
                dash_direction = true;
                StartDash();
            }else{// inertia!!!
                // if obj moving left and the player hits no key, we dash left
                // if obj moving right and the player hits no key, we dash right
                if(body.velocity.x < 0){ // left
                    dash_direction = false;
                    StartDash();
                }else if(body.velocity.x > 0){ // right
                    dash_direction = true;
                    StartDash();
                }

            }
        }

        if (is_dashing)
        {
            if(!dash_direction && Input.GetKey("d")){
                // obj freeze
                body.velocity = new Vector2();
                StopDash();
                return;
            }
            if(dash_direction && Input.GetKey("a")){
                // obj freeze
                body.velocity = new Vector2();
                StopDash();
                return;
            }
            
            // Update dash timer
            dash_timer += Time.fixedDeltaTime;
            // Check if dash duration is over
            if (dash_timer >= dash_duration)
            {
                StopDash();
            }
        }
        if(!is_dashing){ // if we r not dashing
            cooldown_timer += Time.fixedDeltaTime;
        }

    }

    void StartDash()
    {
        if(cooldown_timer < dash_cooldown){ // ability cd, cannot use dash
            return;
        }

        is_dashing = true;
        // StartCoroutine(DashCoroutine());

        // Apply dash force
        // if obj goes left, dash left
        if(!dash_direction){
            // Debug.Log(dash_direction.ToString());
            body.AddForce(Vector2.left * dash_force, ForceMode2D.Impulse);
        }else{ // if obj goes right, dash right
            body.AddForce(Vector2.right * dash_force, ForceMode2D.Impulse);
        }
    }

    void StopDash()
    {
        is_dashing = false;
        // Debug.Log("STOP?");
        body.velocity = new Vector2(0, body.velocity.y); // x axis stop
        
        dash_timer = 0.0f;
        cooldown_timer = 0.0f;

    }


    // IEnumerator DashCoroutine()
    // {
    //     // This coroutine prevents adding dash force in subsequent FixedUpdate frames
    //     yield return new WaitForFixedUpdate();
    //     is_dashing = false;
    //     dash_timer = 0.0f;
    // }
}

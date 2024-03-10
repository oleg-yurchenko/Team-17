using UnityEngine;

public class DragonScript : MonoBehaviour
{
	[SerializeField] Transform target;
	[SerializeField] Rigidbody2D headRb;
	[SerializeField] GameObject body;
	[SerializeField] float speed;
	[SerializeField] float amplitude;
	[SerializeField] float frequency;
	private Rigidbody2D rb;
	private Vector2 movement;
	private Vector2 originalPos;
	private Vector3 directionToPlayer;
	
	// Start is called before the first frame update
	void Start()
	{
		originalPos = transform.position;
		rb = GetComponent<Rigidbody2D>();
	}

	void Update() 
	{
		directionToPlayer = (target.transform.position - headRb.transform.position).normalized;
		float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
		headRb.rotation = Mathf.Clamp(angle, -70, 70);
	}

	void FixedUpdate()
	{   
		Vector2 sinMove = new Vector2(0.0f, Mathf.Sin(Time.time * frequency) * amplitude * Time.deltaTime);
		transform.position = originalPos + sinMove + (Vector2) transform.right * speed * Time.time;
	}
}
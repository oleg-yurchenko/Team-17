using UnityEngine;

public class DragonScript : MonoBehaviour
{
	[SerializeField] GameObject root;
	[SerializeField] float speed;
	[SerializeField] float amplitude;
	[SerializeField] float frequency;
	private Vector2 originalPos;
	private Vector2 originalRootPos;
	
	// Start is called before the first frame update
	void Start()
	{
		originalRootPos = root.transform.position;
		originalPos = transform.position;
	}

	void Update() 
	{
	}

	void FixedUpdate()
	{   
		Vector2 sinMove = new Vector2(0.0f, Mathf.Sin(Time.time * frequency) * amplitude * Time.deltaTime);
		root.transform.position = originalRootPos + sinMove;
		
		transform.position = originalPos + (Vector2) transform.right * speed * Time.deltaTime;
	}
}
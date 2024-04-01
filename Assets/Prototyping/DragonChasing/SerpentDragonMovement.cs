using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class SerpentDragonMovement : MonoBehaviour
{
	[SerializeField] private float flySpeed = 0.0f;
	[SerializeField] private float offsetBetweenBody = 2.5f;
	[SerializeField] private GameObject head;
	[SerializeField] private List<GameObject> body = new List<GameObject>();
	
	private Rigidbody2D headRb;
	
	// Start is called before the first frame update
	void Start()
	{
		InitDragon();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		StartCoroutine(FollowPlayer());
	}
	
	private void InitDragon() 
	{
		headRb = head.GetComponent<Rigidbody2D>();
	}
	
	private IEnumerator FollowPlayer() 
	{
		// float directionToPlayer
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 directionToMouse = (mousePosition - head.transform.position).normalized;
		headRb.velocity = directionToMouse * flySpeed * Time.deltaTime;
		
		float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
		head.transform.rotation = Quaternion.Euler(0, 0, angle);
	
		
		for (int i = 0; i < body.Count; i++) 
		{
			MarkerManager markerManager = (i == 0) ? head.GetComponent<MarkerManager>() 
			: body[i - 1].GetComponent<MarkerManager>();
			
			Vector3 bodyPos = body[i].transform.position;
			Quaternion bodyRot = body[i].transform.rotation;
			body[i].transform.position = Vector3.Lerp(bodyPos, markerManager.markerList[0].position, 0.2f);
			body[i].transform.rotation = Quaternion.Lerp(bodyRot, markerManager.markerList[0].rotation, 0.2f);
			markerManager.markerList.RemoveAt(0);
			yield return new WaitForSeconds(0.1f);
		}
	}
}

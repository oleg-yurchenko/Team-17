using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerManager : MonoBehaviour
{
	public class Marker
	{
		public Vector3 position;
		public Quaternion rotation;
		
		public Marker(Vector3 position, Quaternion rotation) 
		{
			this.position = position;
			this.rotation = rotation;
		}
	}
	
	public List<Marker> markerList = new List<Marker>();
	
	// Start is called before the first frame update
	void Start()
	{
		
	}

	void FixedUpdate()
	{
		UpdateMarkerList();
	}
	
	public void UpdateMarkerList() 
	{
		markerList.Add(new Marker(transform.position, transform.rotation));
	}
	
	public void ClearMarkerList() 
	{
		markerList.Clear();
		UpdateMarkerList();
	}
}

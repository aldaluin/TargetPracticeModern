using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Goldraven.AI {

public class WaypointSet : MonoBehaviour {

	public   Vector3 [] waypoints;
	public   bool ordered = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Vector3 getNextWaypoint(Vector3 oldWaypoint) {
		if (ordered) {
			for (int i = 0; i < waypoints.Length; i++) {
				if (waypoints [i] == oldWaypoint) {
					if (i < (waypoints.Length - 1)) {
						return waypoints [i + 1];
					} else {
						return waypoints [0];
					}
				}
			}
			return waypoints [0];
		} else {
			int i;
			do {
				i = Random.Range (0, waypoints.Length - 1);
			} while  (waypoints [i] == oldWaypoint);
			return waypoints [i];
		}
	
	}
}
}

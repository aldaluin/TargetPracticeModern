using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Goldraven.AI;

namespace Goldraven.AI {
	
public class Sensors : MonoBehaviour
{
		public GameObject[] eyes { get; set; }
		public GameObject[] watched { get; set; }

		private List<Vector3> y = new List<Vector3> ();
		private List<Vector3> w = new List<Vector3>();



	// Use this for initialization
	void Start ()
	{
			foreach (GameObject eye in eyes) {
				y.Add (eye.transform.position);
				//y.Add (eye.GetComponent<CharacterAttributes> ());
			}
			foreach (GameObject view in watched) {
				w.Add (view.transform.position);
				//w.Add (view.GetComponent<CharacterAttributes> ());
			}
	}
	
	// Update is called once per frame
	void Update ()
	{
				
	}

		CharacterAttributes getClosest (GameObject source) {
			// This is the closest attacker/target
			return null;
		}

		Vector3 getClosestDirection (GameObject source) {
			// Normalized vector the closest attacker/target
			return Vector3.zero;

		}

		float getClosestRange(GameObject source) {
			// Distance to the closest target/attacker
			return 0f;
		}

		int CountAllInSight () {
			// This is the total of all combinations of sources and targets/attackers
			return 0;

		}

		int CountInSight( GameObject source) {
			// This is the number of attackers/targets in range
			return 0;

		}

		float getSpread(GameObject source) {
			// This is the angle in degrees between all attackers/targets in range 
			return 0f;

		}

		Vector3 getEscapeDirection(GameObject source){
			// This is the (normalized) vector to the middle of the biggest gap between attackers/targets in range
			return Vector3.zero;

		}
}


}


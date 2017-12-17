using UnityEngine;
using System.Collections;

namespace Goldraven.Weapons
{

	public class ShotLive : MonoBehaviour
	{

		/*
		 * cmcb  2017/10/16
		 * 
		 * For display only; does no damage
		 * because in testing it moved too fast
		 * to affect colliders (even continuous)
		 * 
		 */

		public float speed = 0;
		public float maxDistance = 2000f;
		public float maxTime = 20f;

		private float startTime;
		private  Vector3 startPoint;
		private bool originalObject;   // identify non-clones (by name) so they won't be destroyed

		void Start() {
			startTime = Time.timeSinceLevelLoad;
			startPoint = transform.position;
			if (gameObject.name.Contains ("(Clone)")) {
				originalObject = false;
			} else {
				originalObject = true;
			}
		}

		// Update is called once per frame
		void Update ()
		{
			transform.Translate (0, 0, speed * Time.deltaTime);
			if (originalObject) {
				return;
			}
			if (Vector3.Distance (startPoint, transform.position) > maxDistance) {
				Destroy (this.gameObject);
			} else if (Time.timeSinceLevelLoad - startTime > maxTime) {
				Destroy (this.gameObject);
			}
		}

	}
}

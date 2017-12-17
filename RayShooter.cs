using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using Goldraven.Weapons;
using Goldraven.Prod;
using System;

namespace Goldraven.Weapons
{

	/*
	 * cmcb  2017/10/18
	 * 
	 * RayShooter hits immediately with a raycast,
	 * not when the instantiated missle gets to target distance
	 *
	 */


	public class RayShooter : Shooter
	{
		public Scoreboard scores = null;
		public GameObject missle = null;
		public float AppearDistance = 4.0f;
		// Distance missle appears in front of *camera*
		public GameObject Impact = null;
		public int Damage = 5;
		public float ShotSpeed = 100f;

		// Use this for initialization
		protected override void Start ()
		{
			base.Start ();
		}
	
		// Update is called once per frame
		protected override void Update ()
		{
			base.Update ();
			if (Input.GetButtonDown (fireButton)) {
				if (!pg.Paused) {  
					Aim ();
				}
			}
			if (Input.GetButtonUp (fireButton)) {
				if (!pg.Paused) {  
					Fire ();
					Recover ();
				}
			}
		}

		protected override void Aim () {
			base.Aim ();

		}
		protected override void Fire () {
			base.Fire ();
			Vector3 point = new Vector3 (_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
			Ray ray = _camera.ScreenPointToRay (point);
			RaycastHit hit;
			// Debug.Log (missle);
			if (Physics.Raycast (ray, out hit, float.PositiveInfinity, ~_layermask)) {
				Target target = hit.transform.gameObject.GetComponent<Target> ();

				if (target != null) {
					// Debug.Log ("Target reacted");
					target.ReactToHit (Damage, Impact, hit.collider);
					if (scores != null) {
						scores.AddHit ();
					}

				} else {
					//do nothing
				}

			}
			if (missle != null) {
				// Debug.Log ("Missle fired");
				GameObject _missle = Instantiate (missle, ray.origin + (AppearDistance * ray.direction), Quaternion.LookRotation (ray.direction));
				_missle.SetActive (true);
				ShotLive shot = _missle.GetComponent<ShotLive> ();
				shot.maxDistance = Math.Max( hit.distance, 2000f);
				shot.speed = ShotSpeed;
			}

			if (scores != null) {
				scores.AddShot ();
			}

		}
		protected override void Recover() {
			base.Recover ();

		}


	}
}


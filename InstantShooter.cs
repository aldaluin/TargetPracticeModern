using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using Goldraven.Weapons;
using Goldraven.Prod;

namespace Goldraven.Weapons
{

	/*
	 * cmcb  2017/10/18
	 * 
	 * InstantShooter hits immediately with a raycast.
	 * If it misses it leaves behind a sphere for a few seconds.
	 *
	 */



	public class InstantShooter : Shooter
	{
		public float HitLinger = 2;
		public Scoreboard scores = null;
		public int Damage = 5;

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
				if (!EventSystem.current.IsPointerOverGameObject ()) {  // not over a UI sprite
					Aim ();
				}
			}
			if (Input.GetButtonUp (fireButton)) {
				if (!EventSystem.current.IsPointerOverGameObject ()) {  // not over a UI sprite
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
			if (Physics.Raycast (ray, out hit, float.PositiveInfinity, ~_layermask)) {
				Target target = hit.transform.gameObject.GetComponent<Target> ();
				if (target != null) {
					target.ReactToHit (Damage, hit.collider);
					if (scores != null) {
						scores.AddHit ();
					}
				} else {
					StartCoroutine (SphereIndicator (hit.point));
				}
				if (scores != null) {
					scores.AddShot ();
				}
			}

		}

		protected override void Recover() {
			base.Recover ();

		}


		private IEnumerator SphereIndicator (Vector3 pos)
		{
			GameObject sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			sphere.transform.position = pos;
			yield return new WaitForSeconds (HitLinger);
			Destroy (sphere);

		}


	}
}


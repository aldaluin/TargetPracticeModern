using UnityEngine;
using System.Collections;
using Goldraven.Prod;

namespace Goldraven.Weapons
{
	
	[System.Serializable]
	public struct TargetZone
	{
		public int hitMultiplier;
		public GameObject deathEffect;
		public Vector3 deathEffectOffset;
		public Vector3 deathEffectRotation;
	}

	public class Target : MonoBehaviour
	{
		public bool terminatorTarget = false;
		public int health = 5;
		public float waitAfterDeath = 6.0f;
		public Transform anchor = null;
		public TargetZone headShot;
		public TargetZone centerShot;
		public TargetZone torsoShot;

		//[SerializeField] private GameObject deathEffect = null;
		//[SerializeField] private Vector3 deathEffectOffset = new Vector3(0f, 0f, 0f);

		void Start ()
		{
		}

		private TargetZone  FindHitZone (Collider coll)
		{
			if (coll.gameObject.name == "Head") {
				return headShot;
			} else if (coll.gameObject.name == "Center") {
				return centerShot;
			} else {
				return torsoShot;
			}
		}

		public void ReactToHit (int damage)
		{
			TargetZone zone = torsoShot;
			health -= damage * zone.hitMultiplier;
			transform.position = transform.TransformPoint (Vector3.up * 1.0f);
			if (health <= 0) {
				StartCoroutine (Die (zone));
			}
		}

		public void ReactToHit (int damage, Collider coll)
		{
			TargetZone zone = FindHitZone (coll);
			health -= damage * zone.hitMultiplier;
			transform.position = transform.TransformPoint (Vector3.up * 1.0f);
			if (health <= 0) {
				StartCoroutine (Die (zone));
			}
		}

		public void ReactToHit (int damage, GameObject hitdisplay, Collider coll)
		{
			TargetZone zone = FindHitZone (coll);
			health -= damage * zone.hitMultiplier;
			GameObject _hitdisplay = Instantiate (hitdisplay, transform.TransformPoint (zone.deathEffectOffset), Quaternion.LookRotation(zone.deathEffectRotation));
			_hitdisplay.SetActive (true);
			if (health <= 0) {
				StartCoroutine (Die (zone));
			}
		}


		private IEnumerator Die (TargetZone zone)
		{
			yield return new WaitForSeconds (waitAfterDeath / 2);
			if (zone.deathEffect != null) {
				GameObject _deathEffect = Instantiate (zone.deathEffect, transform.TransformPoint (zone.deathEffectOffset), Quaternion.LookRotation(zone.deathEffectRotation));
				_deathEffect.SetActive (true);
			}
			yield return new WaitForSeconds (waitAfterDeath / 2);

			Destroy (this.gameObject);
			if (terminatorTarget) {
				Application.Quit ();
				#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
				#endif
			}
		}

	}
}

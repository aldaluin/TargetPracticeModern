using UnityEngine;
using System.Collections;
using Goldraven.Prod;
using Goldraven.Weapons;
using Invector.EventSystems;
using System.Xml;

namespace Goldraven.IvWeapons
{


	public class Iv_Target : MonoBehaviour, vIDamageReceiver
	{

		// All that's needed to implement vIDamageReceiver
		public void TakeDamage (vDamage damage, bool hitReaction = true)
		{
			// Debug.Log ("Iv target taking damage");
			// If you're not taking damage, make sure the Enemy tag is set on the ShooterManager
			ReactToHit (damage.damageValue);
		}

		// Vars and properties
		public static Scoreboard scores;
		public Iv_Target baseTarget;
		public bool terminatorTarget = false;
		public int health = 5;
		public int hitMultiplier;
		public GameObject deathEffect;
		public Vector3 deathEffectOffset;
		public Vector3 deathEffectRotation;

		public float waitAfterDeath = 6.0f;
		public Transform anchor = null;

		//[SerializeField] private GameObject deathEffect = null;
		//[SerializeField] private Vector3 deathEffectOffset = new Vector3(0f, 0f, 0f);

		// Methods
		void Start ()
		{
			scores = GameObject.FindWithTag ("Scoreboard").GetComponent<Scoreboard> ();
		}

		public void ReactToHit (int damage)
		{
			health -= damage * hitMultiplier;
			scores.AddHit();
			if ((baseTarget != this) && (baseTarget != null)) {
				baseTarget.ReactToHit (damage);
			} else {
				transform.position = transform.TransformPoint (Vector3.up * 1.0f);
			}
			if (health <= 0) {
				StartCoroutine (Die (baseTarget));
			}
		}

		private IEnumerator Die (Iv_Target victim)
		{
			scores.AddKill ();
			yield return new WaitForSeconds (waitAfterDeath / 2);
			if (deathEffect != null) {
				GameObject _deathEffect = Instantiate (deathEffect, transform.TransformPoint (deathEffectOffset), Quaternion.LookRotation (deathEffectRotation));
				_deathEffect.SetActive (true);
			}
			yield return new WaitForSeconds (waitAfterDeath / 2);

			Destroy (victim.gameObject);
			if (terminatorTarget) {
				Application.Quit ();
				#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
				#endif
			}
		}

	}
}

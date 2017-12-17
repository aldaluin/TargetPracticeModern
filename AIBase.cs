#define CONTROLLERCHECK
// #define CONTROLLERSCRIPTCHECK
#define UNITYNAV
// #define ASTARNAV
// Use exactly one define of [UNITYNAV ASTARNAV}


/*
 * Base class for AI-prefix controller adapters
 * cm 20170801 ish
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITYNAV
using UnityEngine.AI;  //NavMeshAgent
#endif
#if ASTARNAV
using Pathfinding;
#endif

namespace Goldraven.AI {

	[RequireComponent (typeof(Animator))]
	#if UNITYNAV
	[RequireComponent (typeof(UnityEngine.AI.NavMeshAgent))]
	#endif
	#if ASTARNAV
	[RequireComponent (typeof(Seeker))]
	#endif
	[RequireComponent (typeof(Rigidbody))]
	[RequireComponent (typeof(ActBase))]


public abstract class AIBase {

		protected GameObject gameagent;
		protected Animator ctlagent;
		#if UNITYNAV
		public  NavMeshAgent navagent { get; protected set; }
		#endif
		#if ASTARNAV
		public  Seeker navagent { get; protected set; }
		#endif
		protected Rigidbody body;

		protected  bool actScript { get; private set; }
		internal  Vector3 moveDestination { get; set; }
		public string name { get { return gameagent.name; }}

		public AIBase(GameObject AICharacter) {
			// constructor
			gameagent = AICharacter ;
			ctlagent = AICharacter.GetComponent<Animator> (); 
			#if CONTROLLERCHECK
			if (!VerifyAnimatorController ()) {
				Debug.Log ("!! Animator Controller *not* verified on startup !!");
			}
			#endif 
			#if CONTROLLERSCRIPTCHECK
			if (VerifyAnimatorControllerScript ()) {
				actScript = true;
			} else {
				Debug.Log ("!! Animator Controller Script not verified on startup !!");
				actScript = false;
			}
			#else 
			actScript = false;
			#endif 
			#if UNITYNAV
			navagent = AICharacter.GetComponent<NavMeshAgent> ();
			#endif
			#if ASTARNAV
			navagent = AICharacter.GetComponent<Seeker> ();
			navagent.pathCallback += OnPathReady;
			#endif
			body = AICharacter.GetComponent<Rigidbody> ();

		}
	
		#if ASTARNAV
			public void OnPathReady (Path p) {
			Debug.Log ("Yay, we got a path back. Did it have an error? "+p.error);
		}
		#endif

		protected virtual  bool VerifyAnimatorControllerScript() {
			return false;
		}

		protected virtual bool VerifyAnimatorController() {
			return false;
		}

		// abstract action methods
		public abstract void Walk ();
		public abstract void Run ();
		public abstract void Stand();
		public abstract void Die();


	}
}

#define UNITYNAV
// #define ASTARNAV
// Use exactly one define of [UNITYNAV ASTARNAV}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITYNAV
using UnityEngine.AI;  //NavMeshAgent
#endif
#if ASTARNAV
using Pathfinding
#endif

namespace Goldraven.AI {



public class AICharacterController : AIBase {

		BasicBehaviour actScriptBasic = null;
		MoveBehaviour actScriptMove = null;

		public AICharacterController(GameObject AICharacter) : base(AICharacter ) {
			if (actScript) {
				actScriptBasic = AICharacter.GetComponent<BasicBehaviour> ();
				actScriptMove = AICharacter.GetComponent<MoveBehaviour> ();
				//actScriptAim = GetComponent<AimBehaviour> ();
				//actScriptFly = GetComponent<FlyBehaviour> ();
			}

	}
	

		protected sealed override bool VerifyAnimatorController() {
			return (ctlagent.runtimeAnimatorController.name == "CharacterController");
		}

		protected sealed override bool VerifyAnimatorControllerScript() {
			return false;
			// The behavior scripts are tied to input and not appropriate for AI.   Oops.
			// return (gameagent.GetComponent<BasicBehaviour> () != null & gameagent.GetComponent<MoveBehaviour> () != null); 
		}

		// overridden action methods
		public override void Stand (){
			ctlagent.SetFloat ("Speed", 0f);
		}

		public override void Walk () {
			#if UNITYNAV
			if (navagent.SetDestination (moveDestination)) {
				ctlagent.SetFloat ("Speed", 0.3f);
			} else {
				Debug.Log ("Not able to walk");
			}
			#endif
			#if ASTARNAV
			navagent.StartPath(gameagent.transform.position, moveDestination);
			ctlagent.SetFloat ("Speed", 0.3f);
			#endif
		}

		public override void Run () {
			#if UNITYNAV
			if (navagent.SetDestination (moveDestination)) {
				ctlagent.SetFloat ("Speed", 1.0f);
			} else {
				Debug.Log ("Not able to run");
			}
			#endif
			#if ASTARNAV
			navagent.StartPath(gameagent.transform.position, moveDestination);
			ctlagent.SetFloat ("Speed", 0.3f);
			#endif
		}

		public override void Die () {
			// Not available
			// ctlagent.setTrigger("Death");
		}



}
}

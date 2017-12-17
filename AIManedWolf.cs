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
#endif

namespace Goldraven.AI {



	public class AIManedWolf : AIBase {

		ManedWolfAI actScriptWolf = null;


		public AIManedWolf(GameObject AICharacter) : base(AICharacter ) {
			if (actScript) {
				actScriptWolf = AICharacter.GetComponent<ManedWolfAI> ();
			}

	}
	
		protected sealed override bool VerifyAnimatorController() {
			return (ctlagent.runtimeAnimatorController.name == "ManedWolfAC");
		}
	
		protected sealed override bool VerifyAnimatorControllerScript() {
			return (gameagent.GetComponent<ManedWolfAI> () != null); 
		}

		// overridden action methods
		public override void Stand (){
			ctlagent.SetFloat ("Forward", 0f);
		}

		public override void Walk () {
			#if UNITYNAV
			navagent.SetDestination(moveDestination);
			ctlagent.SetFloat ("Forward", 0.5f);
			#endif
			#if ASTARNAV
			navagent.StartPath(gameagent.transform.position, moveDestination);
			ctlagent.SetFloat ("Forward", 0.5f);
			#endif
		}

		public override void Run () {
			#if UNITYNAV
			navagent.SetDestination(moveDestination);
			ctlagent.SetFloat ("Forward", 1.0f);
			#endif
			#if ASTARNAV
			navagent.StartPath(gameagent.transform.position, moveDestination);
			ctlagent.SetFloat ("Forward", 1.0f);
			#endif
		}

		public override void Die () {
			ctlagent.SetTrigger("Death");
		}



	}
}

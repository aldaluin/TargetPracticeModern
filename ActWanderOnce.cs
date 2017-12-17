using System ;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Goldraven.AI {

	/* Test implementation of a non-atomic action
	 * Wanders randomly wanderDistance at a time
	 * cmcb 2017/08/08
	 */

	public class ActWanderOnce : ActBase {

		public float wanderDistance = 40.0f;  // change setparm to allow overriding this default value.
		private AIFactory aifactory;
		protected ActBase thisMove;


		public ActWanderOnce(AIBase ai, Transform loc, AIFactory aifac) : base (ai, loc) {
			// constructor
			aifactory = aifac;
		}

		public override bool setParm (string key, string val)
		{
			throw new NotImplementedException ();
			return false;
		}

		public override bool setParm (string key, Vector3 val)
		{
			throw new NotImplementedException ();
			return false;
		}

		public override bool setParm (string key, float val)
		{
			if (key == "distance") {
				wanderDistance = val;
				return true;
			} else {
				throw new NotImplementedException ();
				return false;
			}
		}

		public override bool setParm (string key, GameObject  val)
		{
			throw new NotImplementedException ();
			return false;
		}


		public override void startAction(){
			Vector3 targetPoint = myLocation.position + UnityEngine.Random.onUnitSphere * wanderDistance;
			thisMove = aifactory.MakeAct(aiagent, "move", myLocation ); // move overrides y so we don't have to
			thisMove.setParm ("target", targetPoint);
			thisMove.startAction ();
		}

		public  override void continueAction(){
			if (thisMove.finished ) {
				finished = true;
				successful = thisMove.successful;
			} else {
				thisMove.continueAction ();
			}
		}
	}
}

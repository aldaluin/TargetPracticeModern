using System ;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Goldraven.AI {

	/* Chases a target goblin until close enough to attack
	 * or the goblin escapes
	 * 
	 * cmcb 2017/08/22
	 */

	public class ActHuntObject : ActBase {

		public float lostDistance = 200.0f;  // change setparm to allow overriding this default value.
		public float lostAngle = 90.0f; // 90 = 45 degrees each way
		public GameObject target;
		private AIFactory aifactory;
		protected ActBase thisMove;


		public ActHuntObject(AIBase ai, Transform loc, AIFactory aifac) : base (ai, loc) {
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
				lostDistance = val;
				return true;
			} else if (key == "angle") {
				lostAngle = val;
				return true;
			} else {
				throw new NotImplementedException ();
				return false;
			}
		}

		public override bool setParm (string key, GameObject  val)
		{
			if (key == "target") {
				target = val;
				return true;
			} else {
				throw new NotImplementedException ();
				return false;
			}
		}

		public override void startAction(){
			Vector3 targetPoint = target.transform.position ;
			thisMove = aifactory.MakeAct(aiagent, "move", myLocation ); // move overrides y so we don't have to
			thisMove.setParm ("target", targetPoint);
			thisMove.startAction ();
		}

		public  override void continueAction(){
			if (thisMove.finished) {
				finished = true;
				successful = true;
			} else if ( Vector3.Distance(target.transform.position, myLocation.position) > lostDistance 
				| Vector3.Angle(target.transform.position - myLocation.position, myLocation.forward) > lostAngle/2f) {
				finished = true;
				successful = false;
			} else {
				thisMove.continueAction ();
			}
		}
	}
}

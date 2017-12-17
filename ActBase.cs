#define LOGMOVES
// #define LOGALLMOVES

using System ;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Specialized;
using System.Threading;

namespace Goldraven.AI {

	/*
	 * Minimal implementation of actions:
	 * Stand and Move only
	 * cmcb 2017/08/07
	 */

	public abstract class ActBase {

		// internal properties
		protected AIBase aiagent {get; }
		protected Transform myLocation { get; }
		public  bool finished {get; protected set; }
		public  bool successful { get; protected set; }

		// abstract methods
		public abstract bool  setParm (string key, string val) ;
		public abstract bool  setParm (string key, Vector3 val);
		public abstract bool  setParm (string key, float val);
		public abstract bool  setParm (string key, GameObject val);
		public abstract void startAction ();
		public abstract void continueAction ();


		// concrete methods
		public ActBase(AIBase ai, Transform loc) {
			// constructor
			aiagent = ai;
			myLocation = loc;
			finished = false;
			successful = false;
		}
	}

	public class ActStand : ActBase {

		// Causes agent to idle for a length of time

		private float standTime = 5;
		private float timerTime = 0;

		public ActStand (AIBase ai, Transform loc) : base (ai,loc ){
			// constructor

		}

		public override bool setParm (string key, string val)
		{
			if (key == "timer") {
				float fval;
				if (float.TryParse (val, out fval)) {
					standTime = fval;
					return true;
				} else {
					throw new ArgumentException ();
					return false;
				}
			} else {
				throw new NotImplementedException ();
				return false;
			}
		}

		public override bool setParm (string key, Vector3 val)
		{
			throw new NotImplementedException ();
			return false;
		}

		public override bool setParm (string key, float val)
		{
			if (key == "timer") {
				standTime = val;
				return true;
			} else {
				throw new NotImplementedException ();
				return false;
			}
		}

		public override bool setParm (string key, GameObject val)
		{
			throw new NotImplementedException ();
			return false;
		}

		public override void startAction(){
			timerTime = 0f;
			aiagent.Stand();

		}

		public override void continueAction() {
			if (timerTime >= standTime) {
				finished = true;
				successful = true;
			} else {
				timerTime += Time.deltaTime;
			}
		}
	}

	public class ActMove : ActBase {

		// Moves agent ON THE GROUND
		// y dimension of destination is overriden by terrain

		protected readonly float STOP_DISTANCE = 5f; // How close counts as being there?
		protected readonly float ABORT_DISTANCE = 5f; // Abort move if you cant go this far in timeout time
		protected readonly float ABORT_TIMEOUT = 3F; // Abort move if you cant move in this amount of real time

		private Vector3 lastPosition;
		private float timerTime = 0f;
		private TerrainData mainTerrain;

		#if(LOGMOVES || LOGALLMOVES)
		private Vector3 destination;
		#endif 

		public ActMove (AIBase ai, Transform loc) : base (ai,loc ){
			// constructor
			lastPosition = new Vector3(0f,0f,0f);
			mainTerrain = Terrain.activeTerrain.terrainData ;

			#if(LOGMOVES || LOGALLMOVES)
			Debug.LogFormat("**ActBase {0} **", "ActMove constructed");
			#endif 

		}

		public override bool setParm (string key, string val)
		{
			if (key == "target") {
				string[] str = val.Split (',');
				if (str.Length != 3) {
					throw new ArgumentException ();
					return false;
				}
				float x, y, z;
				if (float.TryParse (str [0], out x) & float.TryParse (str [1], out  y) & float.TryParse (str [2], out z)) {
					#if(LOGMOVES || LOGALLMOVES)
					destination = new Vector3 (x, mainTerrain.GetHeight((int) x, (int) z), z);
					#endif 
					aiagent.moveDestination = new Vector3 (x, mainTerrain.GetHeight((int) x, (int) z), z);
					return true;
				} else{
					throw new ArgumentException ();
					return false;
				}
			} else {
				throw new NotImplementedException ();
				return false;
			}
		}

		public override bool setParm (string key, Vector3 val)
		{
			if (key == "target") {
				val.y = mainTerrain.GetHeight ((int)val.x, (int)val.z);
				#if(LOGMOVES || LOGALLMOVES)
				destination = val;
				#endif 
				aiagent.moveDestination = val ;
				return true;
			} else {
				throw new NotImplementedException ();
				return false;
			}
		}

		public override bool setParm (string key, float val)
		{
			throw new NotImplementedException ();
			return false;
		}

		public override bool setParm (string key, GameObject val)
		{
			throw new NotImplementedException ();
			return false;
		}

		public override void startAction(){
			aiagent.Walk ();
			#if(LOGMOVES || LOGALLMOVES)
			Debug.LogFormat("**ActBase {0} {1} to {2}. **", aiagent.name, "Starting walk", destination  );
			#endif 


		}

		public override void continueAction() {
			if (HaveArrived ()) {
				finished = true;
				successful = true;
				#if(LOGMOVES || LOGALLMOVES)
				Debug.LogFormat("**ActBase {0} ({1},{2}) **", "Finished walking", finished, successful  );
				#endif 
			}else if (isStuck() ) {
				finished = true;
				successful = false;
				#if(LOGMOVES || LOGALLMOVES)
				Debug.LogFormat("**ActBase {0} ({1},{2}) **", "Finished walking", finished, successful  );
				#endif 
			} else {
				// keep walking
				#if(LOGALLMOVES)
				Debug.LogFormat("**ActBase {0} ({1}) **", "Still walking", finished );
				#endif 

			}
		}

		protected bool isStuck() {
			if (timerTime >= ABORT_TIMEOUT) {
				float distance = Vector3.Distance (myLocation.position, lastPosition);
				if (distance <= ABORT_DISTANCE) {
					return true;
				} else {
					timerTime = 0;
					lastPosition = myLocation.position;
					return false;
				}
			} else {
				timerTime += Time.deltaTime;
				return false;
			}

		}
		protected bool HaveArrived() {
			float distance = Vector3.Distance(myLocation.position, aiagent.moveDestination);
			/*
			Debug.Log ("--- Distance: " + distance + " --- " +
				" dx=" + (dest.x - myLocation.position.x) +
				" dy=" + (dest.y - myLocation.position.y) +
				" dz=" + (dest.z - myLocation.position.z) +
				" self=( "+myLocation.position.x+ ", " + myLocation.position.y + ", " +myLocation.position.z + ") " +
				" dest=( " + dest.x + ", " + dest.y + ", " + dest.z +")"
			);
			*/
			if (distance <= STOP_DISTANCE) {
				return true;
			}
			else {
				return false;
			}
		}

	}


	/*

		// constants
		protected readonly float MIN_FOLLOW_DISTANCE = 5f; // How close counts as being there?
		protected readonly float MAX_FOLLOW_DISTANCE = 30f; // How far counts as being gone?


		// private(ish) methods

		internal void startTimer() {
			actualTimer = timer;
		}

		internal void updateTimer() {
			actualTimer -= Time.deltaTime;
		}

		internal void startAction(ActionBase  newact){
			actCurrent.Push(newact);
			finished = false;
		}

		internal void finishAction (){
			actCurrent.Pop ();
		}

		internal void doAction (ActionBase index){
			switch (index) {
			case ActionBase.stand:
				doStandIdle ();
				break;
			case ActionBase.pause:
				doPause ();
				break;
			case ActionBase.move:
				doMove ();
				break;
			case ActionBase.follow:
				doFollow ();
				break;
			case ActionBase.patrol:
				doPatrol ();
				break;
			}

		}
			

		// public methods


		// action properties

		public Transform targetObject { get; set; }
		public Vector3 targetPoint { get; set; }
		public float timer { get; set; }       // amount the timer is set for in seconds
		public float actualTimer { get; internal set; }      // amount left as the timer counts down
		public WaypointSet waypoints;

			



		public void Pause() {
			// requires timer set
			startTimer();
			aiagent.Stand ();
			startAction (ActionBase.pause);
		}

		protected void doPause() {
			updateTimer ();
			if (actualTimer > 0f) {
				// continue pausing
			} else {
				finished = true;
			}
		}

		public void Move() {
			// requires targetPoint set
			aiagent.moveDestination = targetPoint;
			aiagent.Walk ();
			startAction (ActionBase.move);
		}

		protected void doMove() {
			if (HaveArrived ()) {
				finished = true;
			} else {
				// keep walking 
			}
		}


		public void Follow(){
			// requires targetObject set
			aiagent.moveDestination = targetObject.position;
			aiagent.Walk ();
			startAction (ActionBase.follow);
		}

		protected void doFollow(){
			if (CloseEnough ()) {
				aiagent.Stand ();
			} else if (FarEnough ()) {
				aiagent.Run ();
			}else {
				aiagent.Walk ();
			}
		}

		public void Patrol(){
			targetPoint = waypoints.waypoints [0];
			startAction (ActionBase.patrol);
			Move ();
		}

		protected void doPatrol() {
			targetPoint = waypoints.getNextWaypoint (targetPoint);
			Move ();
		}

		protected bool CloseEnough() {
			float distance = Vector3.Distance (myLocation.position, targetObject.position);
			if (distance <= MIN_FOLLOW_DISTANCE) {
				return true;
			} else {
				return false;
			}
		}

		protected bool FarEnough() {
			float distance = Vector3.Distance(myLocation.position, targetObject.position);
			if (distance >= MAX_FOLLOW_DISTANCE) {
				return true;
			}
			else {
				return false;
			}
		}




		void Start() {

		}

		void Update() {
			ActionBase  currentaction = actCurrent.Peek ();
			doAction(currentaction );
		}
}
*/

}


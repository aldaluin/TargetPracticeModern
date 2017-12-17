using UnityEngine;
using System.Collections;
using UnityEngine.AI;

// [RequireComponent (typeof(AICharacter))]
[RequireComponent (typeof(NavMeshAgent))]

public class AIBasic : MonoBehaviour {

/*

	*** Early AI attempt, depricated, included for reference ***

	// cmcb  2017/05/30

	// Basic AI is mostly about whether the npc needs to rest.

	private AICharacter ctlagent;
	private NavMeshAgent navagent;

	private AISleepRest sr;
	private AIStandReady st;
	private AIPatrolLoop pl;
	private AIPatrolOnce po;
	private AIGuardRandom gd;

	public Transform[] waypoints;  // array of waypoints used by this npc
	public GameObject[] team;      // array of friendly npc's  

	private AIAction currentAction = null;    // what to do next (or finish doing) -- * init in Start
	private AIAction preferredAction = null;  // what self would rather be doing -- * init in Start

	private class AISleepRest {

		private AICharacter agent;
		private float alert = 5f;       // inverse chance of resting; restored by sleep
		private float alertTimer = 0f;
		private float alertInterval = (60f);
		private float stamina = 10f;    // activity level available
		public float maxStamina = 10f;  // activity level available when fully rested
		private bool  isTired = false;  // try to rest?
		private bool justWoke = false;
		private bool isAsleep = false;
		private float sleepTimer = 0f;
		private float sleepInterval = (5f * 60f);
		private bool isResting = false;
		private bool wasResting = false;
		private float restTimer = 0f;     // rest time used
		private float restInterval = 15f;  // seconds to rest before thinking

		public void SetAnimatorAgent(AICharacter anim) {
			// TODO: Put this in a proper constructor
			agent = anim;
		}
			
		public float getAlert() {
			return alert;
		}

		public float getStamina() {
			return stamina;
		}

		public bool Update() {
			if (isAsleep) {
				if (sleepTimer < sleepInterval) {
					sleepTimer += Time.deltaTime;
					return false;
				}
				else if (alert < Random.Range(0, 1))  {
					sleepTimer = 0;
					return false;
				}
				else {
					WakeUp ();
					return true;
				}
			}
			else if (isResting) {
				if (restTimer < restInterval) {
					restTimer += Time.deltaTime;
					return false;
				}
				else if (Random.Range(0, maxStamina) > stamina)  {
					StartRest();
					return false;
				}
				else {
					StopRest ();
					return true;
				}
			}
			else {
				wasResting = false;
				justWoke = false;
				return true;
			}

		}

		public bool JustWoke(){
			return justWoke;
		}

		public bool WasResting(){
			// Debug.Log ("AIS was resting: " + wasResting);
			return wasResting;
		}

		public bool TryToSleep() {
			// Debug.Log ("Trying to sleep");

			if (isTired) {
				GotoSleep ();
				return true;
			} else {
				isTired = true;
				return false;
			}
		}

		private void GotoSleep() {
			// Debug.Log ("Going to sleep");
			agent.Sleep();
			isAsleep = true;
			isTired = false;
			alert += Random.Range(0, 1);
		}

		private void WakeUp() {
			// Debug.Log ("Waking up");
			justWoke = true;
			isAsleep = false;
			alertTimer = 0f;
			agent.SitDown();
		}

		public  void StartRest() {
			// recover from exercise
			// Debug.Log ("Starting rest");
			restTimer = 0;
			if (alert < 0.2f) {
				agent.Lie();
				AddStamina(1.0f);
			}
			else if (alert < 0.5f) {
				agent.SitDown();
				AddStamina(0.5f);
			}
			else {
				agent.Idle();  // stand still
				AddStamina(0.25f);
			}
			alert += 0.1f;
			isResting = true;
		}

		private void StopRest() {
			// Debug.Log ("Stopping rest");
			isResting = false ;
			wasResting = true;
			alertTimer += Time.deltaTime;
			if (alertTimer >= alertInterval) {
				alert *= 0.75f;
				alertTimer = 0;
			}
		}


		private void AddStamina(float amount) {
			// based on rest interval, not every update
			stamina += amount;
			if (stamina > maxStamina) stamina = maxStamina;
			// Debug.Log ("Stamina (+): " + stamina);
		}

		public  void LoseStamina(float amount) {
			// lose some every update
			stamina -= (amount * Time.deltaTime / 30f); // should lose two (amount) per minute
			if (stamina < 0f) stamina = 0f;
			// if (stamina < 3f) Debug.Log ("Stamina: (-) " + stamina);
		}

	}

	private abstract class AIAction{

		protected AICharacter ctlagent;
		protected NavMeshAgent navagent;
		protected AISleepRest restagent;
		protected Transform mylocation;
		protected static bool finished = true;

		protected readonly float STOP_DISTANCE = 5f; // How close counts as being there?

		public abstract void StartAction () ;

		public abstract void ContinueAction () ;

		public void SetAnimatorAgent(AICharacter anim) {
			// TODO: Put this in a proper constructor
			ctlagent = anim;
		}

		public void SetNavigatorAgent(NavMeshAgent navigator) {
			// TODO: Put this in a proper constructor
			navagent = navigator;
		}

		public void SetRestAgent(AISleepRest rester){
			// TODO: Put this in a proper constructor
			restagent = rester;
		}

		public void SetSelfTransform(Transform place) {
			// TODO: Put this in a proper constructor
			mylocation = place;
		}

		public static bool getFinished() {
			// Debug.Log ("AIA move is finished: " + finished );
			return finished;
		}

		public void StandIdle(){
			// Debug.Log ("...standing idle...");
			ctlagent.Idle();
		}

		protected void MoveTo(Vector3 dest) {
			// Debug.Log ("...moving (or not)...");
			if (HaveArrived(dest)) {
				// Debug.Log ("...have arrived...");
				ctlagent.Idle();
			}
			navagent.SetDestination(dest);
			if(restagent.getAlert() > 2f) {
				ctlagent.GallopFast();
				ctlagent.Move ();
				restagent.LoseStamina(3f);
			}
			else if (restagent.getAlert() > 1.5f) {
				ctlagent.Gallop();
				ctlagent.Move ();
				restagent.LoseStamina(1.5f);
			}
			else if (restagent.getAlert() > 1f) {
				ctlagent.Canter();
				ctlagent.Move ();
				restagent.LoseStamina(.5f);
			}
			else if (restagent.getAlert() > 0.75f) {
				ctlagent.Trot();
				ctlagent.Move ();
				restagent.LoseStamina(.25f);
			}
			else if (restagent.getAlert() > 0.1f) {
				ctlagent.Walk();
				ctlagent.Move ();
			}
			else {
				// Debug.Log ("...starting rest...");
				restagent.StartRest();
			}
		}

		protected bool HaveArrived(Vector3 dest) {
			float distance = Vector3.Distance(mylocation.position, dest);
			// Debug.Log ("--- Distance: " + distance);
			if (distance <= STOP_DISTANCE) {
				return true;
			}
			else {
				return false;
			}
		}


	}

	private class AIStandReady : AIAction {

		private float readyTimer = 0f;
		private float readyInterval = (5f);

		public override void StartAction() {
			finished = false;
			readyTimer = 0f;
			// Debug.Log ("<<< Starting stand");
			ContinueAction ();
		}

		public override void ContinueAction() {
			if (readyTimer >= readyInterval) {
				// Debug.Log (">>> Ending stand");
				finished = true;
			} else {
				readyTimer += Time.deltaTime;
				StandIdle ();
				finished = false;
			}
		}

	}

	private abstract class AIPatrol : AIAction {

		protected Transform[] waypoints;

		protected int currentWaypoint = 0;   // the index of the waypoint self is moving toward

		protected abstract void Patrol ();

		public override void StartAction() {
			// Debug.Log ("<<< Starting patrol");
			finished = false;
			currentWaypoint = 0;
			Patrol ();
		}

		public override void ContinueAction() {
			// Debug.Log ("<<< Continuing patrol >>>");
			Patrol();
		}

		public void SetWaypoints(Transform[] points){
			// TODO: Put this in a proper constructor
			waypoints = points;
		}
			
	}

	private class AIPatrolLoop : AIPatrol {

		protected override void Patrol() {
			// move sequentially between waypoints, looping
			// chance of resting at each waypoint
			if ( HaveArrived(waypoints[currentWaypoint].position)) {
				currentWaypoint++;
				if (currentWaypoint >= waypoints.Length){
					currentWaypoint = 0;
				}
				if (restagent.getStamina() < 3f | Random.Range(1,10) < 3) {
					restagent.StartRest();
				}
				else {
					MoveTo(waypoints[currentWaypoint].position);			}
			}
			else {
				MoveTo(waypoints[currentWaypoint].position);
			}
		}

	}

	private class AIPatrolOnce : AIPatrol {

		protected override void Patrol() {
			// move sequentially between waypoints, then rest
			if ( HaveArrived(waypoints[currentWaypoint].position)) {
				currentWaypoint++;
				if (currentWaypoint >= waypoints.Length){
					finished = true;
					restagent.StartRest();
				}
				else {
					MoveTo(waypoints[currentWaypoint].position);
				}
			}
			else {
				MoveTo(waypoints[currentWaypoint].position);
			}
		}

	}

	private class AIGuard : AIAction {

		private Vector3 guarded;
		private Vector3 wanderpoint = new Vector3();
		protected  float range = 0;

		public void SetGuarded(Vector3 guardee) {
			// TODO: Put this in a proper constructor
			guarded = guardee;
		}

		public void SetRange(float distance) {
			// TODO: Put this in a proper constructor
			range = distance;
		}

		protected void Guard() {
			if (range == 0) {
				if (HaveArrived (guarded)) {
					StandIdle ();
				} else {
					MoveTo (guarded);
				}
			} else if (HaveArrived (wanderpoint)) {
				ChooseWanderpoint ();
				MoveTo(wanderpoint);
			} else {
				MoveTo(wanderpoint);
			}
		}

		protected void ChooseWanderpoint() {
			wanderpoint = (Random.onUnitSphere * range) + guarded;
			wanderpoint.y = guarded.y;
			}

		public override void StartAction() {
			// Debug.Log ("AIG starting action");

			finished = false;
			ChooseWanderpoint ();
			Guard ();
			// Debug.Log ("AIG started action");

		}

		public override void ContinueAction() {
			Guard ();
		}



	}

	private class AIGuardRandom : AIGuard {
		
		private Transform[] waypoints;

		private int currentWaypoint = 0;   // the index of the waypoint self is guarding

		public override void StartAction() {
			// Debug.Log ("AIGR starting action");
			currentWaypoint = Random.Range (0, waypoints.Length - 1);
			SetGuarded (waypoints [currentWaypoint].position);
			range = 15;
			base.StartAction ();
			// Debug.Log ("AIGR started action");

		}

		public override void ContinueAction() {
			base.ContinueAction ();
		}

		public void SetWaypoints(Transform[] points){
			// TODO: Put this in a proper constructor
			waypoints = points;
		}


	}



	//TODO: Replace all these damn hardcoded numbers with enums or constants


	// Use this for initialization
	void Start () {
		// Debug.Log ("Starting Wolf");
		ctlagent = GetComponent<AICharacter>();
		navagent = GetComponent<NavMeshAgent>();
		sr = new AISleepRest();
		sr.SetAnimatorAgent (ctlagent);
		st = new AIStandReady ();
		st.SetAnimatorAgent (ctlagent);
		st.SetNavigatorAgent (navagent);
		st.SetSelfTransform (transform);
		st.SetRestAgent (sr);
		pl = new AIPatrolLoop ();
		pl.SetAnimatorAgent (ctlagent);
		pl.SetNavigatorAgent (navagent);
		pl.SetWaypoints (waypoints);
		pl.SetSelfTransform (transform);
		pl.SetRestAgent (sr);
		po = new AIPatrolOnce ();
		po.SetAnimatorAgent (ctlagent);
		po.SetNavigatorAgent (navagent);
		po.SetWaypoints (waypoints);
		po.SetSelfTransform (transform);
		po.SetRestAgent (sr);
		gd = new AIGuardRandom ();
		gd.SetWaypoints (waypoints);
		gd.SetAnimatorAgent (ctlagent);
		gd.SetNavigatorAgent (navagent);
		gd.SetSelfTransform (transform);
		gd.SetRestAgent (sr);
		currentAction = st;
		AIAction[] possibleActions = { pl, po, po, gd };
		preferredAction = RandomPreference(possibleActions );
		// Debug.Log ("Started Wolf");
	}
	
	// Update is called once per frame
	void Update () {
		if (sr.Update ()) {
			if (sr.JustWoke ()) {
				// Debug.Log ("UP: Just woke");
				// do nothing
			} else if (sr.WasResting() | AIAction.getFinished()){
				// Debug.Log ("UP: Starting doing");
				StartDoing ();
			} else {
				// Debug.Log ("UP: Keeping doing");
				KeepDoing ();
			}
		}
		// else do nothing
	}

	private void KeepDoing() {
		// keep on keeping on...

		// Debug.Log ("...keeping doing...");
		currentAction.ContinueAction ();
	}

	private void StartDoing() {
		// finish whatever is current...
		// ...or start something new

		// alters global currentAction

		int diceroll = Random.Range (1, 6) + Random.Range (1, 6);
		// Debug.Log ("Starting doing...(" + diceroll + ")");

		switch (diceroll) {
		case 12:
			currentAction = st; // idle
			if (! sr.TryToSleep ())
				currentAction.StartAction ();
			break;
		case 2:
		case 3:
		case 11:
			currentAction = pl; // start new patrol
			currentAction.StartAction ();
			break;
		case 4:
			currentAction = po; // start patrol once
			currentAction.StartAction ();
			break;
		case 10:
			currentAction = gd; // start guard
			currentAction.StartAction ();
			break;
		case 5:
		case 9:
			currentAction = preferredAction;
			currentAction.StartAction ();
			break;
		case 6:
		case 7:
		case 8:
			// continue/resume prior action
			if (AIAction.getFinished ()) {
				currentAction = preferredAction;
				currentAction.StartAction ();
			} else {
				currentAction.ContinueAction ();
			}
			break;
		}
	}

	AIAction RandomPreference(AIAction[] actions){
		int chosen = Random.Range (0, actions.Length - 1);
		// Debug.Log ("Preferred action: " + chosen);
		return actions [chosen];
	}

	// Below are additional actions

	void RaiseAlarm() {
		// make a lot of noise

	}

	void AnswerAlarm(int wp, bool quietly) {
		// move quickly to wp
		// if not quietly, add to noise

	}

	void AnswerAlarm(GameObject dest, bool quietly, float maxdist) {
		// move quickly toward dest (which may be moving)
		// until there or maxdist
		// if not quietly, add to noise

	}

	*/

}



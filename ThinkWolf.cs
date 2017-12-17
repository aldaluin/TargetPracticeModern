#define LOGEXITS

using UnityEngine;
using System.Collections;
using Goldraven.AI ;

namespace Goldraven.AI {

	public class ThinkWolf :  ThinkBase
	{
		public float distance;
		public float timeToIdle;
		public float initialX;
		public float initialZ;
		public  GameObject target;


		public override void AddStates()
		{
			base.AddStates ();

			AddState<WWander>();
			AddState<WPause>();
			AddState<WGoto> ();
			AddState<WHuntObject> ();  // goblin

			SetInitialState<WGoto>();
			#if(LOGEXITS)
			Debug.LogFormat("**ThinkWolf ({0}) {1} **",this.name, "States added");
			#endif 

		}

		public override  void Start() {

			base.Start ();
			thisAI = aifactory.MakeAI ("wolf", this.gameObject);
			#if(LOGEXITS)
			Debug.LogFormat("**ThinkWolf ({0}) {1} **",this.name, "Started");
			#endif 
		}
	}

	public class WHuntObject : ByTheTale.StateMachine.State
	{

		public ThinkWolf thinkagent { get { return (ThinkWolf )machine; } }
		protected  ActBase thisAct;

		public WHuntObject():base () {
		}

		public override void Enter()
		{
			base.Enter();
			thisAct = thinkagent.aifactory.MakeAct(thinkagent.thisAI, "huntobject", thinkagent.gameObject.transform   );
			thisAct.setParm ("target", thinkagent.target);
			thisAct.startAction ();
			#if(LOGEXITS)
			Debug.LogFormat("**ThinkWolf ({0}) {1} --> {2} **",machine.name, "WHuntObject entered", thinkagent.target.name);
			#endif 

		}

		public override void Execute()
		{
			base.Execute();

			if (thisAct.finished) {
				machine.ChangeState<WPause> ();
			} else {
				thisAct.continueAction ();
			}
		}
	}


	public class WWander : ByTheTale.StateMachine.State
	{

		public ThinkWolf thinkagent { get { return (ThinkWolf )machine; } }
		protected  ActBase thisAct;
		private TerrainData mainTerrain;

		public WWander():base () {
			mainTerrain = Terrain.activeTerrain.terrainData ;
		}

		public override void Enter()
		{
			base.Enter();
			thisAct = thinkagent.aifactory.MakeAct(thinkagent.thisAI, "wander", thinkagent.gameObject.transform   );
			thisAct.setParm ("distance", thinkagent.distance);
			thisAct.startAction ();
			#if(LOGEXITS)
			Debug.LogFormat("**ThinkWolf ({0}) {1} **",machine.name, "WWander entered");
			#endif 

		}

		public override void Execute()
		{
			base.Execute();

			if (thisAct.finished) {
				machine.ChangeState<WPause> ();
			} else {
				thisAct.continueAction ();
			}
		}
	}
	public class WPause : ByTheTale.StateMachine.State
	{

		public ThinkWolf thinkagent { get { return (ThinkWolf )machine; } }
		private ActBase thisAct;

		public override void Enter()
		{
			base.Enter();
			thisAct = thinkagent.aifactory.MakeAct(thinkagent.thisAI, "stand", thinkagent.gameObject.transform   );
			thisAct.setParm ("timer", thinkagent.timeToIdle);
			thisAct.startAction ();
			#if(LOGEXITS)
			Debug.LogFormat("**ThinkWolf ({0}) {1} **",machine.name, "WPause entered");
			#endif 

		}

		public override void Execute()
		{
			base.Execute();

			if (thisAct.finished) {
				machine.ChangeState<WWander> ();
			} else {
				thisAct.continueAction ();
			}
		}


	}
	public class WGoto : ByTheTale.StateMachine.State
	{

		public ThinkWolf thinkagent { get { return (ThinkWolf )machine; } }
		private ActBase thisAct;

		public override void Enter()
		{
			base.Enter ();
			Vector3 targetPoint = new Vector3 (thinkagent.initialX, 0f, thinkagent.initialZ);
			#if (LOGDESTINATIONS)
			Debug.LogFormat("***{0} destination: {1} ***", machine.name , targetPoint );
			#endif 
			thisAct = thinkagent.aifactory.MakeAct(thinkagent.thisAI, "move", thinkagent.gameObject.transform );
			thisAct.setParm ("target", targetPoint);
			thisAct.startAction ();
			#if(LOGEXITS)
			Debug.LogFormat("**ThinkWolf ({0}) {1} **",machine.name, "WGoto entered");
			#endif 

		}

		public override void Execute()
		{
			base.Execute();

			if (thisAct.finished) {
				machine.ChangeState<WPause> ();
			} else {
				thisAct.continueAction ();
			}

		}
	}


}

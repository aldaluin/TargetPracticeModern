#define LOGEXITS

using UnityEngine;
using System.Collections;
using Goldraven.AI ;

namespace Goldraven.AI {

	public class ThinkGoblin :  ThinkBase 
	{
		public float distance;
		public float timeToIdle;
		public float initialX;
		public float initialZ;

		public override void AddStates()
		{
			base.AddStates ();

			AddState<GPause> ();
			AddState<GGoto> ();
			AddState<GWander>();

			SetInitialState<GGoto>();
			#if(LOGEXITS)
			Debug.LogFormat("**ThinkGoblin ({0}) {1} **",this.name, "States added");
			#endif 

		}

		public override  void Start() {

			base.Start ();
			thisAI = aifactory.MakeAI ("goblin", this.gameObject);
			#if(LOGEXITS)
			Debug.LogFormat("**ThinkGoblin ({0}) {1} **",this.name, "Started");
			#endif 
		}
	}

	public class GWander : ByTheTale.StateMachine.State
	{

		public ThinkGoblin thinkagent { get { return (ThinkGoblin )machine; } }
		protected  ActBase thisAct;
		private TerrainData mainTerrain;

		public GWander():base () {
			mainTerrain = Terrain.activeTerrain.terrainData ;
		}

		public override void Enter()
		{
			base.Enter();
			thisAct = thinkagent.aifactory.MakeAct(thinkagent.thisAI, "wanderonce", thinkagent.gameObject.transform   );
			thisAct.setParm ("distance", thinkagent.distance);
			thisAct.startAction ();
			#if(LOGEXITS)
			Debug.LogFormat("**ThinkGoblin ({0}) {1} **",machine.name, "GWander entered");
			#endif 

		}

		public override void Execute()
		{
			base.Execute();

			if (thisAct.finished) {
				machine.ChangeState<GPause> ();
			} else {
				thisAct.continueAction ();
			}
		}
	}
	public class GPause : ByTheTale.StateMachine.State
	{

		public ThinkGoblin thinkagent { get { return (ThinkGoblin )machine; } }
		private ActBase thisAct;

		public override void Enter()
		{
			base.Enter();
			thisAct = thinkagent.aifactory.MakeAct(thinkagent.thisAI, "stand", thinkagent.gameObject.transform   );
			thisAct.setParm ("timer", thinkagent.timeToIdle);
			thisAct.startAction ();
			#if(LOGEXITS)
			Debug.LogFormat("**ThinkGoblin ({0}) {1} **",machine.name, "GPause entered");
			#endif 

		}

		public override void Execute()
		{
			base.Execute();

			if (thisAct.finished) {
				machine.ChangeState<GWander> ();
			} else {
				thisAct.continueAction ();
			}
		}


	}
	public class GGoto : ByTheTale.StateMachine.State
	{

		public ThinkGoblin thinkagent { get { return (ThinkGoblin )machine; } }
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
			Debug.LogFormat("**ThinkGoblin ({0}) {1} **",machine.name, "GGoto entered");
			#endif 

		}

		public override void Execute()
		{
			base.Execute();

			if (thisAct.finished) {
				machine.ChangeState<GPause> ();
			} else {
				thisAct.continueAction ();
			}

		}
	}


}

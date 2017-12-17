/*
 * 
 * superseded  2017/08/22
 * 
 * capability moved to actbase, actwander, thinkgoblin, thinkwolf
 * 

#define LOGEXITS
#define LOGDESTINATIONS

using UnityEngine;
using System.Collections;
using Goldraven.AI ;

namespace Goldraven.AI {


	public class TBWander : ByTheTale.StateMachine.State
	{

		public ThinkBase thinkagent { get { return (ThinkBase )machine; } }
		protected  ActBase thisAct;
		private TerrainData mainTerrain;

		public TBWander():base () {
			mainTerrain = Terrain.activeTerrain.terrainData ;
		}

		public override void Enter()
		{
			base.Enter();
			Vector3 targetPoint = thinkagent.location.position + UnityEngine.Random.onUnitSphere * thinkagent.distance;
			//TODO: set targetPoint.y to the surface of the terrain.
			targetPoint.y = mainTerrain.GetHeight((int)targetPoint.x, (int)targetPoint.z);
			#if (LOGDESTINATIONS)
			Debug.LogFormat("***{0} destination: {1} ***", machine.name , targetPoint );
			#endif 
			thisAct = thinkagent.aifactory.MakeAct(thinkagent.thisAI, "move", thinkagent.gameObject.transform   );
			thisAct.setParm ("target", targetPoint);
			thisAct.startAction ();
			#if(LOGEXITS)
			Debug.LogFormat("**ThinkBase {0} **", "TBWander entered");
			#endif 

		}

		public override void Execute()
		{
			base.Execute();

			if (thisAct.finished) {
				machine.ChangeState<TBPause> ();
			} else {
				thisAct.continueAction ();
			}
		}
	}
	public class TBPause : ByTheTale.StateMachine.State
	{

		public ThinkBase thinkagent { get { return (ThinkBase )machine; } }
		public float timeInState { get; protected set; }
		private ActBase thisAct;

		public override void Enter()
		{
			base.Enter();
			timeInState = 0;
			thisAct = thinkagent.aifactory.MakeAct(thinkagent.thisAI, "stand", thinkagent.gameObject.transform   );
			thisAct.startAction ();
			#if(LOGEXITS)
			Debug.LogFormat("**ThinkBase {0} **", "TBPause entered");
			#endif 

		}

		public override void Execute()
		{
			base.Execute();

			if (thinkagent.timeToIdle <= timeInState) {
				machine.ChangeState<TBWander> ();
			} else {
				timeInState += Time.deltaTime;
				thisAct.continueAction ();
			}
		}


	}
	public class TBGoto : ByTheTale.StateMachine.State
	{

		public ThinkBase thinkagent { get { return (ThinkBase )machine; } }
		private ActBase thisAct;

		public override void Enter()
		{
			base.Enter ();
			Vector3 targetPoint = new Vector3 (thinkagent.initialX, 0f, thinkagent.initialZ);
			//TODO: set targetPoint.y to the surface of the terrain.
			#if (LOGDESTINATIONS)
			Debug.LogFormat("***{0} destination: {1} ***", machine.name , targetPoint );
			#endif 
			thisAct = thinkagent.aifactory.MakeAct(thinkagent.thisAI, "move", thinkagent.gameObject.transform );
			thisAct.setParm ("target", targetPoint);
			thisAct.startAction ();
			#if(LOGEXITS)
			Debug.LogFormat("**ThinkBase {0} **", "TBGoto entered");
			#endif 

		}

		public override void Execute()
		{
			base.Execute();

			if (thisAct.finished) {
				machine.ChangeState<TBPause> ();
			} else {
				thisAct.continueAction ();
			}

		}
	}


}
*/



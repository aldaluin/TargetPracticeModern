#define LOGEXITS
#define LOGDESTINATIONS

using UnityEngine;
using System.Collections;
using Goldraven.AI ;

namespace Goldraven.AI {

	public class ThinkBase :  ByTheTale.StateMachine.MachineBehaviour
	{
		// needed by base
		public  Transform location { get ; private set;}
		public  AIFactory aifactory { get ; private set;}
		public  AIBase thisAI { get; protected set; }

		public override  void Start() {

			base.Start ();
			location = GetComponent<Transform> ();
			aifactory = new AIRealFactory();
			// defer thisAI to subclass
			#if(LOGEXITS)
			Debug.LogFormat("**ThinkBase {0} **", "Started");
			#endif 
		}

		public override void AddStates() {
			
		}

	}


}

using System ;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Goldraven.AI ;

namespace Goldraven.AI {

	/*
	 * First pass at AI factory makes two ai classes and two activity classes
	 * cmcb 2017/08/07
	 */


	public abstract class AIFactory
	{
		public abstract AIBase  MakeAI (string aiKey, GameObject AIChar);
		public abstract ActBase MakeAct (string aiKey, string actKey, GameObject AIChar);
		public abstract ActBase MakeAct (AIBase ai, string actKey, Transform loc);

	}

	public class AIRealFactory : AIFactory 
	{

		public override AIBase MakeAI(string aiKey, GameObject AIChar) {
			if (aiKey == "goblin") {
				return new AICharacterController (AIChar);
			} else if (aiKey == "wolf") {
				return new AIManedWolf (AIChar);
			} else {
				throw new ArgumentException ();
			}
		}
			
		public override ActBase MakeAct (string aiKey, string actKey, GameObject AIChar) {
			AIBase vAI = MakeAI (aiKey, AIChar );
			return MakeAct (vAI, actKey, AIChar.transform  );
		}

		public override ActBase MakeAct (AIBase ai, string actKey, Transform loc) {
			if (actKey == "stand") {
				return new ActStand (ai, loc );
			} else if (actKey == "move") {
				return new ActMove (ai, loc );
			} else if (actKey == "wander") {
				return new ActWander (ai, loc, this );
			} else if (actKey == "wanderonce") {
				return new ActWanderOnce (ai, loc, this );
			} else if (actKey == "huntobject") {
				return new ActHuntObject (ai, loc, this );

			} else {
				throw new ArgumentException ();
			}

		}

	}

}


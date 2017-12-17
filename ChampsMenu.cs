using UnityEngine;
using System.Collections;
using UnityStandardAssets.Utility;
using UnityEngine.EventSystems;
using Goldraven.Prod;

namespace Goldraven.Prod
{

	/*
	 * cmcb 2017/10/31
	 * 
	 * 
 	 * 
 	 * TODO: Don't assume the cursor locked and invisible in regular play
 	 * 
 	 */

	public class ChampsMenu : MonoBehaviour
	{

		public Canvas champsMenu, startMenu;

		public PickPlayer playerChoices = null;
		public PickCamera cameraChoices = null;

		void Start ()
		{
		}

		void Update ()
		{
		}

		public void DoReturn ()
		{
			startMenu.gameObject.SetActive (true);
			champsMenu.gameObject.SetActive (false);
		}

		public void DoPickChamp (int choice)
		{
			playerChoices.PlayerIndex = choice;
			cameraChoices.CameraIndex = choice;
			playerChoices.Init ();
			cameraChoices.Init ();
			DoReturn ();
		}

		public void DoSettings ()
		{
			Debug.Log ("Settings button pushed");
		}

	}
}

using UnityEngine;
using System.Collections;
using System;

namespace Goldraven.Prod
{


	public class PickPlayer : MonoBehaviour
	{
		/*
		 * cmcb  2017/10/16
		 * 
		 * Allow choice of player from an array of options
		 * 
		 */

		[SerializeField] private GameObject[] Players;
		public int PlayerIndex = 0;

		public GameObject  ActiveObject { get { return Players  [PlayerIndex]; } }

		// Use this for initialization
		void Awake ()
		{
			Init ();
		}
	

		public void Init() {
			for (int i = 0; i < Players.Length; i++) {
				Players [i].SetActive (i == PlayerIndex);
			}
		}
		// Update is called once per frame
		void Update ()
		{
	
		}
	}

}
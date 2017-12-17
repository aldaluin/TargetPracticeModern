using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace CharlieAssets.Dev
{

	public class FriendMove : MonoBehaviour 
/*
*  Click on a spot to send the friend to it
*/


	{
		
		public Transform destination;
		public Camera mycamera;
		public int MouseButton = 0;

		private NavMeshAgent agent;

		void Start () 
		{
			agent = gameObject.GetComponent<NavMeshAgent>();
			agent.SetDestination(destination.position);
		}

		void Update()
		{
			if (Input.GetMouseButtonDown(MouseButton))
			{
				Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);

				RaycastHit hit;
				if (Physics.Raycast(screenRay, out hit))
				{
					agent.SetDestination(hit.point);
				}
			}
		}
		void OnGUI()
		{
			int size = 12;
			float posX = mycamera.pixelWidth / 2 - size / 4;
			float posY = mycamera.pixelHeight / 2 - size / 4;
			GUI.Label(new Rect(posX, posY, size, size), "*");

		}

	}
		
}


// #define USE_INVECTOR

using UnityEngine;
using System.Collections;
#if USE_INVECTOR
using Invector;
#endif 

   // TODO: Fix this hack
using UnityEditor;

namespace Goldraven.Prod
{


	public class PickCamera : MonoBehaviour
	{
		/*
		 * cmcb  2017/10/29
		 * 
		 * Allow choice of camera from an array of options
		 * 
		 */

		[SerializeField] private GameObject[] Cameras;
		public int CameraIndex = 0;

		public GameObject ActiveObject { get { return Cameras [CameraIndex]; } }

		public Camera  ActiveCamera { get { return Cameras [CameraIndex].GetComponent<Camera> (); } }

		#if USE_INVECTOR

		public vThirdPersonCamera ActiveInvectorCamera {
			get { 
				vThirdPersonCamera cam = Cameras [CameraIndex].GetComponent<vThirdPersonCamera> ();
				// Debug.Log("Active Invector Camera called: " + cam.ToString());
				return cam;
			}
		}

		#endif 

		void Awake ()
		{
			Init ();
		}


		// Use this for initialization
		public void Init ()
		{
			for (int i = 0; i < Cameras.Length; i++) {
				Cameras [i].SetActive (i == CameraIndex);
			}
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
	}
}

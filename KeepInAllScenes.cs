// #define USE_INVECTOR

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using UnityStandardAssets.Cameras;
using Goldraven.Prod;
#if USE_INVECTOR

using Invector;
#endif 

namespace Goldraven.Prod
{

	/*
	 * cmcb 2017/10/29
	 * 
	 * Just calls DontDestroyOnLoad so the GameObject and
	 * all children will be maintained until the end
	 * of the game.
	 * 
	 * TODO: Fix the invector camera hack
	 */


	public class KeepInAllScenes : MonoBehaviour
	{

		public bool UsePlayerStart = false;

		void Awake ()
		{
			DontDestroyOnLoad (gameObject);
			SceneManager.sceneLoaded += PlaceObject;
		}
	
		void PlaceObject(Scene ns, LoadSceneMode nsmode) {
			if (ns.IsValid ()) {
				GameObject startpoint = GameObject.FindGameObjectWithTag ("PlayerStart");
				gameObject.transform.position = startpoint.transform.position;
				gameObject.transform.rotation = startpoint.transform.rotation;
				#if USE_INVECTOR

				PickCamera pc = GetComponent<PickCamera> ();
				if (pc != null) {
					vThirdPersonCamera lc = pc.ActiveInvectorCamera;
					if (lc != null) {
						lc.Init ();
					}
				}
				#endif 
			} else {
				Debug.Log ("Invalid scene loaded?");
			}
		}

	}
}


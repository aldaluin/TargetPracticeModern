// #define CURSOR_LOCK

using UnityEngine;
using System.Collections;

namespace Goldraven.Dev
{

	public class PauseGame : MonoBehaviour
	{
		/*
		 * cmcb  2017/11/13
		 * 
		 * First hack trying a delegate
		 * 
		 */

		private bool inaPause = false;
		public bool Paused { get { return inaPause; } private set{ inaPause = value; } }


		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
				
		}

		public delegate void PauseFunc();     // void onPause()
		public delegate void ResumeFunc ();   // void onResume()

		public event PauseFunc Pausers;
		public event ResumeFunc Resumers;

		public void doPause(){
			Paused = true;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true; 
			Time.timeScale = 0;// time stopped
			if (Pausers != null) {
				Pausers ();
			}
		}

		public void doResume() {
			Paused = false;
			#if CURSOR_LOCK
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			#endif
			Time.timeScale = 1;// time is normal
			if (Resumers != null) {
				Resumers ();
			}
		}

		public void doQuit() {
			Application.Quit ();
			#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
			#endif
		}


	}
}

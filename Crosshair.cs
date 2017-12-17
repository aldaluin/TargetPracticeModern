// script to draw sight
using UnityEngine;
using System.Collections;
using Goldraven.Prod;

namespace Goldraven.Weapons {

	public class Crosshair : MonoBehaviour {

		public Goldraven.Dev.PauseGame pg;
		public Texture2D crosshairTexture;
		private Rect _position;
		public Rect position { 
			get { 
				_position.Set (aimpoint.x - crosshairTexture.width / 2, aimpoint.y - crosshairTexture.height / 2, crosshairTexture.width, crosshairTexture.height);
				return _position;
			} }
		
		[HideInInspector] public Vector3 aimpoint;

		void  Start ()
		{
			aimpoint = new Vector3 (Screen.width / 2, Screen.height / 2, 0);
		}

		void  OnGUI ()
		{
			if (pg != null)
			if (pg.Paused)
				return;
			GUI.DrawTexture (position, crosshairTexture);	
		}

	}
}


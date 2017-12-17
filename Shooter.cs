using UnityEngine;
using System.Collections;
using System.Configuration;

namespace Goldraven.Weapons
{

	/*
	 * cmcb  2017/10/29
	 * 
	 * Shooter is base for at least rayshooter and instant shooter.
	 * It holds common code to react to pause button
	 * and what button to use to fire
	 * 
	 * Of course it requires the buttons to be defined in project settings/input
	 * 
	 *
	 */



	public class Shooter : MonoBehaviour
	{

		public Goldraven.Dev.PauseGame pg;
		private Crosshair crosshair;
		public string fireButton = "Fire1";
		// public int FontSize = 40;

		protected  Camera _camera;
		protected  LayerMask _layermask;


		protected virtual void Start ()
		{
			if (pg == null) Debug.LogError("Pause menu has no link to game pause");
			_camera = GetComponent<Camera> ();
			if (_camera == null) {
				_camera = Camera.main;
			}
			crosshair = gameObject.GetComponent<Crosshair> ();
			if (crosshair != null)
				crosshair.enabled = false;
			_layermask = (LayerMask.GetMask ("Player"));
		}

		protected virtual void Update ()
		{
		}

		protected virtual void Aim() {
			// triggered by fire button *down*
			if (pg.Paused) return;
			if (crosshair != null)
				crosshair.enabled = true;
			_camera.cullingMask -= _layermask;

		}

		protected virtual void Fire() {
			// triggered by fire button *up*
			if (pg.Paused) return;

		}

		protected virtual void Recover() {
			if (pg.Paused) return;
			if (crosshair != null)
				crosshair.enabled = false;
			
			_camera.cullingMask += _layermask;
		}

	}

}

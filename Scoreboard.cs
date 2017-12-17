using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Goldraven.Weapons;
using Goldraven.IvWeapons;

namespace Goldraven.Prod
{

	/* cmcb  2017/11/12
	 * 
	 * Display time, shots fired, targets hit, and a running score
	 * 
	 */

	public class Scoreboard : MonoBehaviour
	{

		public Text TimeText = null;
		public Text HitsText = null;
		public Text AmmoText = null;
		public Text KillText = null;
		public Text ScoreText = null;

		public Iv_ShooterManager shooterManager;
		public string TimeLabel = "Time: ";
		public string HitsLabel = "Hits: ";
		public string AmmoLabel = "Ammo: ";
		public string KillLabel = "Kills: ";
		public string ScoreLabel = "Score: ";
		public string Score { get { return ScoreCount.ToString ("D5"); } }

		public int HitValue = 2;
		public int KillValue = 5;
		public int ShotPenalty = 1;

		private bool isVictory = false;
		private bool isDefeat = false;

		public  bool Victory { get { return isVictory;} set{ isVictory = value;
				isDefeat = !value; } }
		public  bool Defeat { get { return isDefeat;} set {isDefeat = value;
				isVictory = !value;} }


		public int HitsCount { get; private set; }
		public int AmmoCount  { get; private set; }
		// 0 = unlimited and count is up
		// >0 = limited and counts down
		private bool AmmoUp = true;
		public int KillsCount { get; private set; }
		public int ScoreCount { get; private set; }


		// Use this for initialization
		void Start ()
		{
			if (AmmoCount == 0) {
				AmmoUp = true;
			} else {
				AmmoUp = false;
			}
			HitsCount = 0;
			AmmoCount = 0;
			KillsCount = 0;
			ScoreCount = 0;
			shooterManager.onShoot.AddListener(AddShot);
		}
	
		// Update is called once per frame
		void Update ()
		{
			float timer = Time.timeSinceLevelLoad;
			int minutes = Mathf.FloorToInt (timer / 60F);
			int seconds = Mathf.FloorToInt (timer - minutes * 60);
			string niceTime = string.Format ("{0:0}:{1:00}", minutes, seconds);
			TimeText.text = TimeLabel + niceTime;
			HitsText.text = HitsLabel + HitsCount.ToString ();
			AmmoText.text = AmmoLabel + AmmoCount.ToString ();
			ScoreText.text = ScoreLabel + ScoreCount.ToString ("D5");

		}

		public void AddHit ()
		{
			HitsCount++;
			ScoreCount += HitValue;
		}

		public void AddShot ()
		{
			if (AmmoUp) {
				AmmoCount++;
			} else {
				AmmoCount--;
			}
			ScoreCount -= ShotPenalty;
		}

		public void AddKill() {
			KillsCount++;
			ScoreCount += KillValue;
		}

		public void AddShot (vShooterWeapon weapon) {
			// Debug.Log ("Iv shot seen by scoreboard");
			AddShot ();
		}
	}
}

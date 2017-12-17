using UnityEngine;
using System.Collections;

namespace Goldraven.IvWeapons
{

	public class Iv_ShooterManager : vShooterManager{

		public class OnShoot : UnityEngine.Events.UnityEvent<vShooterWeapon> { }

		[HideInInspector]
		public OnShoot onShoot;

		void Awake() {
			if (onShoot == null) {
				onShoot = new OnShoot ();
			}
		}

		public override void Shoot(Vector3 aimPosition, bool applyHipfirePrecision = false)
	{
			if (isShooting) return;
			if (rWeapon.ammoCount > 0)
			{
				onShoot.Invoke(rWeapon);
			}
			base.Shoot (aimPosition, applyHipfirePrecision);
	}

}

}

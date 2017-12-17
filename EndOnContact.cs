using UnityEngine;
using System.Collections;
using Goldraven.Weapons;

namespace Goldraven.Dev  {

public class EndOnContact : MonoBehaviour

// Add this script to a triggered collider to
// immediately end the game.

{

	void OnTriggerEnter(Collider victim)
	{
		Target  term = victim.GetComponent<Target> ();
		if (term != null && term.terminatorTarget)
        {
            Application.Quit();
			#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
			#endif
        }
	}

}
}

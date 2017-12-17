using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
// using SWS;

namespace CharlieAssets.Prod
{

	public class LoadScene : MonoBehaviour
	{

		public bool NextScene = true;  // overrides the scene name
		public string SceneToLoad = "";
		public bool LoadAsynch = false;
		public Texture2D Splash = null;

		private Rect position;

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void OnTriggerEnter (Collider victim)
		{
			LoadAScene ();

		}

		void LoadAScene () {
			if (Splash != null) {
				position = new Rect ((Screen.width - Splash.width) / 2, (Screen.height - Splash.height) / 2, Splash.width, Splash.height);
				GUI.DrawTexture(position, Splash);	
			}
			if (NextScene) {
				if (LoadAsynch) {
					SceneManager.LoadSceneAsync (SceneManager.GetActiveScene ().buildIndex + 1);
				} else {
					SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
				}
			} else {
				if (LoadAsynch) {
					SceneManager.LoadSceneAsync (SceneToLoad);
				} else {
					SceneManager.LoadScene (SceneToLoad);
				}
			}
		}
	}
}

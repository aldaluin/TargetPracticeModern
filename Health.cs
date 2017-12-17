using UnityEngine;
using System.Collections;

namespace CharlieAssets.Dev
{

public class Health : MonoBehaviour

//
//  Edited cmcb 2017/05/27
//

{
	private float player_health_min = 0;   // min health
    public float player_health = 100;      // current health
    private float timeout = 0;             // timer
    public GUIText HP;                     // text which shows the current health
	public float fire_damage = 10;           // fire damage
	public int explosition_damage = 40;     // explosion damage
	public int bullet_damage=2; // bullet damage
	public int melee_damage=5;// melee atack damage
	public GameObject camera;



    void Update()
    {
        if (player_health <= player_health_min)// if curent health <= min health
        {
            if (!gameObject.GetComponent<Rigidbody>())// if fpc hasn't rigidbody
            {
				gameObject.GetComponent<Health_BlackTexture>().Trigger();// fade to black
				camera.GetComponent<Animation>().Play("Die");// the animation play "Die"
                timeout += Time.deltaTime;// timer active
                if (timeout >= 1)// after 1 second
                {
					Application.Quit();
					#if UNITY_EDITOR
					UnityEditor.EditorApplication.isPlaying = false;
					#endif
				}
            }
            player_health = player_health_min;// curent health = min health

        }
    }
    public void Del()// explosion damage
    {
        player_health -= explosition_damage;// curent health - explosion damage
    }

   void OnTriggerStay(Collider Col)// fire damage
    {
        if (Col.tag == "Fire")// if collider tag = "fire'
        {
            player_health -= fire_damage;// curent health - fire damage
        }
    }



    void OnGUI()// draw curent health
    {
        HP.text = "" + Mathf.Round(player_health);
    }
}
}

using UnityEngine;
using System.Collections;

namespace Goldraven.Weapons
{

public class Fireball : MonoBehaviour {

    public float speed = 10.0f;
    public int damage = 2;
	public int life = 200;

    // Use this for initialization
    void Start () {
		// StartCoroutine (TimeOut (life));
	}
	
	// Update is called once per frame
	void Update () 
	{
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        Target player = other.GetComponent<Target>();
        if (player != null)
        {
            player.ReactToHit(damage);
        }
        Destroy(this.gameObject);
    }
		
	IEnumerator TimeOut(int timetowait) {
		yield return new WaitForSecondsRealtime (timetowait);
		Destroy (this.gameObject);
	}
}}

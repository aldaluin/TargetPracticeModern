using UnityEngine;
using System.Collections;

namespace CharlieAssets.Dev {


public class Teleport : MonoBehaviour {

    public Vector3 destination = new Vector3(-40,0,40);
	public GameObject anchor;

	private Transform _location;

	// Use this for initialization
	void Start () {
		if (!anchor) {
			anchor = this.gameObject;
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider victim)
    {
		victim.transform.position = anchor.transform.position + destination ;
    }
}}

using UnityEngine;
using System.Collections;

namespace CharlieAssets.Dev
{

public class FireballShooter : MonoBehaviour {

    [SerializeField] private GameObject fireballPrefab;
    private GameObject _fireball = null;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(1)) // TODO: Generalize this input
        {
                    _fireball = Instantiate(fireballPrefab) as GameObject;
                    _fireball.transform.position = transform.TransformPoint(Vector3.forward * 3.5f) + Vector3.up;
                    _fireball.transform.rotation = transform.rotation;
        }
    }
}
}
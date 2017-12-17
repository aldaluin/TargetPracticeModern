using UnityEngine;
using System.Collections;
using Goldraven.Weapons;

namespace Goldraven.Dev {


public class WanderingChar : MonoBehaviour
{

    public float speed = 3.0f;
    public float obstacleRange = 5.0f;

    [SerializeField]    private GameObject fireballPrefab;
    private GameObject _fireball = null;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);

		// cast ray forward
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

		// what will fireball hit down ray?
        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
			// hit something
            GameObject hitObject = hit.transform.gameObject;
			// if target, launch fireball
            if (hitObject.GetComponent<Target>())
            {
                if (_fireball == null)
                {
                    _fireball = Instantiate(fireballPrefab) as GameObject;
                    _fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                    _fireball.transform.rotation = transform.rotation;
                }
            }
			// if not target, am i about to run in to something?
            else if (hit.distance < obstacleRange)
            {
				// change direction
                float angle = Random.Range(90, 270);
                transform.Rotate(0, angle, 0);
            }
        }
}
}}

using UnityEngine;
using System.Collections;

namespace CharlieAssets.Dev
{

public class WanderingShooter : MonoBehaviour
{

    public float speed = 3.0f;
    public float obstacleRange = 5.0f;

    [SerializeField]    private GameObject fireballPrefab;
    private GameObject _fireball = null;
	private GameObject[] enemies;

    // Use this for initialization
    void Start()
    {
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
		// do move
        transform.Translate(0, 0, speed * Time.deltaTime);

		// look ahead and turn if something is there
		if (Physics.Raycast(transform.position, transform.forward, obstacleRange))
        {
                float angle = Random.Range(90, 270);
                transform.Rotate(0, angle, 0);
        }

		// check for something to shoot at
		if (enemies == null) {
			enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		}
		foreach (GameObject enemy in enemies) {
			if (Physics.Linecast(transform.position, enemy.transform.position) && (_fireball == null)) {
				// face target
				float deltaX = enemy.transform.position.x - transform.position.x;
				float deltaZ = enemy.transform.position.z - transform.position.z;
				Vector3 relativePos = enemy.transform.position - transform.position;
				Quaternion rotate = Quaternion.LookRotation(relativePos);
				/// transform.rotation = rotate; <-- Need to rotate around Y only.
				// shoot fireball
				_fireball = Instantiate(fireballPrefab) as GameObject;
				_fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
				_fireball.transform.rotation = rotate;

			}
		}


	}
}}

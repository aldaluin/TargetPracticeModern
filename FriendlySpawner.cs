using UnityEngine;
using System.Collections;

namespace CharlieAssets.Dev
{

public class FriendlySpawner : MonoBehaviour {
    public Vector3 SpawnPoint = new Vector3(30, 5, 30);
    [SerializeField]  private GameObject friendlyPrefab;
    private GameObject _friendly;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_friendly == null)
        {
            _friendly = Instantiate(friendlyPrefab) as GameObject;
            _friendly.transform.position = SpawnPoint;
            float angle = Random.Range(0, 360);
            _friendly.transform.Rotate(0, angle, 0);
        }

    }
}}

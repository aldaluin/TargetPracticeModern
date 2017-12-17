using UnityEngine;

namespace CharlieAssets.Prod {

public class EnemySpawner : MonoBehaviour {

    public Vector3 SpawnPoint = new Vector3(0, 5, 0);

	[SerializeField] private Terrain spawnTerrain;
	[SerializeField] private GameObject enemyPrefab;
	private GameObject _enemy;


    // Use this for initialization
    void Start () {
		
	
	}
	
	// Update is called once per frame
	void Update () {
        if (_enemy == null)
        {
            _enemy = Instantiate(enemyPrefab) as GameObject;
			if (spawnTerrain != null) {
				SpawnPoint.x = Random.Range(0, (int)spawnTerrain.terrainData.size.x);
				SpawnPoint.z = Random.Range(0, (int)spawnTerrain.terrainData.size.z);
				SpawnPoint.y = (float) System.Math.Round (spawnTerrain.terrainData.GetHeight ((int)SpawnPoint.x, (int)SpawnPoint.z) + 10);
				// Debug.Log ("Spawn point: " + SpawnPoint.x + " " + SpawnPoint.y + " " + SpawnPoint.z);
			}
            _enemy.transform.position = SpawnPoint;
			float angle = Random.Range (0, 360);
            _enemy.transform.Rotate(0, angle, 0);
        }

    }
}}

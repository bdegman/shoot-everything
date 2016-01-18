using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	public GameObject enemy;
	public float spawnMinX;
	public float spawnMaxX;
	public float spawnY;

	public int hazardCount;
	public int enemyCount;

	public float spawnWait;
	public float startWait;
	public float waveWait;

	private int waveCount = 1;

	void Start () {
		StartCoroutine (SpawnWaves ());
	}

	IEnumerator SpawnWaves () {
		yield return new WaitForSeconds (startWait);
		while (true) {
			for (int i = 0; i < hazardCount; i++) {
				if (i < enemyCount) {
					Vector3 enemySpawnPosition = new Vector3 (Random.Range (spawnMinX, spawnMaxX), 4, 0);
					Quaternion enemySpawnRotation = Quaternion.identity;
					Instantiate (enemy, enemySpawnPosition, enemySpawnRotation);
				}
				float rnd = Random.Range (-1, 1);
				float direction = Mathf.Sign (rnd);
				Vector3 spawnPosition = new Vector3 (Random.Range (spawnMinX, spawnMaxX) * direction, spawnY, 0);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			enemyCount = enemyCount + waveCount;
			hazardCount = hazardCount * waveCount;

			waveCount++;
			yield return new WaitForSeconds (waveWait);
		}

	}


}

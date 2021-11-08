using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawer : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpwanAllWaves());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator SpwanAllWaves() {
        for (int waveIndex = startingWave; waveIndex <= waveConfigs.Count - 1; waveIndex++) {
            WaveConfig currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEmemyInWave(currentWave));
            if(waveIndex == waveConfigs.Count - 1) {
                waveIndex = startingWave - 1;
            }
        }
    }

    private IEnumerator SpawnAllEmemyInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumebrOfEnemies(); enemyCount++)
        {
            GameObject newEmeny = Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetWayPoints()[0].transform.position,
                new Quaternion(
                    waveConfig.GetWayPoints()[0].transform.rotation.x,
                    waveConfig.GetWayPoints()[0].transform.rotation.y,
                    waveConfig.GetWayPoints()[0].transform.rotation.z + 180, 1)
                
            );

            newEmeny.GetComponent<EnemyPath>().SetWaveConfig(waveConfig);

            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }
}
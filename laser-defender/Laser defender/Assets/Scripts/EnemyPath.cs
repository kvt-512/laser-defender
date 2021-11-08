using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    WaveConfig waveConfig;
    List<Transform> wavepoint;
    int currentWavePointIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        wavepoint = waveConfig.GetWayPoints();
        transform.position = wavepoint[currentWavePointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        TravelThroughEnemyPath();
    }

    public void SetWaveConfig(WaveConfig waveConfig) {
        this.waveConfig = waveConfig;
    }

    private void TravelThroughEnemyPath() {
        if(wavepoint.Count - 1 >= currentWavePointIndex) {
            var targetPostion = wavepoint[currentWavePointIndex].transform.position;
            var movemenThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPostion, movemenThisFrame);

            if(transform.position == targetPostion) {
                currentWavePointIndex++;
            }
        }
        else if(transform.position == wavepoint[wavepoint.Count - 1].transform.position) {
            Destroy(this.gameObject);
        }
    }
}

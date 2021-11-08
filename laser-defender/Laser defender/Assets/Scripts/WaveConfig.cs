using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Wave Config", menuName = "Laser defender/WaveConfig")]
public class WaveConfig : ScriptableObject {
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns;
    [SerializeField] float spwanRandomFactor;
    [SerializeField] int numberOfEnemies;
    [SerializeField] float moveSpeed;

    public GameObject GetEnemyPrefab() { return enemyPrefab;}
    public List<Transform> GetWayPoints() {
        var waveWayPoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform) {
            waveWayPoints.Add(child);
        }
        return waveWayPoints;
    }
    public float GetTimeBetweenSpawns() { return timeBetweenSpawns;}
    public float GetSpwanRandomFactor() { return spwanRandomFactor;}
    public int GetNumebrOfEnemies() { return numberOfEnemies;}
    public float GetMoveSpeed() { return moveSpeed;}
}
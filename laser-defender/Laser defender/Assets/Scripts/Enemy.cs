    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health;
    float shoutCounter = 0;
    float minTimeBtwShots = 0.2F;
    float maxTimeBtwShots = 0.7F;
    [SerializeField] GameObject enemyLaser;
    [SerializeField] float projectileSpeed;
    [SerializeField] GameObject explosionVFX;
    [SerializeField] AudioClip shootingProjectileSFX;
    [SerializeField] AudioClip enemyExplodeSFX; 
    float explosionLifeTime = 2F;
    GameSession gameSession;
    int scoreValue = 50;
    // Start is called before the first frame update
    void Start()
    {
        shoutCounter = Random.Range(minTimeBtwShots, maxTimeBtwShots);
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        CountDownShots();
    }

    private void CountDownShots() {
        shoutCounter -= Time.deltaTime;
        if(shoutCounter <= 0F) {
            EnemyFire();
            shoutCounter = Random.Range(minTimeBtwShots, maxTimeBtwShots);
        }
    }

    private void EnemyFire() {
        GameObject enemyLaserAsGameObject = Instantiate(
            enemyLaser,
            new Vector3(
                transform.position.x,
                transform.position.y,
                transform.position.z + 2
                ),
            new Quaternion(
                enemyLaser.transform.rotation.x,
                enemyLaser.transform.rotation.y,
                enemyLaser.transform.rotation.z + 180, 1
                )) as GameObject;
        TriggerProjectileSFX();
        enemyLaserAsGameObject.GetComponent<Rigidbody2D>().velocity = Vector2.down * projectileSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer) {
        health = health - damageDealer.GetDamage();
        damageDealer.Hit(gameObject);
        if(health <= 0) {
            Die();
        }
    }

    private void Die() {
        gameSession.AddScore(scoreValue);
        Destroy(this.gameObject);
        TriggerEnemyExplodeSFX();
        GameObject explosionEffect = Instantiate(explosionVFX, transform.position, Quaternion.identity);
        Destroy(explosionEffect, explosionLifeTime);
    }

    private void TriggerProjectileSFX() {
        AudioSource.PlayClipAtPoint(shootingProjectileSFX, this.transform.position);
    }

    private void TriggerEnemyExplodeSFX() {
        AudioSource.PlayClipAtPoint(enemyExplodeSFX, this.transform.position);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //configaration parameters
    [Header("Player")]
    [SerializeField] int playerHealth = 100;
    [SerializeField] public float playerSpeed;
    [SerializeField] float topPadding, padding, paddingDown;

    [Header("Projectile")]
    [SerializeField] GameObject laser;
    float minX ,maxX, minY, maxY;
    float shootingSpeed = 30F;
    float shootingInterval = 0.1F;
    [SerializeField] AudioClip shootingProjectileSFX;
    [SerializeField] AudioClip playerExplodeSFX;
    Coroutine shootingCoroutine;
    [SerializeField] Level level;
    [SerializeField] GameObject explosionVFX;

    // Start is called before the first frame update
    void Start()
    {
        SetUpBoundires();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    IEnumerator FireContinously() {
        while(true) {
            GameObject laserAsGameObject = Instantiate(laser, this.transform.position, Quaternion.identity) as GameObject;
            laserAsGameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0F, shootingSpeed);
            TriggerProjectileShoud();
            yield return new WaitForSeconds(shootingInterval);
        }
    }

    private void Fire() {
        if(Input.GetButtonDown("Fire1")) {
            shootingCoroutine = StartCoroutine(FireContinously());
        }
        if(Input.GetButtonUp("Fire1")) {
            StopCoroutine(shootingCoroutine);
        }
    }

    private void SetUpBoundires() {
        Camera gameCamera = Camera.main;

        minX = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        maxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        minY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + paddingDown;
        maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - topPadding;
    }

    private void Move()
    {
        float deltaXInput = Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed;
        float deltaYInput = Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed;

        var newXPosition = Mathf.Clamp(transform.position.x + deltaXInput, minX, maxX);
        var newYPosition = Mathf.Clamp(transform.position.y + deltaYInput, minY, maxY);

        transform.position = new Vector2(newXPosition, newYPosition);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer) {
        playerHealth = playerHealth - damageDealer.GetDamage();
        damageDealer.Hit(gameObject);
        if(playerHealth <= 0) {
            TriggerPlayerExplodeSFX();
            Destroy(this.gameObject);
            GameObject explosionFX = Instantiate(explosionVFX, this.transform.position, Quaternion.identity);
            level.LoadGameOver();
        }
    }

    private void TriggerProjectileShoud() {
        AudioSource.PlayClipAtPoint(shootingProjectileSFX, this.transform.position);
    }

    private void TriggerPlayerExplodeSFX() {
        AudioSource.PlayClipAtPoint(playerExplodeSFX, this.transform.position);
    }
}
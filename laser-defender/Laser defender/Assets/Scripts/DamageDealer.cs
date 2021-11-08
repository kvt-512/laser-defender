using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage;

    float unhitProjectileLifeTime = 6F;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SelfDestoryWhenNotHit();
    }

    public int GetDamage() {
        return damage;
    }

    public void Hit(GameObject gameObject) {
        Destroy(gameObject);
    }

    private void SelfDestoryWhenNotHit() {
        Destroy(this.gameObject, unhitProjectileLifeTime);
    }
}
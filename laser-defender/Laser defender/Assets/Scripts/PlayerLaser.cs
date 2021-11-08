using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    float shootingSpeed = 30;
    float upperBound;

    // Start is called before the first frame update
    void Start()
    {
        upperBound = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerShoot();
    }
    public void PlayerShoot() {
        transform.Translate(Vector3.up * Time.deltaTime * shootingSpeed, Space.World);
        float curretYPos = this.transform.position.y;
        if (curretYPos >= upperBound) {
            Destroy(gameObject);
        }
    }
}
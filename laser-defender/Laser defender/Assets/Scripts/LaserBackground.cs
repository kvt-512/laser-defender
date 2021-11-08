using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBackground : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 1;
    // [SerializeField] float playerDirectionSpeed = 0.01F;
    Vector2 offSetY;
    Vector2 offSetX;
    Material myMaterial;

    void Start() {
        myMaterial = GetComponent<Renderer>().material;
        offSetY = new Vector2(0F, scrollSpeed);
        // offSetX = new Vector2(playerDirectionSpeed, offSetY.y);
    }

    void Update() {
        myMaterial.mainTextureOffset += offSetY * Time.deltaTime;
        // RespondToPlayerDirection(); 
    }

    // private void RespondToPlayerDirection() {
    //     float deltaXInput = Input.GetAxis("Horizontal") * Time.deltaTime;
    //     myMaterial.mainTextureOffset -= new Vector2(playerDirectionSpeed + deltaXInput, offSetY.y) * Time.deltaTime;
    //     myMaterial.mainTextureOffset += offSetX * Time.deltaTime;
    // }
}
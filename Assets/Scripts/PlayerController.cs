using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidBody;
    private float jumpForce = 1000;
    public float gravityModifier;
    public bool isOnTheGround;

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier = 3;
    }

    void Update()
    {
        // && = si se cumple la variable isOnThGround, se efectua el salto
        if (Input.GetKeyDown(KeyCode.Space) && isOnTheGround)
        {
            playerRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnTheGround = false;
        }
    }

    // Esta función detecta si el Player está colisionando con cualquier objeto del entorno
    private void OnCollisionEnter(Collision otherCollider)
    {
        // Si mi etiqueta se llama Ground, el Player puede saltar
        if (otherCollider.gameObject.CompareTag("Ground"))
        {
            isOnTheGround = true;
        }

        else if (otherCollider.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log(message: "GAME OVER");
            Time.timeScale = 0;
        }
    }
}

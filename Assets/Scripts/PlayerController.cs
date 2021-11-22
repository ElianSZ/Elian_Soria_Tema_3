using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidBody;
    private Animator playerAnimator;
    private AudioSource playerAudioSource;
    private AudioSource cameraAudioSource;
    private float jumpForce = 1000;
    public float gravityModifier;
    public bool isOnTheGround;
    public bool gameOver;
    public ParticleSystem explosionParticleSystem;
    public ParticleSystem dirtParticleSystem;
    public AudioClip JumpClip;
    public AudioClip ExplosionClip;

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        playerAudioSource = GetComponent<AudioSource>();
        cameraAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier = 3f;
    }

    void Update()
    {
        // && = si se cumple las variables isOnThGround y gameOver, se efectua el salto
        // "!gameOver" = "gameOver == false (la exclamación indica lo contrario a la variable)"
        if (Input.GetKeyDown(KeyCode.Space) && isOnTheGround && !gameOver)
        {
            playerRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnimator.SetTrigger("Jump_trig");
            isOnTheGround = false;
            dirtParticleSystem.Stop();                                                  // Pausa el efecto de partículas
            playerAudioSource.PlayOneShot(JumpClip, 1f);                                // Ejecuta una vez el audio de saltar
        }
    }

    // Esta función detecta si el Player está colisionando con cualquier objeto del entorno
    private void OnCollisionEnter(Collision otherCollider)
    {
        // Si no estamos muertos, ejecuta todo
        if (!gameOver)
        {
            // Si mi etiqueta se llama Ground, el Player puede saltar
            if (otherCollider.gameObject.CompareTag("Ground") && !gameOver)
            {
                isOnTheGround = true;
                dirtParticleSystem.Play();                                              // Ejecuta el efecto de partículas cuando le das al play
            }

            else if (otherCollider.gameObject.CompareTag("Obstacle"))
            {
                int randomDeathType = Random.Range(1, 3);                               // Indica que el rango de tipos de muerte será entre 1 y 2
                playerAnimator.SetBool("Death_b", true);                                // Indica que pasa de vivo a muerto
                playerAnimator.SetInteger("DeathType_int", randomDeathType);            // Indica que animación de muerte tiene

                explosionParticleSystem.Play();                                         // Ejecuta el efecto de partículas

                playerAudioSource.PlayOneShot(ExplosionClip, 1f);                       // Ejecuta una vez el audio de explosión
                cameraAudioSource.volume = 0.1f;                                       // Baja el volumen de la música

                gameOver = true;
                Debug.Log(message: "GAME OVER");
            }
        }
    }
}

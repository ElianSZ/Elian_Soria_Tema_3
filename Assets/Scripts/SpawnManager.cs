using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float startDelay = 2.0f;
    private float repeatRate = 1.5f;
    private Vector3 spawnPos = new Vector3(35, 0, 0);
    private PlayerController playerControllerScript;
    public GameObject[] obstaclePrefabs;

    // Start is called before the first frame update
    void Start()
    {
        // Repetimos la función SpawnObstacle cada 1.5 segundos
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnObstacle()
    {
        // Estamos accediendo a las variables del script PlayerController, en concreto, a la variable GameOver
        // También se puede hacer así: if (!playerControllerScript.GameOver)
        // !gameOver = no estamos muertos
        if (playerControllerScript.gameOver == false)
        {
            // obstaclePrefabs.Length = indica la longitud máxima de prefabs que he puesto en SpawnManager
            int randomIndex = Random.Range(0, obstaclePrefabs.Length);
            Instantiate(obstaclePrefabs[randomIndex], spawnPos, obstaclePrefabs[randomIndex].transform.rotation);
        }
    }
}

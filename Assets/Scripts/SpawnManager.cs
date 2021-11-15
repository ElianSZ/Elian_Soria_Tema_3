using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float startDelay = 2.0f;
    public float repeatRate = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnObstacle()
    {
        Instantiate(obstaclePrefab, new Vector3(35, 0, 0), obstaclePrefab.transform.rotation);
    }
}

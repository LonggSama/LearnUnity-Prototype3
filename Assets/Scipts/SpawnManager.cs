using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefab;
    [SerializeField] private Vector3 spawnPosition = new Vector3(25, 0 , 0);
    [SerializeField] private float startDelay = 0.5f;
    [SerializeField] private float repeatTime = 2f;

    private PlayerController playerControllerScipt;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScipt = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("spawnObstacle", startDelay, repeatTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // Spawn Ostacle
    private void spawnObstacle()
    {
        int ostacleIndex = Random.Range(0, obstaclePrefab.Length);
        if (playerControllerScipt.gameOver == false && playerControllerScipt.gameStart)
        {
            Instantiate(obstaclePrefab[ostacleIndex], spawnPosition, obstaclePrefab[ostacleIndex].transform.rotation);
        }
    }
}

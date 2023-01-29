using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float leftBound = -15f;

    private PlayerController playerControllerScipt;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScipt = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        moveLeft();
        destroyObstacle();
    }

    // Object Move
    void moveLeft()
    {
        if (!playerControllerScipt.gameOver && playerControllerScipt.gameStart)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    }

    // Destroy Obstacle
    private void destroyObstacle()
    {
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private PlayerController playerController;

    public Text scoreText;
    public GameObject superSpeed;
    public float scoreAmount;
    public float pointIncreasePerSecond;

    private void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        scoreAmount = 0;
        pointIncreasePerSecond = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
        SuperSpeedText();
    }

    // Update Score
    void UpdateScore()
    {
        if (!playerController.gameOver)
        {
            if (playerController.isSuperSpeed)
            {
                scoreText.text = (int)scoreAmount + " Score X5";
                scoreAmount += pointIncreasePerSecond * 5 * Time.deltaTime;
            }
            else
            {
                scoreText.text = (int)scoreAmount + " Score";
                scoreAmount += pointIncreasePerSecond * Time.deltaTime;
            }  
        }
        else
        {
            scoreText.text = (int)scoreAmount + " Score";
            Debug.Log("Score: " + (int)scoreAmount);
        }
    }

    // Hide SuperSpeed Text
    void SuperSpeedText()
    {
        if (!playerController.isSuperSpeed && !playerController.gameOver)
        {
            superSpeed.SetActive(false);
        }
        else if (playerController.isSuperSpeed && !playerController.gameOver)
        {
            superSpeed.SetActive(true);
        }
        else if (playerController.gameOver)
        {
            superSpeed.SetActive(false);
        }  
    }
}

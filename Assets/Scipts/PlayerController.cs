using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudioSource;

    [SerializeField] private AudioClip[] crashSounds;
    [SerializeField] private AudioClip[] jumpSounds;
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private ParticleSystem dirtParticle;
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private float gravityModifier;
    [SerializeField] private GameObject dirt;

    public float playerSpeed = 1.5f;
    public int startPoint = 4;


    public bool gameStart = false;
    public bool gameOver;
    public bool isSuperSpeed;
    private int jumpCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameStart = false;
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudioSource = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStart)
        {
            dirt.SetActive(false);
        }
        else if (gameStart)
        {
            dirt.SetActive(true);
        }
        playerJump();
        superSpeed();
        PlayerMove();
    }


    //Get SpaceBar to Player Jump
    void playerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount <= 1 && !gameOver && gameStart)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;
            // Stop Particle
            dirtParticle.Stop();
            // Jump Anim
            playerAnim.SetTrigger("Jump_trig");
            // Jump Sound
            playerAudioSource.PlayOneShot(jumpSounds[Random.Range(0, jumpSounds.Length)], 0.5f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            dirtParticle.Play(gameStart);
            jumpCount = 0;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            dirtParticle.Stop();
            explosionParticle.Play();
            playerAudioSource.PlayOneShot(crashSounds[Random.Range(0, crashSounds.Length)],0.5f);
            Debug.Log("Game Over!");
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
        }
    }

    // Super Speed
    void superSpeed()
    {
        if (gameStart)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Time.timeScale = 2;
                isSuperSpeed = true;
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                Time.timeScale = 1;
                isSuperSpeed = false;
            }
        }
    }

    //Press Enter To Player Move
    void PlayerMove()
    {
        playerAnim.SetFloat("Speed_f", 0.5f);
        playerRb.transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed);
        if (transform.position.x > startPoint)
        {
            transform.position = new Vector3(startPoint, transform.position.y,transform.position.z);
            gameStart = true;
            playerAnim.SetFloat("Speed_f", 1f);
        }
    }
}

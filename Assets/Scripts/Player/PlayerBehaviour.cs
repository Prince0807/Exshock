using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;

    [Header("Audio")]
    public AudioClip jumpSFX;
    public AudioClip deathSFX;
    private AudioSource _audioSource;

    [Header("Movement")]
    [SerializeField] private float speed;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    public Transform groundPoint;
    public float groundRadius;
    public LayerMask groundLayerMask;
    private bool isGrounded;

    [Header("UI")]
    public GameObject levelCompletionUIPanel;
    public GameObject gameOverUIPanel;

    private bool isDead = false;
    private bool isLevelFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isLevelFinished)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            else if (isDead)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            else
                Jump();
        }
        if(Input.GetButtonDown("Slide"))
            _animator.SetTrigger("Slide");
    }
    private void FixedUpdate()
    {
        if (isLevelFinished || isDead)
            return;

        _rigidbody2D.velocity = new Vector2(speed, _rigidbody2D.velocity.y);
        // Check if Player is on ground or not
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, groundRadius, groundLayerMask);
        _animator.SetBool("isGrounded", isGrounded);
    }

    private void Jump()
    {
        if (isGrounded)
        {
            _rigidbody2D.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            _animator.SetTrigger("Jump");
            _audioSource.clip = jumpSFX;
            _audioSource.Play();
        }        
    }

    private void Death()
    {
        isDead = true;
        gameOverUIPanel.SetActive(true);
        _rigidbody2D.velocity = Vector3.zero;
        _audioSource.clip = deathSFX;
        _audioSource.Play();

    }
    private void Win()
    {
        isLevelFinished = true;
        _rigidbody2D.velocity = Vector3.zero;
        levelCompletionUIPanel.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DeathZone")
            Death();
        else if (collision.gameObject.name == "Win")
            Win();
    }
}

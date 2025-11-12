using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    private Rigidbody2D m_playerRigidbody;

    [SerializeField] private InputActionAsset m_inputFile;
    private InputAction m_moveAction;
    private InputAction m_jumpAction;

    [SerializeField] private float m_playerSpeed;
    [SerializeField] private float m_speedMultiplier;
    private float m_elapsed;
    private float m_delay = 5.0f;
    private float m_timer = 0;

    [SerializeField] private float m_playerJumpForce;
    private bool m_isOnGround;
    private bool m_canJump;
    private bool m_isStarted = false;
    public bool IsStarted => m_isStarted;

    [SerializeField] private Sprite m_playerRunSprite;
    [SerializeField] private Sprite m_playerJumpSprite;

    [SerializeField] private Animator m_playerAnimator;

    [SerializeField] private AudioSource m_soundsSource;
    [SerializeField] private AudioClip m_jumpSFX;
    private bool m_spaceKeyPressed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_playerAnimator = GetComponent<Animator>();
        m_playerRigidbody = GetComponent<Rigidbody2D>();

        m_isOnGround = false;
        m_canJump = false;

        m_moveAction = m_inputFile.FindAction("Move");
        m_jumpAction = m_inputFile.FindAction("Jump");
    }

    // Update is called once per frame
    void Update()
     {
        // TODO: Wait for player to press key to start
        Running();
        Jumping();
        m_elapsed += Time.deltaTime;
        if (m_elapsed >= m_delay)
        {
            m_playerSpeed *= m_speedMultiplier;
            m_elapsed = 0.0f;
        }
        m_timer += Time.deltaTime;
        if(m_timer >= 1)
        {
            UI_Manager.UpdateScore(10);
            m_timer = 0;
        }
    }

    private void Running()
    {
        Vector3 calculatedVeclocity = m_playerRigidbody.linearVelocity;
        calculatedVeclocity.x = m_playerSpeed;
        m_playerRigidbody.linearVelocity = calculatedVeclocity;
        if (calculatedVeclocity.magnitude > 0)
        {
            m_playerAnimator.SetBool("IsRunning", true);
        }
        else
        {
            m_playerAnimator.SetBool("IsRunning", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        m_isOnGround = true;
        m_canJump = true;
    }

    // Gael m'a aidé avec cette partie de mon code pour faire le double jump

    private void Jumping()
    {
        m_spaceKeyPressed = m_jumpAction.WasPressedThisFrame();
        if (m_isOnGround && m_canJump && m_spaceKeyPressed)
        {
            Jump();
            // m_playerAnimator.SetBool("IsJumping", true);
            m_isOnGround = false;

        }
        else if (m_isOnGround == false && m_canJump && m_spaceKeyPressed)
        {
            Jump();
            // m_playerAnimator.SetBool("IsJumping", true);
            m_canJump = false;
        }
    }
    private void Jump()
    {
		m_soundsSource.clip = m_jumpSFX;
		m_soundsSource.Play();
		m_playerRigidbody.AddForce(Vector2.up * m_playerJumpForce, ForceMode2D.Impulse);
	}
    public void CollectCoin()
    {
        UI_Manager.UpdateScore(100);
    }
    public void HitObstacle()
    {
        UI_Manager.Save();
        SceneManager.LoadScene("GameOver");
    }
}

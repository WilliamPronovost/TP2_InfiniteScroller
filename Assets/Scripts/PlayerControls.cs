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

    [SerializeField] private float m_playerJumpForce;
    [SerializeField] private float m_playerDoubleJumpForce;
    private bool m_isOnGround = true;

    [SerializeField] private Sprite m_playerRunSprite;
    [SerializeField] private Sprite m_playerJumpSprite;

    [SerializeField] private Animator m_playerAnimator;

    [SerializeField] private AudioSource m_soundsSource;
    [SerializeField] private AudioClip m_jumpSFX;

    private int m_jumpCount = 0;
    private int m_highScore = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_playerAnimator = GetComponent<Animator>();
        m_playerRigidbody = GetComponent<Rigidbody2D>();
        m_soundsSource = GetComponent<AudioSource>();

        m_moveAction = m_inputFile.FindAction("Move");
        m_jumpAction = m_inputFile.FindAction("Jump");
	}

    // Update is called once per frame
    void Update()
    {
		Running();
        bool spaceKeyPressed = m_jumpAction.WasPressedThisFrame();
        if (spaceKeyPressed && m_isOnGround && m_jumpCount == 0)
        {
            Jumping();
            m_isOnGround = false;
            m_jumpCount += 1;
            if(spaceKeyPressed && !m_isOnGround && m_jumpCount == 1)
            {
                DoubleJumping();
                m_jumpCount -= 1;
            }
        }

        m_elapsed += Time.deltaTime;
        if (m_elapsed >= m_delay)
        {
            m_playerSpeed *= m_speedMultiplier;
            m_elapsed = 0.0f;
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
        m_jumpCount = 0;
	}
    private void Jumping()
    {
        m_soundsSource.clip = m_jumpSFX;
        m_soundsSource.Play();
		m_playerRigidbody.AddForce(Vector3.up * m_playerJumpForce, ForceMode2D.Impulse);
        m_playerAnimator.SetBool("IsJumping", true);
    }
    private void DoubleJumping()
    {
        m_soundsSource.clip = m_jumpSFX;
        m_soundsSource.Play();
        m_playerRigidbody.AddForce(Vector3.up * m_playerDoubleJumpForce, ForceMode2D.Impulse);
        m_playerAnimator.SetBool("IsJumping", true);
    }
    public void CollectCoin()
    {
        m_highScore+= 100;
	}
    public void HitObstacle()
    {
        SceneManager.LoadScene("GameOver");
	}
}

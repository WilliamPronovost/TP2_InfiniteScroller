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
    private float m_elapsed;
    private float m_delay = 5.0f;

    [SerializeField] private float m_playerJumpForce;
	private bool m_isOnGround = true;

    [SerializeField] private Sprite m_playerRunSprite;
    [SerializeField] private Sprite m_playerJumpSprite;

    [SerializeField] private Animator m_playerAnimator;

    [SerializeField] private AudioSource m_soundsSource;
    [SerializeField] private AudioClip m_jumpSFX;
	[SerializeField] private AudioClip m_deathSFX;
	[SerializeField] private AudioClip m_gameSoundtrackSFX;

	private int m_highScore = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_playerAnimator = GetComponent<Animator>();
        m_playerRigidbody = GetComponent<Rigidbody2D>();
        m_soundsSource = GetComponent<AudioSource>();

        m_moveAction = m_inputFile.FindAction("Move");
        m_jumpAction = m_inputFile.FindAction("Jump");

		m_soundsSource.clip = m_gameSoundtrackSFX;
		m_soundsSource.Play();
	}

    // Update is called once per frame
    void Update()
    {
		Running();
        bool spaceKeyPressed = m_jumpAction.WasPressedThisFrame();
        if (spaceKeyPressed && m_isOnGround)
        {
            Jumping();
            m_isOnGround = false;
        }
        m_elapsed += Time.deltaTime;
        if (m_elapsed >= m_delay)
        {
            m_playerSpeed *= 1.3f;
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
	}
    private void Jumping()
    {
        m_soundsSource.clip = m_jumpSFX;
        m_soundsSource.Play();
		m_playerRigidbody.AddForce(Vector3.up * m_playerJumpForce, ForceMode2D.Impulse);
        m_playerAnimator.SetBool("IsJumping", true);
    }
    public void CollectCoin()
    {
        m_highScore+= 100;
	}
    public void HitObstacle()
    {
        m_soundsSource.clip = m_deathSFX;
        m_soundsSource.Play();
        SceneManager.LoadScene("GameOver");
	}
}

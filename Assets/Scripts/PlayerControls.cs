using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private Animator m_playerAnimator;
    [SerializeField] private InputActionAsset m_inputFile;
    [SerializeField] private Sprite m_playerRunSprite;
    [SerializeField] private Sprite m_playerJumpSprite;
    [SerializeField] private float m_playerSpeed;
    [SerializeField] private float m_playerJumpForce;
    private bool m_isOnGround = true;
    private float m_elapsed;
    private float m_delay = 5.0f;
    private int m_highScore = 0;
	private InputAction m_moveAction;
    private InputAction m_jumpAction;
    private Rigidbody2D m_playerRigidbody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_playerAnimator = GetComponent<Animator>();
        m_playerRigidbody = GetComponent<Rigidbody2D>();
        m_moveAction = m_inputFile.FindAction("Move");
        m_jumpAction = m_inputFile.FindAction("Jump");
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
        if(calculatedVeclocity.magnitude > 0)
        {
            m_playerAnimator.SetBool("IsRunning", true);
        }
        else
        {
            m_playerAnimator.SetBool("IsRunning", false);
        }
    }
    private void Jumping()
    {
        m_playerRigidbody.AddForce(Vector3.up * m_playerJumpForce, ForceMode2D.Impulse);
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

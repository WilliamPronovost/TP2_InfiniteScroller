using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private InputActionAsset m_inputFile;
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
            m_playerSpeed *= 1.1f;
            m_elapsed = 0.0f;
        }
    }
    private void Running()
    {
        Vector3 calculatedVeclocity = m_playerRigidbody.linearVelocity;
        calculatedVeclocity.x = m_playerSpeed;
        m_playerRigidbody.linearVelocity = calculatedVeclocity;
	}
    private void Jumping()
    {
        m_playerRigidbody.AddForce(Vector3.up * m_playerJumpForce, ForceMode2D.Impulse);
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

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class KeyboardAndButtons : MonoBehaviour
{
	[SerializeField] private InputActionAsset m_inputFile;
	private InputAction m_goingToMenuAction;
	private InputAction m_startingNewGameAction;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		m_startingNewGameAction = m_inputFile.FindAction("GoToGame");
		m_goingToMenuAction = m_inputFile.FindAction("GoToMenu");
	}

    // Update is called once per frame
    void Update()
    {
		bool enterKeyPressed = m_startingNewGameAction.WasPressedThisFrame();
		bool escapeKeyPressed = m_goingToMenuAction.WasPressedThisFrame();
		if (enterKeyPressed)
		{
			SceneManager.LoadScene("Game");
		}
		if (escapeKeyPressed)
		{
			SceneManager.LoadScene("MainMenu");
		}
	}
	public void OnClickedYesOrPlayGame()
	{
		SceneManager.LoadScene("Game");
	}
	public void OnClickedNo()
	{
		SceneManager.LoadScene("MainMenu");
	}
	public void OnClickedQuit()
	{
		Application.Quit();
	}
	public void OnClickedMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}
}

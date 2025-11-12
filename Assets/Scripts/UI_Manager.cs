using TMPro;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    static private UI_Manager instance;
    [SerializeField] private TMP_Text m_currentScoreText;
    [SerializeField] private TMP_Text m_highScoreText;
    private int m_score = 0;
    private int m_highScore;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	private void Awake()
	{
		instance = this;
        Load();
	}
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    private void Load()
    {
        if (PlayerPrefs.HasKey("highScore"))
        {
            m_highScore = PlayerPrefs.GetInt("highScore");
        }
		else
		{
            m_highScore = 0;
		}
        m_highScoreText.text = m_highScore.ToString();
	}
    public static void Save()
    {
        if(instance.m_score > instance.m_highScore)
        {
            PlayerPrefs.SetInt("highScore", instance.m_score);
        }
    }
    public static void UpdateScore(int amount)
    {
        instance.m_score += amount;
		instance.m_currentScoreText.text = instance.m_score.ToString();
	}
}

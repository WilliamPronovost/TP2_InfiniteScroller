using UnityEngine;

public class GameOverSound : MonoBehaviour
{
    [SerializeField] private AudioSource m_soundSource;
    [SerializeField] private AudioClip m_deathSFX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_soundSource = GetComponent<AudioSource>();
        m_soundSource.clip = m_deathSFX;
        m_soundSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

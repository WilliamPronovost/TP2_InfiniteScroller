using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    [SerializeField] private AudioSource m_musicSource;
    [SerializeField] private AudioClip m_gameSoundtrackSFX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_musicSource = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        m_musicSource.clip = m_gameSoundtrackSFX;
        m_musicSource.Play();
    }
}

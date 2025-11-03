using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform m_player;
    [SerializeField] private Vector3 m_offset;
	// Update is called once per frame
	void Update()
    {
        transform.position = m_player.position - m_offset;
	}
}

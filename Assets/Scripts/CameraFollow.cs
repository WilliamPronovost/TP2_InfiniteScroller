using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform m_player;
    [SerializeField] private Vector3 m_offset;
	// Update is called once per frame
	void Update()
    {
		float playerX = m_player.position.x - m_offset.x;
		if (playerX < transform.position.x)
			return;
		Vector3 pos = transform.position;
		pos.x = playerX;
		transform.position = pos;
	}
}

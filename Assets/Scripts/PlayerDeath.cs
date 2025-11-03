using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		PlayerControls player = collision.GetComponent<PlayerControls>();
		if(player != null)
		{
			player.HitObstacle();
		}
	}
}

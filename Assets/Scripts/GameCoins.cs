using UnityEngine;

public class GameCoins : MonoBehaviour {
	private void OnTriggerEnter2D(Collider2D collision)
	{
		PlayerControls player = collision.GetComponent<PlayerControls>();
		if (player != null)
		{
			Destroy(gameObject);
			player.CollectCoin();
		}
	}
}

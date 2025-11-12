using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] m_platformTypes;
    //[SerializeField] private Transform[] m_platformCache = { };
    [SerializeField] private GameObject m_greenPlatformPrefab;
	[SerializeField] private GameObject m_yellowPlatformPrefab;
	[SerializeField] private GameObject m_whitePlatformPrefab;
	[SerializeField] private GameObject m_coinPrefab;
	[SerializeField] private GameObject m_sawPrefab;

	//[SerializeField] private PlayerControls m_player;
	[SerializeField] private Transform m_playerPosition;
	[SerializeField] private Transform m_spawner;
    [SerializeField] private Transform m_startingPlatform;


	[SerializeField] private float m_platformTypeSpawnChance;
    [SerializeField] private float m_distanceForPlatformSpawn;
    [SerializeField] private float m_intervalX;
    [SerializeField] private float m_platformLength;
    [SerializeField] private float m_playerRange;

	private List<Transform> m_platformCache;
	private List<Transform> m_coinCache;
    private List<Transform> m_sawCache;
    [SerializeField] private int m_maxPlatformCount;
    [SerializeField] private int m_currentPlatformCount; 
	[SerializeField] private int m_maxCoinCount;
    [SerializeField] private int m_maxSawCount;

    private Transform m_lastCreatedPlatform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_platformCache = new List<Transform>();
        m_coinCache = new List<Transform>();
        m_sawCache = new List<Transform>();
        m_lastCreatedPlatform = m_startingPlatform;
    }

    // Update is called once per frame
    void Update()
    {
        //if (!m_player.IsStarted)
        //    return;

        if (Vector3.Distance(m_playerPosition.position, m_lastCreatedPlatform.position) < m_distanceForPlatformSpawn)
        {
            Vector3 spawnDistance = new Vector3(m_intervalX, 0, 0);

            Transform platformEndPoint = m_lastCreatedPlatform.Find("End");
            float coinRangeX = Random.Range(m_greenPlatformPrefab.transform.position.x - m_platformLength, m_greenPlatformPrefab.transform.position.x + m_platformLength);
            float coinRangeY = Random.Range(m_greenPlatformPrefab.transform.position.y + 1, m_greenPlatformPrefab.transform.position.y + m_playerRange);
            Vector3 coinSpawningRange = new Vector3(coinRangeX, coinRangeY, 0);
            Vector3 coinSpawnDistance = platformEndPoint.position + coinSpawningRange + spawnDistance;

            float sawRangeX = Random.Range(m_greenPlatformPrefab.transform.position.x - m_platformLength, m_greenPlatformPrefab.transform.position.x + m_platformLength);
            float sawRangeY = m_greenPlatformPrefab.transform.position.y + 1.5f;
            Vector3 sawSpawningRange = new Vector3(sawRangeX, sawRangeY, 0);
            Vector3 sawSpawnDistance = platformEndPoint.position + sawSpawningRange + spawnDistance;
            if (m_playerPosition.position.x == m_spawner.position.x)
            {
                spawnDistance = Vector3.zero;
            }
            m_lastCreatedPlatform = SpawnPosition(platformEndPoint.position + spawnDistance, coinSpawnDistance, sawSpawnDistance);
            m_platformCache.Add(m_lastCreatedPlatform);
            m_coinCache.Add(platformEndPoint);
            if (m_platformCache.Count > m_maxPlatformCount)
            {
                Transform platformToDelete = m_platformCache[0];
                Destroy(platformToDelete.gameObject);
                m_platformCache.RemoveAt(0);
            }
			if (m_coinCache.Count > m_maxCoinCount)
            {
				Transform coinToDelete = m_coinCache[0];
				Destroy(coinToDelete.gameObject);
				m_coinCache.RemoveAt(0);
            }
            if (m_sawCache.Count > m_maxSawCount)
            {
                Transform sawToDelete = m_sawCache[0];
                Destroy(sawToDelete.gameObject);
                m_sawCache.RemoveAt(0);
            }
        }
    }

    private Transform SpawnPosition(Vector3 spawnPosition, Vector3 coinPosition, Vector3 sawPosition)
    {
		GameObject newCoin = Instantiate(m_coinPrefab, coinPosition, Quaternion.identity);
        GameObject newSaw = Instantiate(m_sawPrefab, sawPosition, Quaternion.identity);
        GameObject newPlatform = Instantiate(m_greenPlatformPrefab, spawnPosition, Quaternion.identity);
	    float randomData = Random.Range(0f, 1f);
		if (randomData == m_platformTypeSpawnChance)
		{
            Instantiate(m_greenPlatformPrefab, spawnPosition, Quaternion.identity);
		}
		if (randomData > m_platformTypeSpawnChance)
		{
            Instantiate(m_yellowPlatformPrefab, spawnPosition, Quaternion.identity);
		}
		if (randomData < m_platformTypeSpawnChance)
		{
			Instantiate(m_whitePlatformPrefab, spawnPosition, Quaternion.identity);
		}
			return newPlatform.transform;
        //GameObject newSaw = Instantiate(m_sawPrefab, spawnPosition, Quaternion.identity);
    }
}

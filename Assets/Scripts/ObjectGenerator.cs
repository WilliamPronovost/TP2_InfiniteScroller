using NUnit.Framework;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    //[SerializeField] private GameObject[] m_platformPrefab = { };
    //[SerializeField] private Transform[] m_platformCache = { };
    [SerializeField] private GameObject m_platformPrefab;
	[SerializeField] private GameObject m_coinPrefab;
	[SerializeField] private GameObject m_sawPrefab;
	[SerializeField] private Transform m_player;
    [SerializeField] private Transform m_spawner;
    [SerializeField] private Transform m_startingPlatform;
    [SerializeField] private float m_distanceForPlatformSpawn;
    [SerializeField] private float m_intervalX;
    [SerializeField] private float m_platformTypeSpawnChance;
   

    private List<Transform> m_platformCache;
    [SerializeField] private int m_maxPlatformCount;

    private Transform m_lastCreatedPlatform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_platformCache = new List<Transform>();
        m_lastCreatedPlatform = m_startingPlatform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(m_player.position, m_lastCreatedPlatform.position) < m_distanceForPlatformSpawn)
        {
            Vector3 spawnDistance = new Vector3(m_intervalX, 0, 0);
            Transform platformEndPoint = m_lastCreatedPlatform.Find("End");
            if (m_player.position.x == m_spawner.position.x)
            {
                spawnDistance = Vector3.zero;
            }
            m_lastCreatedPlatform = SpawnPosition(platformEndPoint.position + spawnDistance);
            m_platformCache.Add(m_lastCreatedPlatform);

            if (m_platformCache.Count > m_maxPlatformCount)
            {
                Transform platformToDelete = m_platformCache[0];
                Destroy(platformToDelete.gameObject);
                m_platformCache.RemoveAt(0);
            }
        }
    }

    private Transform SpawnPosition(Vector3 spawnPosition)
    {
        GameObject newPlatform = Instantiate(m_platformPrefab, spawnPosition, Quaternion.identity);
        //GameObject newCoin = Instantiate(m_coinPrefab, spawnPosition, Quaternion.identity);
        //GameObject newSaw = Instantiate(m_sawPrefab, spawnPosition, Quaternion.identity);
        return newPlatform.transform;
    }
}

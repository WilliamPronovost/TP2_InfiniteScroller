using UnityEngine;
using UnityEngine.UIElements;

public class SawMovement : MonoBehaviour
{
    [SerializeField] private float m_sawSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-1, 0, 0) * m_sawSpeed * Time.deltaTime);
    }
}

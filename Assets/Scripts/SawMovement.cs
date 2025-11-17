using UnityEngine;
using UnityEngine.UIElements;

public class SawMovement : MonoBehaviour
{
    [SerializeField] private float m_sawSpeed;
    [SerializeField] private float m_sawDirection;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(m_sawDirection, 0, 0) * m_sawSpeed * Time.deltaTime);
    }
}

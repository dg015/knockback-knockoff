using Cinemachine;
using UnityEngine;

public class getBounds : MonoBehaviour
{
    [SerializeField] private CinemachineConfiner2D cinemachineConfiner;
    [SerializeField] private PolygonCollider2D polygonCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        polygonCollider = FindAnyObjectByType<PolygonCollider2D>().GetComponent<PolygonCollider2D>();
        cinemachineConfiner.m_BoundingShape2D = polygonCollider;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

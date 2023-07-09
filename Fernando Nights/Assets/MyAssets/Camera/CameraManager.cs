using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private float speedFollow = 5;
    [SerializeField]
    private Transform target;
    void Start()
    {
        target = FindObjectOfType<PlayerMotor>().gameObject.transform;
    }
    void FixedUpdate()
    {
        Vector3 newPosition = target.position;
        newPosition.y = target.position.y + 0.8f;
        newPosition.z = -10;
        transform.position = Vector3.Slerp(transform.position, newPosition, speedFollow * Time.deltaTime);
    }
}

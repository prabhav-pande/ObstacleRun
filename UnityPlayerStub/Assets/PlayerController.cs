using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float mouseSens, sprintSpeed, walkSpeed, jumpForce, smoothTime;

    Rigidbody rb;

    void Awake()
    {
        rb.GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSens);
    }
}

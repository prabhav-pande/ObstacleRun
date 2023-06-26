using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviourPun
{
    private float speed = 2f;
    private CharacterController controller = null;

    void Start() => controller = GetComponent<CharacterController>();

    void Update()
    {
        if (photonView.IsMine)
        {
            TakeInput();
        }
    }

    private void TakeInput()
    {
        Vector3 movement = new Vector3
        {
            x = Input.GetAxisRaw("Horizontal"),
            y = 0f,
            z = Input.GetAxisRaw("Vertical"),
        }.normalized;

        //controller.SimpleMove(movement * speed);
    }
}

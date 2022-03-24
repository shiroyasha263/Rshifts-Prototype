using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField]
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;
    public float jetpackThrust = 10f;
    public float unstabilityFactor = 0.01f;

    public bool jetpack;


    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        if (!view.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
        }
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * playerSpeed);

            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            if (Input.GetButtonDown("Fire1"))
            {
                jetpack = !jetpack;
            }

            controller.Move(move * Time.deltaTime * playerSpeed);

            if (Input.GetButton("Jump") && jetpack)
            {
                playerVelocity.y += jetpackThrust * Time.deltaTime;
                playerVelocity.y = Mathf.Clamp(playerVelocity.y, 0, 10);
            }

            if (!jetpack)
            {
                playerVelocity.y += gravityValue * Time.deltaTime;
            }
            else
            {
                if (playerVelocity.y > 2)
                {
                    playerVelocity.y += gravityValue * Time.deltaTime / 2;
                }
                playerVelocity.y += gravityValue * Time.deltaTime / 20;
            }

            controller.Move(playerVelocity * Time.deltaTime);
        }
    }
}

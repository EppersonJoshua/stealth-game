using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isDead { get; set; }
    public bool playerIsMoving { get; private set; }
    public bool playerIsJumping { get; private set; }
    private Camera cam;
    [SerializeField]
    private int camMode = 0;
    [SerializeField]
    private float playerSpeed, playerJumpPower = 0;

    private void Awake()
    {
        cam = Camera.main;
        camMode = 2;
        playerIsJumping = false;
        playerIsMoving = false;
    }
    void OnCollisionEnter(Collision col)
    {
        playerIsJumping = false;
    }
    void OnCollisionExit(Collision col)
    {
        playerIsJumping = true;
    }
    private void FixedUpdate()
    {
        if (isDead == false)
        {
            if (playerIsJumping == false && Input.GetKeyDown(KeyCode.Space))
            {
                playerIsJumping = true;
                GetComponent<Rigidbody>().AddForce(Vector3.up * playerJumpPower, ForceMode.Acceleration);
                GetComponent<Animation>().Play("Jump");
            }

            if (Input.GetKeyDown(KeyCode.I) && camMode > 1)
            {
                camMode--;
            }
            else if (Input.GetKeyDown(KeyCode.O) && camMode != 3)
            {
                camMode++;
            }

            switch (camMode)
            {
                case 1:
                    cam.fieldOfView = 40;
                    break;
                case 2:
                    cam.fieldOfView = 70;
                    break;
                case 3:
                    cam.fieldOfView = 100;
                    break;
            }
            if (Input.GetAxis("Horizontal") == 0f && Input.GetAxis("Vertical") == 0f)
            {
                playerIsMoving = true;
                GetComponent<Animation>().Play("Happy Idle");
            }
            else
            {
                playerIsMoving = false;
                if (playerIsJumping == false)
                {
                    GetComponent<Animation>().Play("Running");

                }
                transform.Translate(0f, 0f, playerSpeed * Time.unscaledDeltaTime * Input.GetAxis("Vertical"), Space.Self);
                transform.eulerAngles += new Vector3(0f, Input.GetAxis("Horizontal"), 0f);
            }
        }
    }
}

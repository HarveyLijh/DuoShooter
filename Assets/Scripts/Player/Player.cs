﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform = null;
    private bool jumpKeyWasPressed;
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody rigibodyComponent;
    [SerializeField] private LayerMask playerMask;
    private int superJumpsRemaining;
    [SerializeField] private float moveSpeed;
    private Camera mainCamera;
    public GunController[] theGuns;
    private GunController currentGun;
    private int currentGunIndex;
    private UI_GunInfo myGunInfo;

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < theGuns.Length; i++)
        {
            theGuns[i].gameObject.SetActive(false);
        }
        rigibodyComponent = GetComponent<Rigidbody>();
        mainCamera = GameObject.Find("TopDownMainCamera").GetComponent<Camera>();
        myGunInfo = gameObject.GetComponent<UI_GunInfo>();
        if (theGuns.Length > 0)
        {
            updateGunInfo(0, 0);

        }

    }
    void updateGunInfo(int prevIndex, int calculation)
    {
        theGuns[prevIndex].gameObject.SetActive(false);
        int index = mod(prevIndex + calculation, theGuns.Length);
        //Debug.Log(index);
        currentGun = theGuns[index];
        currentGunIndex = index;
        currentGun.theGunInfo = myGunInfo;
        //Debug.Log("before active");
        theGuns[index].gameObject.SetActive(true);
    }
    int mod(int x, int m)
    {
        int r = x % m;
        return r < 0 ? r + m : r;
    }
    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.GameIsPaused)
        {
            // check if the space is pressed down
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpKeyWasPressed = true;
            }
            // ad movements
            horizontalInput = Input.GetAxis("Horizontal");
            //ws movements
            verticalInput = Input.GetAxis("Vertical");

            // ray point from camera to ground plane
            Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;

            if (groundPlane.Raycast(cameraRay, out rayLength))
            {
                Vector3 pointToLook = cameraRay.GetPoint(rayLength);
                //Debug.DrawLine(cameraRay.origin, pointToLook, Color.red);

                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }

            //handle gun shooting and reloading events

            if (Input.GetMouseButtonDown(0))
            {
                currentGun.isFiring = true;

            }
            if (Input.GetMouseButtonUp(0))
            {
                currentGun.isFiring = false;
            }

            if (Input.GetKey(KeyCode.R))
            {
                currentGun.Reload();
            }

            //shift gun
            // scroll forward
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                updateGunInfo(currentGunIndex, 1);
            }
            // scroll backward
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                updateGunInfo(currentGunIndex, -1);
            }
        }

    }

    // Fixedupdate is called once every physic update
    private void FixedUpdate()
    {
        // update moving speed with current jump height
        rigibodyComponent.velocity = new Vector3(horizontalInput * moveSpeed, rigibodyComponent.velocity.y, verticalInput * moveSpeed);


        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
        {
            return;
        }
        if (jumpKeyWasPressed)
        {
            float jumpPower = 5f;
            if (superJumpsRemaining > 0)
            {
                jumpPower *= 2;
                superJumpsRemaining--;
            }
            rigibodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            Destroy(other.gameObject);
            superJumpsRemaining++;
        }
    }

}
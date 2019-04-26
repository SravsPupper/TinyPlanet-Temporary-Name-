﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float gravity;
    Vector3 gravityVector;
    public Transform Planet;
    public float speed;
    public float rotateSpeed;
    private Vector3 velocity = Vector3.zero;
    private bool isGrounded;
    private bool insidePlanet=false;
    public float jumpValue;
    public float gravityRotation;
    Vector3 jumpDirection;
    Vector3 CameramoveDirection;
    public float camerashakeDuration;
    public float camerashakeMagnitude;
    bool isCameraShaking;
    bool canJump;

    float timer;
    public float maxJumpTimer;

    public Camera Camera;

    public bool canMove;

    public string PlanetTag;
    private void Start()
    {
        
    }
    private void Update()
    {

        if (canMove == true)
        {

            CameramoveDirection = (Camera.transform.forward * (-Input.GetAxis("Joystick Vertical")) + Camera.transform.right * (Input.GetAxis("Joystick Horizontal") * 0.5f));

            gravityVector = (Planet.position - transform.position).normalized * gravity * Time.deltaTime;
            if (insidePlanet)
            {
                StayOnPlanet();
            }
            Vector3 RightVector = Vector3.Cross(CameramoveDirection, gravityVector);
            Vector3 PlayerForwardVector = Vector3.Cross(gravityVector, RightVector).normalized;
            transform.position += PlayerForwardVector * speed * Time.fixedDeltaTime;


            if (Input.GetButtonDown("Joystick Jump") && isGrounded)
            {
                timer = 0f;
                isGrounded = false;
                jumpDirection = (transform.position - Planet.position).normalized;
                velocity += jumpDirection * jumpValue * 2f;
                canJump = true;
            }
            else if (Input.GetButton("Joystick Jump") && canJump && timer < maxJumpTimer)
            {
                timer += Time.deltaTime;
                velocity += jumpDirection * jumpValue;
            }
            else
            {
                canJump = false;
            }
            velocity += gravityVector;


            Quaternion rot;
            if (CameramoveDirection.sqrMagnitude > 0.01f)
            {
                rot = Quaternion.LookRotation(PlayerForwardVector, -gravityVector);
            }
            else
            {
                rot = Quaternion.LookRotation(transform.forward, -gravityVector);
            }

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, gravityRotation * Time.deltaTime);
            transform.position += velocity * Time.deltaTime;
        }



    }
  

    private void LateUpdate()
    {
        if (isCameraShaking)
        {
           Camera.transform.position += Random.insideUnitSphere * camerashakeMagnitude;
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Planet")
        {
            insidePlanet = true;
            isCameraShaking = true;
            Invoke("CameraShake", camerashakeDuration);
        }
        if (other.tag == PlanetTag || other.tag =="FauxGravity")
        {
            Planet = other.transform.parent;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Planet")
        {
            insidePlanet = true;        }
    }


    void CameraShake()
    {
        isCameraShaking = false;
    }

    void StayOnPlanet()
    {
        isGrounded = true;
        velocity = Vector3.zero;
        transform.position = Planet.position - gravityVector.normalized * Planet.transform.localScale.y * 0.5f;
        insidePlanet = false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 15;
    private Vector3 _inputDirection;
    
    public Transform cameraTransform;
    private float _mouseX;
    private float _mouseY;

    private float _xRotation;
    private float _yRotation;
    public float mouseSensitivity = 1;

    [SerializeField] private float JumpForceMultiplier;

    private Rigidbody _rigidbody;
    
    void Start() {
    _rigidbody = gameObject.GetComponent<Rigidbody>();

    }

    void Update() {
        _inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        _mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        _mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        _yRotation -= _mouseY;
        _yRotation = Mathf.Clamp(_yRotation, -90f, 90f);
        
        // cameraTransform.Rotate(-_mouseY, 0, 0); // WOULD USE THIS BUT CAN'T USE CLAMP
        
        // moves player model left and right
        transform.Rotate(0, _mouseX, 0);
        // moves camera up and down
        cameraTransform.localRotation = Quaternion.Euler(_yRotation, 0, 0);
        _rigidbody.AddForce( transform.up* JumpForceMultiplier * Convert.ToInt32(Input.GetKeyDown(KeyCode.Space)));
    }

    void FixedUpdate() {
        _rigidbody.AddForce(transform.TransformDirection(_inputDirection * moveSpeed));
    }
}

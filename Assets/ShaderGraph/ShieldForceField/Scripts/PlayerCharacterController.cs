using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour {

    [SerializeField] private float mouseSensitivity = 1f;

    private CharacterController characterController;
    private float cameraVerticalAngle;
    private float characterVelocityY;
    private Vector3 characterVelocityInteria;
    private Camera playerCamera;


    private void Awake() {
        characterController = GetComponent<CharacterController>();
        playerCamera = transform.Find("Camera").GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {
        HandleCharacterLook();
        HandleCharacterMovement();
    }

    private void HandleCharacterLook() {
        float lookX = Input.GetAxisRaw("Mouse X");
        float lookY = Input.GetAxisRaw("Mouse Y");

        // Rotate the transform with the input speed around its local Y axis
        transform.Rotate(new Vector3(0f, lookX * mouseSensitivity, 0f), Space.Self);
        
        // Add vertical inputs to the camera's vertical angle
        cameraVerticalAngle -= lookY * mouseSensitivity;

        // Limit the camera's vertical angle to min/max
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -89f, 89f);

        // Apply the vertical angle as a local rotation to the camera transform along its right axis (makes it pivot up and down)
        playerCamera.transform.localEulerAngles = new Vector3(cameraVerticalAngle, 0, 0);
    }

    private void HandleCharacterMovement() {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        float moveSpeed = 20f;

        Vector3 characterVelocity = transform.right * moveX * moveSpeed + transform.forward * moveZ * moveSpeed;
        
        /*
        if (moveX != 0) characterVelocityInteria += transform.right * moveX * moveSpeed;
        if (moveZ != 0) characterVelocityInteria += transform.forward * moveZ * moveSpeed;
        characterVelocityInteria = Vector3.ClampMagnitude(characterVelocityInteria, moveSpeed * 2f);
        //*/


        // Jump
        if (characterController.isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            float jumpSpeed = 35f;
            characterVelocityY = jumpSpeed;
        }

        // Apply gravity to the velocity
        float gravityDownForce = -80f;
        characterVelocityY += gravityDownForce * Time.deltaTime;

        // Apply Y velocity to move vector
        characterVelocity.y = characterVelocityY;
        
        /*
        // Apply inertia
        characterVelocity += characterVelocityInteria;
        //*/

        // Move Character Controller
        characterController.Move(characterVelocity * Time.deltaTime);

        
        /*
        // Dampen inertia
        if (characterVelocityInteria.magnitude > 0f) {
            characterVelocityInteria -= characterVelocityInteria * 10f * Time.deltaTime;
            if (characterVelocityInteria.magnitude < .05f) {
                characterVelocityInteria = Vector3.zero;
            }
        }
        //*/
    }

}

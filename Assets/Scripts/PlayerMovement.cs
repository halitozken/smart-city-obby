using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float jumpForce = 5f;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;

    [SerializeField] Transform cam;
    [SerializeField] float lookSensitivity;
    [SerializeField] float maxXRot;
    [SerializeField] float minXRot;
    private float curXRot;

    [SerializeField] Animator animator;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {

        if (Cursor.lockState == CursorLockMode.Locked)
            Look();

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }



    void Move()
    {
        Vector3 cameraDir = Camera.main.transform.forward;
      
        cameraDir.y = 0;
        cameraDir.Normalize();

       
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        
        Vector3 move = new Vector3(horizontal, 0.0f, vertical).normalized;

        Vector3 target = transform.position + cameraDir * move.z * movementSpeed * Time.deltaTime +
                             Camera.main.transform.right * move.x * movementSpeed * Time.deltaTime;

        rb.MovePosition(target);

        if(move != Vector3.zero)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isRunning", true);
        }else
        {
            animator.SetBool("isRunning", false);
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        animator.SetBool("isJumping", true);
    }

    void Look()
    {
        float x = Input.GetAxis("Mouse X") * lookSensitivity;
        float y = Input.GetAxis("Mouse Y") * lookSensitivity;

        transform.eulerAngles += Vector3.up * x;

        curXRot += y;
        curXRot = Mathf.Clamp(curXRot, minXRot, maxXRot);

        cam.localEulerAngles = new Vector3(-curXRot, 0.0f, 0.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy Head"))
        {
            Destroy(collision.transform.parent.gameObject);
            Jump();
        }
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, .1f, ground);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float movementSpeed = 7f;
    [SerializeField] float climbSpeed = 7f;
    [SerializeField] float jumpForce = 5f;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;

    [SerializeField] Transform cam;
    [SerializeField] float lookSensitivity;
    [SerializeField] float maxXRot;
    [SerializeField] float minXRot;
    private float curXRot;

    [SerializeField] Animator animator;

    private bool isClimbing;
    private bool isMove;

    private void Start()
    {
        PlayerPrefs.DeleteAll();
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        isMove = true;
        isClimbing = false;
    }

    private void Update()
    {

        if (Cursor.lockState == CursorLockMode.Locked)
            Look();

        
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Jump(); 
            
        }else
        {
            animator.SetBool("isJumping", false);
        }

    }

    private void FixedUpdate()
    {
        if (isMove)
        {
            Move();
        }

        if (isClimbing)
        {
            if (Input.GetKey(KeyCode.W))
            {
                Climb();
            }

        }
           
    }



    void Move()
    {
        
        Vector3 cameraDir = Camera.main.transform.forward;

        cameraDir.y = 0;
        cameraDir.Normalize();

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        

        Vector3 move = new Vector3(horizontal, 0.0f, vertical).normalized;
        Vector3 target = transform.position + cameraDir * move.z * movementSpeed * Time.deltaTime + Camera.main.transform.right * move.x * movementSpeed * Time.deltaTime;
        rb.MovePosition(target);
       

        if (move != Vector3.zero)
        {

            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
       
       
    }

    void Climb()
    {
        
        isMove = false;
        rb.useGravity = false;
        transform.position += Vector3.up * climbSpeed * Time.deltaTime;
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
        if (collision.gameObject.CompareTag("Enemy Head"))
        {
            Destroy(collision.transform.parent.gameObject);
            Jump();
        }

        if (collision.gameObject.CompareTag("Climb"))
        {
            isClimbing = true;
            isMove = false;
            Debug.Log("Duvar");
        }


    }

    private void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.CompareTag("Climb"))
        {
            isClimbing = false;
            isMove = true;
            rb.useGravity = true;
       
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Climb"))
        {
            isClimbing = false;
            isMove = true;
            rb.useGravity = true;
        }
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, .1f, ground);
        
    }


}

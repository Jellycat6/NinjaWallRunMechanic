using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] CharacterController _controller;
    [SerializeField] Transform cam;

    [SerializeField] float _speed = 6f;
    [SerializeField] float _turnSmoothTime = 0.1f;
    [SerializeField] float _turnSmoothVelocity;
    [SerializeField] float _gravity = -9.8f;
    [SerializeField] float _jumpHeight = 3f;
    [SerializeField] Transform _groundCheck;
    [SerializeField] float _groundDistance = 0.4f;
    [SerializeField] LayerMask _groundMask;
    [SerializeField] bool _isGrounded;
    Vector3 _velocity;
    private Vector3 direction;

    //For Walking on Walls
    private float horizontal;
    private float vertical;
    RaycastHit hit;
    RaycastHit hit2;
    [SerializeField] GameObject raycastSpawner;
    [SerializeField] GameObject raycastSpawner2;
    Vector3 _currentPosition;
    bool notTouchingWall;


    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Cimable")
        {
            Debug.Log("Collided");
            GetOnWall();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Cimable")
        {
            Debug.Log("Collided");
            OffWall();
        }
    }*/

    private void Start()
    {
            Cursor.lockState = CursorLockMode.Locked;
        notTouchingWall = false;
    }
    void Update()
    {
        _currentPosition = transform.position;
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if (vertical > 0f)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            _controller.Move(transform.forward * 6f * Time.deltaTime);
            
        }
        else if (vertical < 0f)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            _controller.Move(transform.forward * 6f * Time.deltaTime);
            
        }
        if (horizontal > 0f)
        {
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            _controller.Move(transform.forward * 6f * Time.deltaTime);
            
        }
        else if (horizontal < 0f)
        {
            transform.rotation = Quaternion.Euler(0f, -90f, 0f);
            _controller.Move(transform.forward * 6f * Time.deltaTime);
            
        }
        // direction = new Vector3(horizontal, 0f, vertical).normalized;

        Debug.DrawRay(raycastSpawner.transform.position, raycastSpawner.transform.forward, Color.cyan);
        Debug.DrawRay(raycastSpawner.transform.position, -raycastSpawner2.transform.up, Color.cyan);
        //Change Orientation When touching wall;
        if (Physics.Raycast(raycastSpawner.transform.position, raycastSpawner.transform.forward,out hit, 1f))
        {
            notTouchingWall = true;
            
            Debug.Log("Touched");
            if (hit.transform.tag == "Cimable" && Physics.Raycast(raycastSpawner.transform.position, -raycastSpawner2.transform.up, out hit2, 10f))
            {
                Debug.Log("Attatched");
                if (notTouchingWall)
                {
                    transform.rotation = Quaternion.Euler(-90f,0f,0f);
                }
                notTouchingWall = false;

            }
            else
            {
                //Debug.Log("Off");
                //transform.SetPositionAndRotation(_currentPosition, Quaternion.Euler(0f, 0f, 0f));
            }
        }
        

        /*if (horizontal >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(transform.position.x, transform.position.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _controller.Move(moveDir.normalized * _speed * Time.deltaTime);
        }*/
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            transform.SetPositionAndRotation(_currentPosition, Quaternion.Euler(0f, 0f, 0f));
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            Debug.Log("Jump");
        }

        _velocity.y += _gravity * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);

    }
}

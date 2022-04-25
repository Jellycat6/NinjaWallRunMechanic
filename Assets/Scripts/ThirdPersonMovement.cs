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
    [SerializeField] GameObject _graphics;
    private Vector3 _currentPosition;
    private float horizontal;
    private float vertical;
    Vector3 _counterForce;
    RaycastHit hit;


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
    }
    void Update()
    {
        _currentPosition = _graphics.transform.position;
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        

        //Change Orientation When touching wall;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward),out hit, 2f))
        {
            if (hit.transform.tag == "Cimable")
            {
                transform.Rotate(-90, 0, 0);
                direction = new Vector3(horizontal, vertical, 0f).normalized;
            }
        }
        else
        direction = new Vector3(horizontal, 0f, vertical).normalized;



        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _controller.Move(moveDir.normalized * _speed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            OffWall();
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            Debug.Log("Jump");
        }

        _velocity.y += _gravity * Time.deltaTime;
        _counterForce.y = -_velocity.y;

        _controller.Move(_velocity * Time.deltaTime);

    }

    private void GetOnWall()
    {
        _graphics.transform.Rotate(-90, 0, 0);
        
        if (Physics.Raycast(_graphics.transform.position, _graphics.transform.TransformDirection(Vector3.down), 2f))
        {
            _graphics.transform.Rotate(90, 0, 0);
        }
    }

    private void OffWall()
    {
        _graphics.transform.SetPositionAndRotation(_currentPosition, Quaternion.Euler(0f,0f,0f));
    }
}

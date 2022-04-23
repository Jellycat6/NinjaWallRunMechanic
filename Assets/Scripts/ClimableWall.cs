using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimableWall : MonoBehaviour
{
    [SerializeField] GameObject _graphics;
    private Vector3 _currentPosition;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Cimable")
        {
            Debug.Log("Collided");
            GetOnWall();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _currentPosition = _graphics.transform.position;
    }

    private void GetOnWall()
    {
        _graphics.transform.Rotate(-90, 0, 0);
        if (Physics.Raycast(_graphics.transform.position, _graphics.transform.TransformDirection(Vector3.down), 1f))
        {
            _graphics.transform.Rotate(90, 0, 0);
        }
    }
}

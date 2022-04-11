using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToWall : MonoBehaviour
{
    [SerializeField] GameObject _stayAttatchedToWall;
    [SerializeField] GameObject _player;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
        if (other.tag == "Climable")
        {
            Debug.Log("Climb");

            AttatchToWall();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void AttatchToWall()
    {
        _player.transform.Rotate(-90, 0, 0);
        _stayAttatchedToWall.SetActive(true);
    }
}

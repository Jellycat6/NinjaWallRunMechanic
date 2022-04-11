using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayAttatched : MonoBehaviour
{
    [SerializeField] GameObject _player;
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Climable")
            ResetPlayer();
    }
    
    private void ResetPlayer()
    {
        _player.transform.Rotate(90, 0, 0);
        gameObject.SetActive(false);
    }
}

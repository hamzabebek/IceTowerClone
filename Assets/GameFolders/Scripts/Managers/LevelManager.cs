using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Bounce _player;
    [SerializeField] CameraFollow _camera;

    [SerializeField] private int levelUpdateInterval = 10;


    private void Update()
    {
        LevelUpdate();
    }

    void LevelUpdate()
    {
        float currentScore = _player.value;
        if (currentScore>=levelUpdateInterval)
        {
           // Debug.Log("Level Updated");
           Debug.Log(_player.jumpForce);
            _player.jumpForce +=2f;
            levelUpdateInterval +=10;
            _player.value = 0;

        }

    }
}

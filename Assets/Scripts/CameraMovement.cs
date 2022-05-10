using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    public bool isMoved => !isStop;
    private bool isStop = true;
    public void ChangeState()
    {
        isStop = isStop ? isStop = false : isStop = true;
    }

    public void Start()
    {
        Application.targetFrameRate = 60;
        
    }

    private void Update()
    {
        if (!isStop)
        {
            transform.position += Vector3.forward * Time.smoothDeltaTime * _speed;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    
    public static CameraMovement Instance;
    AudioListener audioListener;
    [SerializeField] private float _speed;
    private bool SoundEnabled;
    public bool isMoved => !isStop;
    private bool isStop = true;
    public void ChangeState()
    {
        isStop = isStop ? isStop = false : isStop = true;
    }
    public void SetState(bool _bool)
    {
        isStop = _bool;
    }
    private void Awake()
    {
        Instance = this;
    }
    public void Start()
    {
        SoundEnabled = true;
        audioListener = GetComponent<AudioListener>();
        Application.targetFrameRate = 60;
        
    }
    public void ToogleSound()
    {
        SoundEnabled = !SoundEnabled;
        if (SoundEnabled)
        {
            audioListener.enabled = true;


        }
        else
        {
            audioListener.enabled = false;
        }
    }

    private void Update()
    {
        if (!isStop)
        {
            transform.position += Vector3.forward * Time.smoothDeltaTime * _speed;
        }
    }
}

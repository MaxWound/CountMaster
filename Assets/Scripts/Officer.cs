using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Officer : MonoBehaviour
{
    Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Run(bool state)
    {
        _animator.SetBool("Run", state);
        _animator.SetBool("Death", false);
        _animator.SetBool("Idle",false);
    }
    public void Death(bool state)
    {
        _animator.SetBool("Run", false);
        _animator.SetBool("Death", state);
        _animator.SetBool("Idle", false);
    }
    public void Idle(bool state)
    {
        _animator.SetBool("Run", false);
        _animator.SetBool("Death", false);
        _animator.SetBool("Idle", state);
    }

}

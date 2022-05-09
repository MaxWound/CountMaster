using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : MonoBehaviour
{
    private Animator _animator;
    public SpawnPoint SpawnPoint { get; set; }
    [SerializeField] private Weapon _weapon;
    public Weapon Weapon => _weapon;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Attack(bool state)
    {
        _animator.SetBool("Attack", state);
    }

    public void Run(bool state)
    {
        _animator.SetBool("Run", state);
    }

    public void Dancing()
    {
        _animator.SetBool("Victory", true);
    }
}

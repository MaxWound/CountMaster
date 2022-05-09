using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private Weapon _weapon;
    public int currentHealth;
    
    public Weapon Weapon => _weapon;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        currentHealth = health;
    }
    private void OnTriggerEnter(Collider other)
    {
        AlliesGroup.Instance.Battle(this);

    }
    public void Attack()
    {
        _animator.SetBool("Attack", true);
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
}

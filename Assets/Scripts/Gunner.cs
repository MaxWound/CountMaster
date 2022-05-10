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
    private bool isFighting = false;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        currentHealth = health;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ally ally) && isFighting != true)
        {
            isFighting = true;
            AlliesGroup.Instance.Battle(this);
        }

    }
    public void Attack()
    {
        _animator.SetBool("Attack", true);
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
    public void SelfDestroy()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner : MonoBehaviour
{
    [SerializeField] GameObject vfxGo;
    private ParticleSystem gunnerParticles;
    [SerializeField] private int health;
    [SerializeField] private Weapon _weapon;
    public int currentHealth;
    
    public Weapon Weapon => _weapon;
    private Animator _animator;
    private bool isFighting = false;
    private void Awake()
    {
        gunnerParticles = vfxGo.GetComponent<ParticleSystem>();
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
    public void Idle()
    {
        _animator.SetBool("Attack", false);
    }
    public void Attack()
    {
        _animator.SetBool("Attack", true);
    }
    public void Death()
    {
        _animator.SetBool("Attack", false);
        _animator.SetBool("Death", true);
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
    public void SelfDestroy()
    {
        vfxGo.transform.parent = null;
        Destroy(vfxGo, 1f);

        gunnerParticles.Play();
        Death();
        Destroy(gameObject,1f);
        
    }
}

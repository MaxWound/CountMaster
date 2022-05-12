using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : MonoBehaviour
{
    private Animator _animator;
    public SpawnPoint SpawnPoint { get; set; }
    [SerializeField] private Weapon _weapon;
    [SerializeField] GameObject vfxGo;
    private ParticleSystem allyParticles;
    public Weapon Weapon => _weapon;

    private void Start()
    {
        
        _animator = GetComponent<Animator>();
        allyParticles = vfxGo.GetComponent<ParticleSystem>();
    }
    public void SelfDestroy()
    {
        allyParticles.Play();
        vfxGo.transform.parent = null;
        Destroy(vfxGo, 1f);
        Destroy(gameObject);
    }
    public void SelfDestroyWithDelay(float t)
    {
        allyParticles.Play();
        vfxGo.transform.parent = null;
        Destroy(vfxGo, 1f);
       
        Destroy(gameObject, t);
    }
    public void Die(bool state)
    {
        _animator.SetBool("Die", true);
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

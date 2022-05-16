using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    AudioSource punchSoundSource;
    [SerializeField]
    AudioSource deathSoundSource;
    [SerializeField] private float _helth;
    private float _currentHelth;
    public float Helth => _currentHelth;
    [SerializeField] private HelthBar _helthBar;
    private Animator _animator;

    [SerializeField] private GameObject _helicopter;

    private BoxCollider _boxCollider;

    private void Start()
    {
        deathSoundSource = GetComponent<AudioSource>();
        _currentHelth = _helth;
        _animator = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ally ally))
        {
            AlliesGroup.Instance.Battle(this);
        }
    }
    public void PlayPunchSound()
    {
        punchSoundSource.Play();
    }
    public void TakeDamage(float value)
    {
        _animator.SetBool("Attack", true);
        

        _currentHelth -= value;
       
        float helthToPercent = _currentHelth / _helth;
        _helthBar.UpdateValue(helthToPercent);

        if (_currentHelth <= 0)
        {
            Death();
            DestroyCollider();
        }
    }

    public void Death()
    {
        deathSoundSource.Play();
        transform.position = new Vector3(transform.position.x, 0.75f, transform.position.z);
        _animator.SetBool("Death", true);
        
    }


    #region Animation Events

    public void KillAllies()
    {
        PlayPunchSound();
        AlliesGroup.Instance.KillAllies(this);
    }

    public void DestroyCollider()
    {
        Destroy(_boxCollider);
    }

    public void Idle()
    {
        _animator.SetBool("Attack", false);
    }
    #endregion
}

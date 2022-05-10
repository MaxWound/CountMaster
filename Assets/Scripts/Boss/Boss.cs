using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private float _helth;
    private float _currentHelth;
    public float Helth => _currentHelth;
    [SerializeField] private HelthBar _helthBar;
    private Animator _animator;

    [SerializeField] private GameObject _helicopter;

    private BoxCollider _boxCollider;

    private void Start()
    {
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
        transform.position = new Vector3(transform.position.x, 0.75f, transform.position.z);
        _animator.SetBool("Death", true);
        _helicopter.GetComponent<Animator>().Play("Root");
    }


    #region Animation Events

    public void KillAllies()
    {
        
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

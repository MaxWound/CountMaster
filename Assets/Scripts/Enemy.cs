using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    public Weapon Weapon => _weapon;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        _animator.SetBool("Attack", true);
    }
}

using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject vfxGo;
    private ParticleSystem enemyParticles;
    [SerializeField] private Weapon _weapon;
    public Weapon Weapon => _weapon;
    private Animator _animator;
    public SpawnPoint SpawnPoint { get; set; }
    private void Start()
    {
        enemyParticles = vfxGo.GetComponent<ParticleSystem>();
        _animator = GetComponent<Animator>();
    }

    public void SelfDestroy()
    {
        Death();
        enemyParticles.Play();
        vfxGo.transform.parent = null;
       
        Destroy(vfxGo, 1f);
        
        Destroy(gameObject, 1f);

            
    }
    public void Death()
    {
        Weapon.ExitFire();
        _animator.SetBool("Attack", false);
        
        _animator.SetBool("Death",true);
    }
    public void Attack()
    {
        _animator.SetBool("Attack", true);
    }
}

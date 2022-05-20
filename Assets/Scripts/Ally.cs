using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : MonoBehaviour
{

    private Animator _animator;
    public bool IsAlive = true;
    public SpawnPoint SpawnPoint { get; set; }
    [SerializeField] private Weapon _weapon;
    [SerializeField] GameObject vfxGo;
    [SerializeField] AudioClip deathSound;
    [SerializeField]
    MeshRenderer meshRenderer;
    [SerializeField]
    AudioSource deathAudioSource;
    AudioSource audioSource;
    [Space]
    [SerializeField]
    MeshRenderer rifleRenderer;
    [SerializeField]
    MeshRenderer headRenderer;
    [SerializeField]
    SkinnedMeshRenderer bodyRenderer;
    Collider capsuleCollider;
    Rigidbody rb;
    private ParticleSystem allyParticles;
    public Weapon Weapon => _weapon;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        allyParticles = vfxGo.GetComponent<ParticleSystem>();
        meshRenderer = GetComponent<MeshRenderer>();
    }
    public void SetRendererBool(bool _bool)
    {
        rifleRenderer.enabled = _bool;
        headRenderer.enabled = _bool;
        bodyRenderer.enabled = _bool;
    }
    public void SetRendererBoolWithDelay(bool _bool, float t)
    {
        StartCoroutine(StartSetRendererBoolWithDelay(_bool, t));
    }

    public IEnumerator StartSetRendererBoolWithDelay(bool _bool, float t)
    {
        yield return new WaitForSeconds(t);
        rifleRenderer.enabled = _bool;
        headRenderer.enabled = _bool;
        bodyRenderer.enabled = _bool;
    }

    public void FireSound()
    {
        audioSource.Play();
    }

    public void AddForceBack()
    {
        rb.constraints = RigidbodyConstraints.None;
        rb.useGravity = true;
        gameObject.transform.parent = null;

        rb.AddForce(Vector3.back * 20f + Vector3.up * 0.1f, ForceMode.Impulse);
        rb.AddTorque(new Vector3(Random.Range(0,5), Random.Range(0, 5), Random.Range(0, 5)));

    }
    
    public void SelfDestroy()
    {
        IsAlive = false;
        allyParticles.Play();
        //vfxGo.transform.parent = null;
        //Destroy(vfxGo, 1f);
        SetRendererBool(false);
        GoToPoolWithDelay(0.5f);
    }
    public void SelfOffWithDelay(float t)
    {
        IsAlive = false;
        allyParticles.Play();
       
        SetRendererBoolWithDelay(false,t);
        GoToPoolWithDelay(t);


    }
    public void GoToPoolWithDelay(float t)
    {
        StartCoroutine(StartGoToPoolWithDelay(t));
    }
    public IEnumerator StartGoToPoolWithDelay(float t)
    {
        yield return new WaitForSeconds(t);
        GoToPool();

    }
    public void GoToPool()
    {
        
        transform.position = PoolManager.Instance.transform.position;
        transform.parent = PoolManager.Instance.transform;
        PoolManager.Instance._pool.Add(this);
        rifleRenderer.enabled = true;
        headRenderer.enabled = true;
        bodyRenderer.enabled = true;
        capsuleCollider.enabled = true;
        IsAlive = true;
        gameObject.SetActive(false);

    }
    public void SelfOff()
    {
        deathAudioSource.Play();
        allyParticles.Play();
        capsuleCollider.enabled = false;
        SetRendererBool(false);
        GoToPoolWithDelay(0.5f);
    }    
    public void OSelfDestroyWithDelay(float t)
    {
        allyParticles.Play();
        //vfxGo.transform.parent = null;
        //Destroy(vfxGo, 1f);
       
        Destroy(gameObject, t);
    }
    public void PlayDeathWithDetach()
    {
        deathAudioSource.Play();
        //deathAudioSource.gameObject.transform.parent = null;
       // Destroy(deathAudioSource.gameObject, 1f);
    }
    public void Salute()
    {
        _animator.SetBool("Salute", true);
        
    }
    public void Die(bool state)
    {
        _animator.SetBool("Die", true);
        deathAudioSource.Play();
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

using UnityEngine;
using System.Collections.Generic;

public class Finish : MonoBehaviour
{
    public static Finish Instance;
    private bool finished = false;
    [SerializeField] private List<ParticleSystem> _particleSystem;
    private BoxCollider _boxCollider;
    private void Awake()
    {
        Instance = this;
        _boxCollider = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ally ally) && finished == false)
        {
            AlliesGroup.Instance.StopSteps();
            finished = true;
            Destroy(_boxCollider);
            AlliesGroup.Instance.AlliesFormation();
            MovementController.Instance.SetControllerState(false);

            AlliesGroup.Instance._officer.Idle(true);
            AlliesGroup.Instance.Dancing();

            
        }
    }
    public void PlayParticles()
    {
    _particleSystem.ForEach(particle => particle.Play());
    }


}

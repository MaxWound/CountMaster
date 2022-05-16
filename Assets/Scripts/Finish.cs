using UnityEngine;
using System.Collections.Generic;

public class Finish : MonoBehaviour
{
    private bool finished = false;
    [SerializeField] private List<ParticleSystem> _particleSystem;
    private BoxCollider _boxCollider;
    private void Awake()
    {
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
            
            _particleSystem.ForEach(particle => particle.Play());
        }
    }


}

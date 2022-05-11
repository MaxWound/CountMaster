using UnityEngine;
using System.Collections.Generic;

public class Finish : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> _particleSystem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ally ally))
        {
            MovementController.Instance.ChangeControllerState();
            UIManager.Instance.ShowCondition(Condition.Victory);
            AlliesGroup.Instance._officer.Idle(true);
            AlliesGroup.Instance.Dancing();
            _particleSystem.ForEach(particle => particle.Play());
        }
    }


}

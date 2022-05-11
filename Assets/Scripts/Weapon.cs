using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> _muzzleFlash;

    public void Fire()
    {
        
        _muzzleFlash.ForEach(vfx => vfx.Play());
        print("Fire");
    }
}

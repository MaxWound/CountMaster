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

    public void ExitFire()
    {
        StartCoroutine(ExitFireWithSeconds(0.2f));
    }
    public IEnumerator ExitFireWithSeconds(float t)
    {
        yield return new WaitForSeconds(t);
            _muzzleFlash.ForEach(vfx => vfx.Stop(true,ParticleSystemStopBehavior.StopEmittingAndClear));
    }
}

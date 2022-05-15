using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField]
    GameObject vfxGo;
    ParticleSystem mineParticles;
    AudioSource mineAudio;
    private bool ToExplode;
    private void Awake()
    {
        mineParticles = vfxGo.GetComponent<ParticleSystem>();
        mineAudio = vfxGo.GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Ally>() != null)
        {
            ExplodeWithSeconds(0.2f);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Ally>() != null)
        {
            if (ToExplode == true)
            {
                Explode();
                print("KillByMine");
                Ally ally = other.GetComponent<Ally>();
                AlliesGroup.Instance.Kill(ally, true);

            }
        }
    }
    private void Explode()
    {
        mineParticles.Play();
        mineAudio.Play();
        vfxGo.transform.parent = null;
        Destroy(vfxGo, 1f);
        Destroy(gameObject);
    }
    public void SetToExplodeTrue()
    {
        ToExplode = true;
    }
    public void ExplodeWithSeconds(float t)
    {
        StartCoroutine(StartExplodeWithSeconds(t));

    }
    public IEnumerator StartExplodeWithSeconds(float t)
    {
        yield return new WaitForSeconds(t);
        SetToExplodeTrue();
       
    }
}

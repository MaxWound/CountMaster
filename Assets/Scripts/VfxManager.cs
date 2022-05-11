using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxManager : MonoBehaviour
{
   public static VfxManager Instance;
    
    public GameObject vfxGO;
    public ParticleSystem dieVfx;
    public ParticleSystem enemyDieVfx;
    private void Awake()
    {
        Instance = this;   
    }
    
}

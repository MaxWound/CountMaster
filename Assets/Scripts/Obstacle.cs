using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Ally ally = other.gameObject.GetComponent<Ally>();
        if (ally != null && AlliesGroup.Instance.Count != 0)
        {
            
            AlliesGroup.Instance.Kill(ally);
            
           
        }
    }
}

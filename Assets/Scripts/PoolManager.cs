using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
   
    public static PoolManager Instance;
    
    int StartCount;
    [SerializeField]
    Ally prefab;
    [SerializeField]
    List<Ally> _pool;
    Transform GroupTransform;





    

    private void Awake()
    {
        StartCount = 700;
        if(Instance == null)
        Instance = this;
        
    }
    private void Start()
    {
        prefab = AlliesGroup.Instance._original;
        InitPool();
        prefab = AlliesGroup.Instance.original;
        GroupTransform = AlliesGroup.Instance.transform;
    }

    private void InitPool()
    {
        for (int i = 0; i < StartCount; i++)
        {
          Ally newObj = Object.Instantiate(prefab, transform);
            
            _pool.Add(newObj);
            newObj.gameObject.SetActive(false);
        }
    }
    public Ally SpawnFromPool(Transform parent)
    {
        _pool[0].transform.parent = parent;
        _pool[0].gameObject.SetActive(true);
        Ally allyToReturn = _pool[0];
        _pool.Remove(allyToReturn);
        return allyToReturn;
    }
    public void ReturnToPool(Ally allyToReturn)
    {
        allyToReturn.gameObject.SetActive(false);
        _pool.Add(allyToReturn);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

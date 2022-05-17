using UnityEngine;
using System.Collections.Generic;
using TMPro;

[RequireComponent(typeof(Collider))]
public class WallPart : MonoBehaviour
{
    [SerializeField] private WallSide _side;
    public WallSide Side => _side;
    [SerializeField] private TextMeshPro _coefficientText;
    [SerializeField] private List<ParticleSystem> _particleSystem;
    private Collider _collider;
    public Collider Collider => _collider;
    public Wall Wall { get; set; }
    public int SpawnCoefficient { get; set; }
    [SerializeField] private WallOperation _operation;

    public delegate void AlliesChanged();
    public event AlliesChanged OnAlliesCountChanged;
    Ally _allyOriginal;
    private void Start()
    {
        _allyOriginal = AlliesGroup.Instance.original;
        OnAlliesCountChanged += AlliesGroup.Instance.UpdateRingsValue;

        _collider = GetComponent<Collider>();
        _coefficientText.text = $"{GetOperationString()}{SpawnCoefficient}";
        _particleSystem.ForEach(system => system.gameObject.transform.parent = null);
    }
    public WallOperation GetOperation()
    {
        return _operation;
    }
    public string GetOperationString()
    {
        if (_operation == WallOperation.Multiply)
        {
            return "X";
        }
        if (_operation == WallOperation.Sum)
        {
            return "+";
        }
        if (_operation == WallOperation.Substruction)
        {
            return "-";
        }

        return null;
    }

    private void OnTriggerEnter(Collider other)
    {

        Wall.Triggered();
        AlliesGroup.Instance.UpdateRingsValue();
        
        SpawnAllies(other.GetComponent<Ally>(), _operation);
        _particleSystem.ForEach(vfx => vfx.Play());

        OnAlliesCountChanged?.Invoke();

        Destroy(gameObject);
       
    }

    private void SpawnAllies(Ally _allyOriginal, WallOperation operation)
    {
        
        
        SpawnPoint spawnPoint;
        if (operation == WallOperation.Sum)
        {
            for (int i = 0; i < SpawnCoefficient; i++)
            {
                if (AlliesGroup.Instance.TryGetAllySpawnPosition(out spawnPoint))
                {
                    
                  Ally ally = spawnPoint.Spawn(_allyOriginal, _allyOriginal.transform.parent);
                    
                    ally.SpawnPoint = spawnPoint;
                   
                    AlliesGroup.Instance.Add(ally);
                   
                }
            }
            SoundsController.Instance.Play(Sound.CheckPoint);
        }

        if (operation == WallOperation.Multiply)
        {
            int multiplyValue = AlliesGroup.Instance.Count * (SpawnCoefficient - 1);
            for (int i = 0; i < multiplyValue; i++)
            {
                if (AlliesGroup.Instance.TryGetAllySpawnPosition(out spawnPoint))
                {
                    Ally ally = spawnPoint.Spawn(_allyOriginal, _allyOriginal.transform.parent);
                    
                    ally.SpawnPoint = spawnPoint;
                    AlliesGroup.Instance.Add(ally);
                    
                }
            }
            SoundsController.Instance.Play(Sound.CheckPoint);
        }

        if (operation == WallOperation.Substruction)
        {
            AlliesGroup.Instance.Substruct(SpawnCoefficient);
        }
       
    }
}

public enum WallSide
{
    Right,
    Left
}

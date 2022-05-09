using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGroup : MonoBehaviour
{
    private const int _enemiesPerRing = 6;
    [SerializeField] private int _enemiesCount;
    public void SetEnemiesCount(int value)
    {
        _enemiesCount = value;
    }

    [SerializeField] private Enemy _original;
    [SerializeField] private TextMeshPro _text;
    [SerializeField] private ParticleSystem _auraVFX;

    private bool DetectCollisions = true;
    private List<Enemy> _enemy = new List<Enemy>();
    public List<Enemy> GetEnemies() => _enemy;
    public void Remove(Enemy enemyToRemove)
    {
        _enemy.Remove(enemyToRemove);
    }

    void Start()
    {

        int Ring = GetRing();
        _enemy.Add(_original);
        List<SpawnPoint> spawnPoints = GetSpawnPoints(Ring);

        SpawnEnemies(spawnPoints);

        Sort(Ring,spawnPoints);
        
    }

    private void Update()
    {
        _text.text = _enemy.Count.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (DetectCollisions)
        {
            if (other.TryGetComponent(out Ally ally))
            {
                AlliesGroup.Instance.Battle(this);
                DetectCollisions = false;
            }
        }
    }

    private int GetRing()
    {
        int ring = 0;
        int enemies = _enemiesCount - 1;
        while (true)
        {
            enemies -= _enemiesPerRing * ring;
            ring++;
            if (enemies <= 0)
                break;
        }

        return ring - 1;
    }

    private List<SpawnPoint> GetSpawnPoints(int rings)
    {
        List<SpawnPoint> points = new List<SpawnPoint>();
        for (int currentRing = 1; currentRing <= rings; currentRing++)
        {
            for (float angle = 0; angle <= 360f; angle += 360f / (_enemiesPerRing * currentRing))
            {
                if (points.Count < _enemiesCount)
                {
                    Vector3 position = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0f, Mathf.Cos(angle * Mathf.Deg2Rad)) / 2f * currentRing * 0.5f;
                    points.Add(new SpawnPoint(_original.transform.position + position, currentRing));
                }else
                {
                    break;
                }
            }
        }

        return points;
    }

    private void SpawnEnemies(List<SpawnPoint> spawnPoint)
    {
        for (int i = 0; i < spawnPoint.Count; i ++)
        {
            _enemy.Add(spawnPoint[i].Spawn(_original, transform));
        }
    }

    private void Sort(int ring, List<SpawnPoint> pointsToSort)
    {
        pointsToSort.Sort((x, y) => (x.Ring).CompareTo(y.Ring));
        pointsToSort.Reverse();

    }

    public void Destory()
    {
        Destroy(gameObject);
    }
}

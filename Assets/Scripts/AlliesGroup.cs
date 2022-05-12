using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlliesGroup : MonoBehaviour
{
    private const float DensityCoefficient = 0.5f;
    private const int AlliesPerRing = 6;
    public static AlliesGroup Instance;
    [SerializeField] public Ally _original;
    [SerializeField] public Officer _officer;
    public Ally original => _original;
    private Vector3 _centerPoint => transform.position;
    [SerializeField] private int _maxAlliesRings;
    private float _totalSpawnPoints;

    private List<SpawnPoint> _spawnPoint = new List<SpawnPoint>();
    private List<Ally> _ally = new List<Ally>();
    public int Count => _ally.Count;

    private int FilledRings { get; set; }

    #region OnAlliesChanged Event
    public delegate void FilledRingsChanged();
    public event FilledRingsChanged OnAlliesChanged;
    #endregion

    private bool BossAttackStarted = false;

    public void Add(Ally ally)
    {
        _ally.Add(ally);
    }

    [SerializeField] private float _leftBound;
    [SerializeField] private float _rightBound;

    [SerializeField] private TextMeshPro _text;

    private void Awake()
    {
        Instance = this;
        FilledRings = 1;
    }

    public void SetOriginal()
    {
        
            _original = _ally[0];
            print($"ORIGINAL {_ally[0].gameObject.name}");
        
    }
    private void Start()
    {
        _ally.Add(_original);

        SpawnPoint spawnPoint = new SpawnPoint(_original.transform.localPosition, 0);
        
        _ally[0].SpawnPoint = spawnPoint;
        
        spawnPoint.SetEmpty(false);
        _spawnPoint.Add(spawnPoint);
        for (int allyRing = 1; allyRing < _maxAlliesRings; allyRing++)
        {
            for (float angle = 0; angle <= 360f; angle += 360f / (AlliesPerRing * allyRing))
            {
                Vector3 position = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0f, Mathf.Cos(angle * Mathf.Deg2Rad)) / 2f * allyRing * DensityCoefficient;
                _spawnPoint.Add(new SpawnPoint(position, allyRing));
            }
        }

        _totalSpawnPoints = _spawnPoint.Count;
    }

    public void Update()
    {
        if (_ally.Count > 0)
        {
            _text.text = _ally.Count.ToString();
        }
        else
        {
            _text.text = "";
        }
    }

    public bool TryGetAllySpawnPosition(out SpawnPoint spawnPoint)
    {
        spawnPoint = new SpawnPoint(Vector3.zero, 0);
        List<SpawnPoint> emptyPoints = _spawnPoint.FindAll(point => point.isEmpty);

        if (emptyPoints.Count > 0)
        {
            
            spawnPoint = emptyPoints[0];
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UpdateRingsValue()
    {
        List<SpawnPoint> list = _spawnPoint.FindAll(point => !point.isEmpty);
        int LastPointIndex = 0;
        for (int i = 0; i < _spawnPoint.Count; i++)
        {
            if (list[list.Count - 1] == _spawnPoint[i])
            {
                LastPointIndex = i;
                break;
            }
        }

        for (int i = 0; i < _maxAlliesRings; i++)
        {
            LastPointIndex -= i * AlliesPerRing;
            if (LastPointIndex <= 0)
            {
                FilledRings = i;
                break;
            }
        }
    }

    public float GetBound(BoundBorder boundBorder)
    {
        float bound = boundBorder == BoundBorder.Right ? _rightBound - FilledRings * 0.23f : _leftBound + FilledRings * 0.23f;
        return bound;
    }

    #region Battle
    public void Battle(EnemiesGroup group)
    {
        StartCoroutine(StartBattle(group.GetEnemies(), group));
    }

    public void Battle(Boss boss)
    {
        if (!BossAttackStarted)
        {
            StartCoroutine(StartBattle(boss));
            BossAttackStarted = true;
        }
    }
    public void Battle(Gunner gunner)
    {
        StartCoroutine(StartBattle(gunner));
    }

    private IEnumerator StartBattle(List<Enemy> enemies, EnemiesGroup groupToRemove)
    {
        MovementController.Instance.ChangeControllerState();
        _ally.ForEach(ally => ally.Attack(true));
        _officer.Idle(true);
        enemies.ForEach(enemies => enemies.Attack());
        yield return new WaitForSeconds(0.25f);
        while (true)
        {
            if (_ally.Count == 0)
            {

                break;
            }

            if (enemies.Count == 0)
            {
                break;
            }

            _ally[0].Weapon.Fire();
            enemies[0].Weapon.Fire();
            SoundsController.Instance.Play(Sound.Fire);
            Enemy enemyToDestroy = enemies[0];
            enemyToDestroy.SelfDestroy();
            enemies.Remove(enemies[0]);
            Ally allyToDestroy = _ally[0];
            Kill(allyToDestroy);
            yield return new WaitForSeconds(0.125f);


            /*
            Destroy(enemyToDestroy.gameObject);
            */

            yield return new WaitForEndOfFrame();

            if (_ally.Count == 0)
            {

                break;
            }
            if (enemies.Count == 0)
            {
                _officer.Idle(false);
                break;
            }
        }

        if (_ally.Count > 0)
        {
            MovementController.Instance.ChangeControllerState();
            SetOriginal();
            _ally.ForEach(ally => ally.Attack(false));
            _officer.Run(true);
            groupToRemove.Destory();
        }

    }
    private IEnumerator StartBattle(Gunner gunner)
    {
        if (_ally.Count != 0)
        {
            MovementController.Instance.ChangeControllerState();
            _ally.ForEach(ally => ally.Attack(true));
            _officer.Idle(true);
            gunner.Attack();
            yield return new WaitForSeconds(0.25f);
            while (gunner.currentHealth > 0)
            {
                if (_ally.Count == 0)
                    break;
                if (gunner.currentHealth == 0)
                    break;
                gunner.TakeDamage(1);

                SoundsController.Instance.Play(Sound.Fire);
                _ally[0].Weapon.Fire();
                gunner.Weapon.Fire();
                yield return new WaitForSeconds(0.125f);

                yield return new WaitForEndOfFrame();

                Ally allyToDestroy = _ally[0];
                Kill(allyToDestroy);

                if (_ally.Count == 0)
                {
                    gunner.Idle();



                    break;
                }
                else if (gunner.currentHealth == 0)
                {
                    _ally.ForEach(ally => ally.Attack(false));
                    _officer.Run(true);
                    MovementController.Instance.ChangeControllerState();
                    gunner.SelfDestroy();
                    break;
                }

            }

        }


    }
    private IEnumerator StartBattle(Boss boss)
    {
        MovementController.Instance.ChangeControllerState();
        while (boss.Helth > 0)
        {
            _ally.ForEach(ally => ally.Attack(true));
            _officer.Idle(true);
            for (int i = 0; i < _ally.Count; i++)
            {
                _ally[i].Weapon.Fire();
                boss.TakeDamage(1);

                SoundsController.Instance.Play(Sound.Fire);
                yield return new WaitForSeconds(0.11f);

            }

            if (_ally.Count != 0)
            {
                boss.KillAllies();
            }

            yield return new WaitForSeconds(0.75f);
        }
        MovementController.Instance.ChangeControllerState();
        _officer.Run(true);
        _ally.ForEach(ally => ally.Attack(false));
    }
    #endregion

    public void Run()
    {
        _ally.ForEach(ally => ally.Run(true));
        _officer.Run(true);
    }

    public void Dancing()
    {
        _ally.ForEach(ally => ally.Dancing());
    }

    public void KillAllies(Boss bossWichAttack)
    {
        if (_ally.Count != 0)
        {
            int alliesToKill = Random.Range(1, 3);
            int i = 0;
            while (i < alliesToKill && _ally.Count > 0)
            {
                if (_ally.Count > 0)
                {
                    _ally[0].Die(true);
                    _ally[0].SelfDestroyWithDelay(0.5f);
                    _ally.Remove(_ally[0]);

                }
                i++;
            }

            if (_ally.Count == 0)
            {
                bossWichAttack.DestroyCollider();
                _officer.Death(true);
                UIManager.Instance.ShowCondition(Condition.Lose);
            }
            bossWichAttack.Idle();
        }
    }

    public void Kill(Ally ally)
    {

        if (_ally.Count != 0)
        {
            _ally.Remove(ally);
            ally.SpawnPoint.Despawn();
            if (_original == null && _ally.Count != 0)
            {
                _original = _ally[0];
            }
            ally.Die(true);
            ally.SelfDestroyWithDelay(0.5f);
            
            if (_ally.Count <= 0)
            {
                _officer.Death(true);

                UIManager.Instance.ShowCondition(Condition.Lose);
            }
        }
        else
        {
            _officer.Death(true);

            UIManager.Instance.ShowCondition(Condition.Lose);
        }

    }
    public void Kill(Ally ally, bool byObstacle)
    {

        if (_ally.Count != 0)
        {
            _ally.Remove(ally);
            ally.SpawnPoint.Despawn();
            ally.SelfDestroy();

            if (_ally.Count <= 0)
            {
             
                    MovementController.Instance.ChangeControllerState();
                
                _officer.Death(true);

                UIManager.Instance.ShowCondition(Condition.Lose);
            }
            else
            {
                if (_original == null)
                {
                    _original = _ally[0];
                }
                //_spawnPoint.Remove(ally.SpawnPoint);
            }
        }
        else
        {
            

            MovementController.Instance.ChangeControllerState();
            
            _officer.Death(true);

            UIManager.Instance.ShowCondition(Condition.Lose);
        }

    }

    public void Substruct(float count)
    {
        if (_ally.Count - count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                Kill(_ally[_ally.Count - 1]);
            }
        }
        else
        {
            while (_ally.Count > 0)
            {
                Kill(_ally[0]);
            }
            _officer.Death(true);
            UIManager.Instance.ShowCondition(Condition.Lose);
            MovementController.Instance.ChangeControllerState();
        }
    }
}

public enum BoundBorder
{
    Left,
    Right
}

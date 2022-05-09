using System.Collections.Generic;
using UnityEngine;


public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private List<Wall> _wall = new List<Wall>();
    [SerializeField] private List<EnemiesGroup> _enemiesGroup = new List<EnemiesGroup>();
    [SerializeField] private Boss _boss;

    private void Awake()
    {
        GenerateEnemiesGroup();
    }

    private void GenerateEnemiesGroup()
    {
        for (int i = 0; i < _enemiesGroup.Count; i++)
        {
            int EnemiesValue = Random.Range(5, 20);
            int lowerValue = EnemiesValue - Random.Range(0, EnemiesValue / 4);
            int higherValue = EnemiesValue + Random.Range(5, (int)(EnemiesValue * 1.5f));
            _enemiesGroup[i].SetEnemiesCount(EnemiesValue);

            int leftValue = 0;
            int rightValue = 0;
            if (Random.Range(0, 2) == 0)
            {
                leftValue = higherValue;
                rightValue = lowerValue;
            }else
            {
                leftValue = lowerValue;
                rightValue = higherValue;
            }

            _wall[i].SetWallValue(leftValue, rightValue);
        }
    }
}

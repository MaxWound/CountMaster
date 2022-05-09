using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private UIPanel _UIPanel;
    [SerializeField] private VictoryScreen _victoryScreen;
    [SerializeField] private LoseScreen _loseScreen;
    [SerializeField] private StartText _startText;

    private void Awake()
    {
        Instance = this ;
    }

    private void Start()
    {
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        yield return new WaitUntil(() => Input.GetMouseButton(0));
        MovementController.Instance.Enable();
        MovementController.Instance.ChangeControllerState();
        _UIPanel.Hide();
    }

    public void ShowCondition(Condition condition)
    {
        switch (condition)
        {
            case Condition.Victory: _victoryScreen.Show(); _loseScreen.Hide();
                break;
            case Condition.Lose: _loseScreen.Show();_victoryScreen.Hide();
                break;
        }
    }
}

public enum Condition
{
    Victory,
    Lose
}
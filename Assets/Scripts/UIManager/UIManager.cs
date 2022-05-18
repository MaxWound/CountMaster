using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private ScreenSettings _SettingsScreen;
    [SerializeField] private UIPanel _UIPanel;
    [SerializeField] private VictoryScreen _victoryScreen;
    [SerializeField] private LoseScreen _loseScreen;
    [SerializeField] private StartText _startText;
    private bool SoundEnabled;

    private void Awake()
    {
        

        Instance = this ;
    }

    private void Start()
    {
        StartCoroutine(StartGame());
    }
    public void ToogleVibrate()
    {
        AlliesGroup.Instance.ToogleToVibrate();
    }
    public void ToogleSettings()
    {
        _SettingsScreen.ToogleShow();
    }
    private IEnumerator StartGame()
    {
        yield return new WaitUntil(() => Input.GetMouseButton(0));
        MovementController.Instance.Enable();
        MovementController.Instance.ChangeControllerState();
        AlliesGroup.Instance.StartSteps();
        _UIPanel.Hide();
    }
    public void ToogleSound()
    {
        CameraMovement.Instance.ToogleSound();
    }

    public void ShowCondition(Condition condition)
    {
        print("ShowCondition");
        switch (condition)
        {
            case Condition.Victory: _victoryScreen.Show(); _loseScreen.Hide();
                break;
            case Condition.Lose: _loseScreen.Show();_victoryScreen.Hide();
                break;
        }
        AlliesGroup.Instance.StopSteps();
    }
    public void ShowConditionWithDelay(Condition condition , float t)
    {
        StartCoroutine(StartShowConditionWithDelay(condition, t));
    }
    public IEnumerator StartShowConditionWithDelay(Condition condition ,float t)
    {
        AlliesGroup.Instance.StopSteps();
        yield return new WaitForSeconds(t);
        print("ShowCondition");
        switch (condition)
        {
            case Condition.Victory:
                _victoryScreen.Show(); _loseScreen.Hide();
                break;
            case Condition.Lose:
                _loseScreen.Show(); _victoryScreen.Hide();
                break;
        }
        
    }
    
}

public enum Condition
{
    Victory,
    Lose
}
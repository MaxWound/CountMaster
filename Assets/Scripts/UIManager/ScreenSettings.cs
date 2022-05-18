using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSettings : MonoBehaviour
{

    [SerializeField] private CanvasGroup _canvasGroup;
    private bool isActive;
    private void Awake()
    {
        _canvasGroup.alpha = _canvasGroup.alpha == 1 ? 0 : 0;
        _canvasGroup.interactable = false;
        isActive = false;
    }

    public void ToogleShow()
    {
        isActive = !isActive;
        if (isActive)
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;


        }
        else
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}

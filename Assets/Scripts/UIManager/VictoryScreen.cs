using UnityEngine.SceneManagement;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    private void Awake()
    {
        _canvasGroup.alpha = _canvasGroup.alpha == 1 ? 0 : 0;
        _canvasGroup.interactable = false;
    }

    public void Show()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        SoundsController.Instance.Play(Sound.Victory);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Continue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

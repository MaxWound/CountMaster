using UnityEngine.SceneManagement;
using UnityEngine;

public class LoseScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    private void Awake()
    {
        _canvasGroup.alpha = 0f;
        _canvasGroup.interactable = false;
    }

    public void Show()
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.interactable = true;
        SoundsController.Instance.Play(Sound.Lose);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

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
        Finish.Instance.PlayParticles();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Continue()
    {
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}

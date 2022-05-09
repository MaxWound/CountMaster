using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartText : MonoBehaviour
{
    private const float AlphaSlope = 0.02f;
    [SerializeField] private TextMeshProUGUI _text;

    private void Start()
    {
        Animate();
    }

    public void Animate()
    {
        StartCoroutine(FadedAnimation());
    }

    private IEnumerator FadedAnimation()
    {
        float alpha = _text.color.a;
        bool fade = true;
        while (true)
        {
            if (alpha > 0.5f && fade)
            {
                alpha -= AlphaSlope;
            }else
            {
                fade = false;
                if (alpha == 1)
                    fade = true;

                alpha += AlphaSlope;
            }
            _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, alpha);
            yield return null;
        }
    }
}

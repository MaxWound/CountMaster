using UnityEngine.UI;
using UnityEngine;

public class HelthBar : MonoBehaviour
{
    [SerializeField] private Image _image;

    public void UpdateValue(float value)
    {
        if (value < 0)
            value = 0;
        _image.rectTransform.sizeDelta = new Vector2(value, _image.rectTransform.sizeDelta.y);
    }
}

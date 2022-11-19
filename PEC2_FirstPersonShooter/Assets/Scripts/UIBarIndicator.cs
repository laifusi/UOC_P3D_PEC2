using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBarIndicator : MonoBehaviour
{
    [SerializeField] Indicator type;

    private RectTransform rectTransform;
    private float maxWidth;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        maxWidth = rectTransform.sizeDelta.x;

        if(type == Indicator.Shield)
        {
            Health.OnShieldChange += UpdateIndicator;
        }
        else if(type == Indicator.Health)
        {
            Health.OnHealthChange += UpdateIndicator;
        }
    }

    private void UpdateIndicator(float currentNumber)
    {
        rectTransform.sizeDelta = new Vector2(currentNumber * maxWidth/100, rectTransform.sizeDelta.y);
    }

    private void OnDestroy()
    {
        if (type == Indicator.Shield)
        {
            Health.OnShieldChange -= UpdateIndicator;
        }
        else if (type == Indicator.Health)
        {
            Health.OnHealthChange -= UpdateIndicator;
        }
    }

    public enum Indicator
    {
        Health, Shield
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBarIndicator : MonoBehaviour
{
    [SerializeField] Indicator type;

    private RectTransform rectTransform;
    private float maxWidth;

    /// <summary>
    /// Initialize variables and listen to the listener associated to the Indicator's type
    /// </summary>
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

    /// <summary>
    /// Update the bar indicator: change the sizeDelta of the rectTransform according to the current value and the maxWidth
    /// </summary>
    /// <param name="currentNumber"></param>
    private void UpdateIndicator(float currentNumber)
    {
        rectTransform.sizeDelta = new Vector2(currentNumber * maxWidth/100, rectTransform.sizeDelta.y);
    }

    /// <summary>
    /// Stop listening to events when destroyed
    /// </summary>
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

    /// <summary>
    /// Enum to define the types of Indicators
    /// </summary>
    public enum Indicator
    {
        Health, Shield
    }
}

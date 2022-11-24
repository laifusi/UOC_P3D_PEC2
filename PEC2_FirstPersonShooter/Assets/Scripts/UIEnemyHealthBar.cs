using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEnemyHealthBar : MonoBehaviour
{
    [SerializeField] EnemyAIController enemy;

    private RectTransform rectTransform;
    private float maxWidth;

    private void Start()
    {
        enemy.OnLifeChange += UpdateIndicator;

        rectTransform = GetComponent<RectTransform>();
        maxWidth = rectTransform.sizeDelta.x;
    }

    private void UpdateIndicator(float life)
    {
        rectTransform.sizeDelta = new Vector2(life * maxWidth / 100, rectTransform.sizeDelta.y);
    }
}

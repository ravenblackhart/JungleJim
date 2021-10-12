using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GlowEdge : MonoBehaviour
{
    [SerializeField] private float GlowModerator = 1f;
    [SerializeField] private float SpreadSpeed = 2f;
    [SerializeField] private float colorSpeed;

    [SerializeField] private Color32 color1;
    [SerializeField] private Color32 color2;
    
    private float glowSize;
    private float posX;
    private float posY;
    private RectTransform scaler;

    private Image glowEdge;

    private void Start()
    {
        glowEdge = gameObject.GetComponent<Image>();
        scaler = GetComponent<RectTransform>();
    }

    void Update()
    {
        glowSize = 1 + (Mathf.Sin(Time.time / SpreadSpeed) * GlowModerator);

        scaler.localScale = new Vector2(glowSize, 1);

        glowEdge.color =Color32.Lerp(color1, color2,Mathf.Sin(Time.time) * colorSpeed);
    }
}

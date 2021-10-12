using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class GlowPulse : MonoBehaviour
{
    [SerializeField] private float GlowModerator = 1f;
    [SerializeField] private float PulseSpeed = 2f;
    private float glowSize; 
    
    void Update()
    {
        glowSize = (1 + (Mathf.Sin(Time.time / PulseSpeed) )* GlowModerator);

        transform.localScale = new Vector2(glowSize, glowSize);
    }
}

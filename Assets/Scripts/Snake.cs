using UnityEngine;

public class Snake : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] private Texture[] textures;
    [SerializeField] private float fps = 30;
    private int animationStep;
    private float fpsCounter;
    
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        fpsCounter += Time.deltaTime;
        if (fps >= 1f/fps)
        {
            animationStep++;
            if (animationStep == textures.Length)
            {
                animationStep = 0;
            }

//            lineRenderer.material.SetTexture("_MainTex", textures[animationStep]);

            fpsCounter = 0f; // not the best thing if game is lagging it skipps animationframes cause time.delta time took long time 
        }
    }
}

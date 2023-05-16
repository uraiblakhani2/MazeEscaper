using UnityEngine;
using System.Collections;

public class LightColorChanger : MonoBehaviour
{
    public Light directionalLight;
    public float transitionTime = 4f;
    public float waitTime = 30f;
    private Color color1 = new Color(0.3537736f, 0.5617576f, 1, 1);
    private Color color2 = new Color(0.9909983f, 0.7688679f, 1, 1);
    private bool isTransitioning = false;
    private float transitionStart;

    void Start()
    {
        // Set the initial light color
        directionalLight.color = color1;
        StartCoroutine(WaitAndChangeColor());
    }

    void Update()
    {
        if (isTransitioning)
        {
            float t = (Time.time - transitionStart) / transitionTime;
            directionalLight.color = Color.Lerp(color1, color2, t);

            if (t >= 1f)
            {
                isTransitioning = false;
                // Switch the colors for the next transition
                Color temp = color1;
                color1 = color2;
                color2 = temp;

                StartCoroutine(WaitAndChangeColor());
            }
        }
    }

    IEnumerator WaitAndChangeColor()
    {
        yield return new WaitForSeconds(waitTime);
        isTransitioning = true;
        transitionStart = Time.time;
    }
}

/// 
/// Script to make pet move a little but so it doesnt look so dead...
/// Made by Gracie Arianne Peh (S10265899G) 10/12/25 
/// Some reference to ChatGPT (i couldnt figure some stuff out)
/// 


using UnityEngine;

public class FloatingMotion : MonoBehaviour
{
    public float heightAmplitude = 0.2f;   // how high up/down
    public float widthAmplitude = 0.1f;    // how far left/right
    public float speed = 1f;               // overall speed of movement

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float y = Mathf.Sin(Time.time * speed) * heightAmplitude;
        float x = Mathf.Sin(Time.time * speed * 0.8f) * widthAmplitude;

        transform.localPosition = startPos + new Vector3(x, y, 0);
    }
}

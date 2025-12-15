/// 
/// Tiny script for food and toy prefabs (generated on food and toy card) will move up and down (less static)
/// Made by Gracie Arianne Peh (S10265899G) 11/12/25
/// 

using UnityEngine;

public class BounceForever : MonoBehaviour
{
    public float bounceHeight = 0.05f;   // how high it moves
    public float bounceSpeed = 2f;        // how fast it bounces

    private Vector3 startPos;

    void Start() 
    {
        startPos = transform.localPosition;
    }

    void Update() //to constantly change position
    {
        float yOffset = Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        transform.localPosition = startPos + new Vector3(0f, yOffset, 0f);
    }
}

/// 
/// Script to place items on the first plane detected automatically
/// Made by Gracie Arianne Peh (S10265899G) 11/12/25
/// Some reference to chatGPT
/// 


using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class AutoPlacement : MonoBehaviour
{
    public ARRaycastManager raycaster;

    public GameObject petPrefab;
    public GameObject foodPrefab;
    public GameObject toyPrefab;

    private bool placed = false;

    void Update()
    {
        if (placed) return;

        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        // Raycast from center of screen
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);

        if (raycaster.Raycast(screenCenter, hits, TrackableType.Planes))
        {
            Pose pose = hits[0].pose;

            // Spawn pet
            Instantiate(petPrefab, pose.position, Quaternion.identity);

            // Spawn food on the left
            Instantiate(foodPrefab, pose.position + new Vector3(-0.2f, 0, 0), Quaternion.identity);

            // Spawn toy on the right
            Instantiate(toyPrefab, pose.position + new Vector3(0.2f, 0, 0), Quaternion.identity);

            placed = true;
        }
    }
}

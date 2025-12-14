using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class AutoPlacement : MonoBehaviour
{
    private ARRaycastManager raycaster;

    public GameObject petPrefab;
    public GameObject foodPrefab;
    public GameObject toyPrefab;

    private bool placed = false;

    void Start()
    {
        // Find raycaster from persistent XR Origin
        raycaster = FindObjectOfType<ARRaycastManager>();

        if (raycaster == null)
        {
            Debug.LogError("ARRaycastManager not found. Make sure XR Origin persists from Home scene.");
        }
    }

    void Update()
    {
        if (placed || raycaster == null) return;

        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);

        if (raycaster.Raycast(screenCenter, hits, TrackableType.Planes))
        {
            Pose pose = hits[0].pose;

            // ---- PET ----
            Vector3 petPos = pose.position + Vector3.up * 0.10f; // lift pet
            GameObject pet = Instantiate(petPrefab, petPos, Quaternion.identity);

            // Face camera
            Vector3 lookDir = Camera.main.transform.forward;
            lookDir.y = 0;
            pet.transform.rotation = Quaternion.LookRotation(lookDir);

            // ---- FOOD & TOY ----
            Vector3 forward = -pet.transform.forward;
            Vector3 right = pet.transform.right;

            float forwardOffset = 0.15f;
            float sideOffset = 0.08f;
            float heightOffset = 0.03f;

            Vector3 foodPos = petPos
                              + forward * forwardOffset
                              - right * sideOffset
                              + Vector3.up * heightOffset;

            Vector3 toyPos = petPos
                             + forward * forwardOffset
                             + right * sideOffset
                             + Vector3.up * heightOffset;

            Instantiate(foodPrefab, foodPos, Quaternion.identity);
            Instantiate(toyPrefab, toyPos, Quaternion.identity);

            placed = true;
        }
    }
}

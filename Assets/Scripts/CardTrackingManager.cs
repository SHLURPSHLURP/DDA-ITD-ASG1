/// 
/// Script to handle card tracking and spawning of prefabs yipee
/// Made by Gracie Arianne Peh (S10265899G) 10/12/25
/// 


using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CardTrackingManager : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;

    [Header("Prefabs must match image names EXACTLY")]
    public GameObject[] placeablePrefabs;

    private Dictionary<string, GameObject> prefabLookup = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> spawnedObjects = new Dictionary<string, GameObject>();

    void Awake()
    {
        foreach (GameObject prefab in placeablePrefabs)
        {
            prefabLookup[prefab.name] = prefab;
        }
    }

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnImagesChanged;
    }

    void OnImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var img in args.added)
            ProcessImage(img);

        foreach (var img in args.updated)
            ProcessImage(img);

        foreach (var img in args.removed)
            HideImage(img);
    }

    void ProcessImage(ARTrackedImage img)
    {
        if (img.referenceImage == null)
            return;

        string name = img.referenceImage.name;
        if (string.IsNullOrEmpty(name))
            return;

        if (img.trackingState != TrackingState.Tracking)
        {
            HideImage(img);
            return;
        }

        Vector3 pos = img.transform.position;
        Quaternion rot = img.transform.rotation;

        if (!spawnedObjects.ContainsKey(name))
        {
            if (!prefabLookup.ContainsKey(name)) return;

            GameObject newObj = Instantiate(prefabLookup[name], pos, rot);
            spawnedObjects[name] = newObj;
        }

        GameObject obj = spawnedObjects[name];
        obj.SetActive(true);
        float offsetY = 0.02f;   // adjust as needed
        obj.transform.SetPositionAndRotation(pos + new Vector3(0, offsetY, 0), rot);

    }

    void HideImage(ARTrackedImage img)
    {
        if (img.referenceImage == null) return;

        string name = img.referenceImage.name;
        if (string.IsNullOrEmpty(name)) return;

        if (spawnedObjects.ContainsKey(name))
            spawnedObjects[name].SetActive(false);
    }
}

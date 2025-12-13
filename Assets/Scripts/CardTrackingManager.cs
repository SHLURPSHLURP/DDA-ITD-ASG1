using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CardTrackingManager : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;

    [Header("Prefabs must match image names EXACTLY")]
    public GameObject[] placeablePrefabs;

    private Dictionary<string, GameObject> prefabLookup = new();
    private Dictionary<string, GameObject> spawnedObjects = new();

    // Images that have already been used
    private HashSet<string> consumedImages = new();

    void Awake()
    {
        foreach (GameObject prefab in placeablePrefabs)
            prefabLookup[prefab.name] = prefab;
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

        string imageName = img.referenceImage.name;

        // 🚫 Already consumed → ignore forever
        if (consumedImages.Contains(imageName))
            return;

        if (!prefabLookup.ContainsKey(imageName))
            return;

        if (!spawnedObjects.ContainsKey(imageName))
            spawnedObjects[imageName] = Instantiate(prefabLookup[imageName]);

        GameObject obj = spawnedObjects[imageName];

        if (img.trackingState != TrackingState.Tracking)
        {
            obj.SetActive(false);
            return;
        }

        obj.SetActive(true);
        obj.transform.SetPositionAndRotation(
            img.transform.position + Vector3.up * 0.02f,
            img.transform.rotation
        );

        // Pet presence
        if (imageName == "PetCard")
            GameManager.Instance.petPresent = true;
    }

    void HideImage(ARTrackedImage img)
    {
        if (img.referenceImage == null)
            return;

        string imageName = img.referenceImage.name;

        if (spawnedObjects.ContainsKey(imageName))
            spawnedObjects[imageName].SetActive(false);

        if (imageName == "PetCard")
            GameManager.Instance.petPresent = false;
    }

    // 🔑 CALLED BY CardAction BEFORE REMOVAL
    public void MarkImageAsConsumed(string imageName)
    {
        consumedImages.Add(imageName);

        if (spawnedObjects.ContainsKey(imageName))
        {
            Destroy(spawnedObjects[imageName]);
            spawnedObjects.Remove(imageName);
        }
    }
}

/// 
/// script for spawned prefabs on card and checking for pet being scanned or not (linked to game state)
/// Made by Gracie Arianne Peh (S10265899G) 11/12/25
/// 

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CardTrackingManager : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;
    // prefab names must match reference image names
    public GameObject[] placeablePrefabs;

    // lookup table: image name -> prefab to spawn
    private Dictionary<string, GameObject> prefabLookup = new();

    // tracks spawned objects so we only spawn one per image name
    private Dictionary<string, GameObject> spawnedObjects = new();

    void Awake()
    {
        // populate lookup dictionary so we can quickly find prefabs by image name
        foreach (GameObject prefab in placeablePrefabs)
            prefabLookup[prefab.name] = prefab;
    }

    void OnEnable()
    {
        // subscribe to ar image tracking events when this script is enabled
        trackedImageManager.trackedImagesChanged += OnImagesChanged;
    }

    void OnDisable()
    {
        // unsubscribe from ar image tracking events when disabled
        trackedImageManager.trackedImagesChanged -= OnImagesChanged;
    }

    void OnImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        // handle newly detected images
        foreach (var img in args.added)
            ProcessImage(img);

        // handle updated images (movement, tracking state changes)
        foreach (var img in args.updated)
            ProcessImage(img);

        // handle images that are no longer tracked
        foreach (var img in args.removed)
            HideImage(img);
    }

    void ProcessImage(ARTrackedImage img)
    {
        // safety check: ensure the reference image exists
        if (img.referenceImage == null)
            return;

        // get the name of the detected reference image
        string imageName = img.referenceImage.name;

        // ignore images that do not have a matching prefab
        if (!prefabLookup.ContainsKey(imageName))
            return;

        // if this image has not spawned an object yet, create it
        if (!spawnedObjects.ContainsKey(imageName))
            spawnedObjects[imageName] = Instantiate(prefabLookup[imageName]);

        GameObject obj = spawnedObjects[imageName];

        // if image is not actively tracked, hide the object
        if (img.trackingState != TrackingState.Tracking)
        {
            obj.SetActive(false);
            return;
        }

        // image is actively tracked, show and move the object
        obj.SetActive(true);
        obj.transform.SetPositionAndRotation(
            img.transform.position + Vector3.up * 0.02f,
            img.transform.rotation
        );

        // if the detected image is the pet card, mark pet as present in game state
        if (imageName == "PetCard")
            GameState.Instance.petPresent = true;
    }

    void HideImage(ARTrackedImage img)
    {
        // safety check
        if (img.referenceImage == null)
            return;

        // get image name
        string imageName = img.referenceImage.name;

        // hide the spawned object if it exists
        if (spawnedObjects.ContainsKey(imageName))
            spawnedObjects[imageName].SetActive(false);

        // if pet card is lost, update game state
        if (imageName == "PetCard")
            GameState.Instance.petPresent = false;
    }
}

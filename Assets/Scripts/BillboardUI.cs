/// 
/// Script so that UI will always face player
/// Made by Gracie Arianne Peh (S10265899G) 11/12/25
/// This was chatGPT generated, the only thing i wrote in this file are the comments lololol
/// 

using UnityEngine;

public class BillboardUI : MonoBehaviour
{
    void LateUpdate()
    {
        if (Camera.main == null) return;

        // Make the UI face the camera
        transform.forward = Camera.main.transform.forward;
    }
}


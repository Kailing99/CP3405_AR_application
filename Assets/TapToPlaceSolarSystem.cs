using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TapToPlaceSolarSystem : MonoBehaviour
{
    // Reference to the solar system prefab
    public GameObject solarSystemPrefab;

    // Raycast manager to detect taps
    private ARRaycastManager raycastManager;

    // List to store raycast hits
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // This will initialize the raycast manager
    private void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    // This checks for taps every frame
    private void Update()
    {
        // Make sure there is at least one touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Check if the touch just began
            if (touch.phase == TouchPhase.Began)
            {
                // Perform raycast to detect planes in AR
                if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    // Get the hit position where the user tapped
                    Pose hitPose = hits[0].pose;

                    // Place the solar system prefab at the hit position
                    Instantiate(solarSystemPrefab, hitPose.position, hitPose.rotation);
                }
            }
        }
    }
}

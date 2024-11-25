using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlanetInteractionManager : MonoBehaviour
{
    public ARRaycastManager raycastManager; 
    public List<GameObject> planetPanels;   

    private Dictionary<string, GameObject> planetDictionary;

    void Start()
    {
        // Initialize dictionary linking planet names to their panels
        planetDictionary = new Dictionary<string, GameObject>();
        foreach (GameObject panel in planetPanels)
        {
            planetDictionary[panel.name] = panel;
            panel.SetActive(false); // Start with all panels inactive
        }
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Perform AR raycast from touch position
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                raycastManager.Raycast(touch.position, hits, TrackableType.Planes);

                // Perform a physics raycast to detect the planets
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    // Check if the hit object has a corresponding panel in the dictionary
                    string planetName = hitInfo.collider.gameObject.name;
                    if (planetDictionary.ContainsKey(planetName))
                    {
                        // Deactivate all panels first
                        foreach (var panel in planetPanels)
                        {
                            panel.SetActive(false);
                        }

                        // Activate the corresponding panel
                        planetDictionary[planetName].SetActive(true);
                    }
                }
            }
        }
    }
}


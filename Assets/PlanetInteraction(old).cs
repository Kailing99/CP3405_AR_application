using UnityEngine;

public class PlanetInteraction : MonoBehaviour
{
    public GameObject infoPanel;  // The UI Panel to show info
    public UnityEngine.UI.Text infoText;  // The Text component inside the panel

    void Update()
    {
        // Detects if the user touches the screen
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Cast a ray from the camera to the touch position
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            // Check if the ray hits a planet
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Planet"))
                {
                    ShowPlanetInfo(hit.transform.gameObject);
                }
            }
        }
    }

    void ShowPlanetInfo(GameObject planet)
    {
        // Activate the info panel and update the text with planet info
        infoPanel.SetActive(true);
        infoText.text = "Planet Name: " + planet.name + "\nMore info here...";
    }

    public void CloseInfoPanel()
    {
        // Deactivate the info panel
        infoPanel.SetActive(false);
    }
}

using UnityEngine;

public class PlanetInteraction : MonoBehaviour
{
    public GameObject infoPanel;

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
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
        infoPanel.SetActive(true);
        infoPanel.GetComponentInChildren<UnityEngine.UI.Text>().text = "Info about " + planet.name;
    }

    public void CloseInfoPanel()
    {
        infoPanel.SetActive(false);
    }
}

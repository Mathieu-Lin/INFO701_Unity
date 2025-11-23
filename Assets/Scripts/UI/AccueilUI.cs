using UnityEngine;
using UnityEngine.UI;

public class AccueilUI : MonoBehaviour
{
    [SerializeField] private GameObject panelAccueil;
    private bool isReady = false;
    private bool hasStarted = false;

    void Start()
    {
        panelAccueil.SetActive(false); // caché au début
        PokemonManager.Instance.StartDownload(OnDownloadComplete);
    }

    void OnDownloadComplete()
    {
        panelAccueil.SetActive(true); // affiché une fois les données prêtes
        isReady = true;
    }

    void Update()
    {
        if (!isReady || hasStarted) return;

        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            hasStarted = true;
            panelAccueil.SetActive(false);
        }
    }
}
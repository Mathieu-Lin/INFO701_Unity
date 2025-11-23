using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LoadingUI : MonoBehaviour
{
    [Header("UI Références")]
    [SerializeField] private Button clickableButton;
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private GameObject accueilPanel; // panneau à désactiver après clic

    private Coroutine typingCoroutine;
    private bool hasStarted = false;

    void Start()
    {
        if (PokemonManager.Instance == null)
        {
            Debug.LogError("PokemonManager.Instance est null. Vérifie qu’il est bien présent dans la scène.");
            return;
        }

        if (clickableButton == null || statusText == null || accueilPanel == null)
        {
            Debug.LogError("Références manquantes dans LoadingUI. Vérifie les assignations dans l’inspecteur.");
            return;
        }

        clickableButton.interactable = false;
        clickableButton.onClick.AddListener(OnStartClicked);
        statusText.text = "";

        StartTypeText("Chargement des Pokémon...");

        PokemonManager.Instance.StartDownload(() =>
        {
            StartTypeText("Chargement terminé !\nAppuyez pour continuer");
            clickableButton.interactable = true;
        });
    }

    void StartTypeText(string fullText)
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(fullText));
    }

    IEnumerator TypeText(string fullText)
    {
        statusText.text = "";
        foreach (char c in fullText)
        {
            statusText.text += c;
            yield return new WaitForSeconds(0.03f);
        }
    }

    void OnStartClicked()
    {
        if (hasStarted) return;
        hasStarted = true;

        clickableButton.interactable = false;
        StartTypeText("Bienvenue !");
        accueilPanel.SetActive(false); // masque l’accueil
    }
}
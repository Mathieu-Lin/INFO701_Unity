using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("[GameInitializer] Démarrage du jeu…");

        PokemonManager.Instance.StartDownload(() =>
        {
            Debug.Log("[GameInitializer] Téléchargement terminé, initialisation de l’équipe…");
            PokemonTeamManager.Instance.InitializeDefaultTeam();
            PokemonUIManager.Instance.TeamPanelOpen(); // ← test visuel
        });
    }
}
using UnityEngine;

public class TeamPanelController : MonoBehaviour
{
    [Header("Panel Principal de l'équipe")]
    public GameObject panelRoot;

    /// <summary>
    /// Ouvre le panel de l’équipe.
    /// </summary>
    public void OpenTeamPanel()
    {
        panelRoot.SetActive(true);
    }

    /// <summary>
    /// Ferme le panel de l’équipe.
    /// </summary>
    public void CloseTeamPanel()
    {
        panelRoot.SetActive(false);
    }
}

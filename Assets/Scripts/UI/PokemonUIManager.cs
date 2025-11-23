using UnityEngine;

public class PokemonUIManager : MonoBehaviour
{
    public static PokemonUIManager Instance;

    [Header("Panels")]
    public GameObject teamPanel;
    public GameObject detailPanel;

    [Header("Team UI")]
    public Transform teamContainer;
    public PokemonSlotUI[] pokemonSlots = new PokemonSlotUI[6];

    [Header("Detail UI")]
    public PokemonDetailUI detailUI;

    private void Awake() => Instance = this;

    public void TeamPanelOpen()
    {
        RefreshTeamUI();
        teamPanel.SetActive(true);
    }

    public void TeamPanelClose() => teamPanel.SetActive(false);

    public void RefreshTeamUI()
    {
        var team = PokemonTeamManager.Instance.team;

        for (int i = 0; i < pokemonSlots.Length; i++)
        {
            var slot = pokemonSlots[i];
            if (slot == null) continue;

            if (i < team.Count)
                slot.Setup(team[i], i);
            else
                slot.Setup(null, i);
        }
    }
    public void ShowDetails(int index)
    {
        var p = PokemonTeamManager.Instance.team[index];
        detailUI.Show(p);
        detailPanel.SetActive(true);
    }

    public void DetailPanelClose() => detailPanel.SetActive(false);
}
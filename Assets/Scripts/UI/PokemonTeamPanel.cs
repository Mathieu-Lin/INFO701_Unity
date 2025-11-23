using UnityEngine;

public class PokemonTeamPanel : MonoBehaviour
{
    [SerializeField] private PokemonSlotUI[] slots = new PokemonSlotUI[6];

    private void OnEnable()
    {
        RefreshSlots();
    }

    public void RefreshSlots()
    {
        var team = PokemonTeamManager.Instance?.team;
        if (team == null) return;

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null) continue;

            if (i < team.Count)
            {
                slots[i].gameObject.SetActive(true);
                slots[i].Setup(team[i], i);
            }
            else
            {
                slots[i].Setup(null, i); // slot vide
                slots[i].gameObject.SetActive(true); // visible mais grisé
            }
        }
    }
}
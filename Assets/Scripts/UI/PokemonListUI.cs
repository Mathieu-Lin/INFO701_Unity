using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PokemonListUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Button toggleButton;
    [SerializeField] private Transform listContainer;
    [SerializeField] private GameObject pokemonCardPrefab;

    void Start()
    {
        toggleButton.onClick.AddListener(() => panel.SetActive(!panel.activeSelf));
        PopulateList();
    }

    void PopulateList()
    {
        foreach (Transform child in listContainer) Destroy(child.gameObject);

        var pokemons = PokemonManager.Instance?.pokemons;
        if (pokemons == null) return;

        foreach (var p in pokemons)
        {
            var card = Instantiate(pokemonCardPrefab, listContainer);
            card.GetComponent<PokemonCardUI>().Setup(p);
        }
    }

}
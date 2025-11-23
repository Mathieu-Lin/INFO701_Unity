using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameSceneController : MonoBehaviour
{
    [SerializeField] private Transform pokemonListContainer;
    [SerializeField] private GameObject pokemonCardPrefab;

    void Start()
    {
        var pokemons = PokemonManager.Instance?.pokemons;
        if (pokemons == null || pokemons.Count == 0)
        {
            Debug.LogWarning("Aucun Pokémon trouvé dans le manager.");
            return;
        }

        foreach (var pokemon in pokemons)
        {
            GameObject card = Instantiate(pokemonCardPrefab, pokemonListContainer);
            var cardUI = card.GetComponent<PokemonCardUI>();
            cardUI.Setup(pokemon);
        }
    }
}
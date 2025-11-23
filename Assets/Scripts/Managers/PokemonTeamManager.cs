using System.Collections.Generic;
using UnityEngine;

public class PokemonTeamManager : MonoBehaviour
{
    public static PokemonTeamManager Instance;
    public List<PokemonData> team = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ← optionnel si tu veux garder l’équipe entre les scènes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InitializeDefaultTeam()
    {
        if (team.Count > 0)
        {
            Debug.Log("[TeamManager] Équipe déjà initialisée.");
            return;
        }

        var all = PokemonManager.Instance?.pokemons;
        Debug.Log($"[TeamManager] Liste récupérée : {all?.Count ?? 0} Pokémon");

        if (all == null || all.Count == 0)
        {
            Debug.LogError("[TeamManager] Liste des Pokémon vide !");
            return;
        }

        string[] starters = { "bulbasaur", "charmander", "squirtle", "pikachu" };
        foreach (var name in starters)
        {
            var p = all.Find(x => x.name.ToLower() == name);
            if (p != null)
            {
                team.Add(p);
                Debug.Log($"[TeamManager] {name} ajouté à l’équipe.");
            }
            else
            {
                Debug.LogWarning($"[TeamManager] {name} introuvable dans la liste.");
            }
        }
    }

    public bool AddPokemon(PokemonData data)
    {
        if (team.Count >= 6) return false;
        team.Add(data);
        return true;
    }

    public void RemovePokemon(int index)
    {
        if (index >= 0 && index < team.Count)
            team.RemoveAt(index);
    }

    public void Swap(int indexA, int indexB)
    {
        if (indexA < 0 || indexB < 0 || indexA >= team.Count || indexB >= team.Count) return;
        (team[indexA], team[indexB]) = (team[indexB], team[indexA]);
    }
}
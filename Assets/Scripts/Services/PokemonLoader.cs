using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Linq;
using System.Collections; // ← nécessaire pour IEnumerator

public class PokemonLoader : MonoBehaviour
{
    public string pokemonName = "charizard";
    public PokemonDisplay display;

    void Start() => StartCoroutine(LoadPokemon(pokemonName));

    IEnumerator LoadPokemon(string name)
    {
        string url = $"https://pokeapi.co/api/v2/pokemon/{name.ToLower()}";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            PokemonData data = JsonUtility.FromJson<PokemonData>(www.downloadHandler.text);
            display.DisplayPokemon(data);
        }
        else
        {
            Debug.LogError("Erreur : " + www.error);
        }
    }

    public static int CalculateStat(int baseStat, bool isHP)
    {
        int iv = 31, ev = 252, level = 50;
        return isHP
            ? Mathf.FloorToInt(((2 * baseStat + iv + (ev / 4)) * level) / 100f) + level + 10
            : Mathf.FloorToInt(((2 * baseStat + iv + (ev / 4)) * level) / 100f) + 5;
    }
}
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class PokemonManager : MonoBehaviour
{
    public static PokemonManager Instance;
    public List<PokemonData> pokemons = new List<PokemonData>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void StartDownload(System.Action onComplete)
    {
        StartCoroutine(DownloadGen1(onComplete));
    }

    IEnumerator DownloadGen1(System.Action onComplete)
    {
        string[] gen1 = { "bulbasaur", "charmander", "squirtle", "pikachu" };

        foreach (string name in gen1)
        {
            if (pokemons.Exists(p => p.name.ToLower() == name.ToLower()))
                continue;

            if (PokemonCache.Exists(name))
            {
                pokemons.Add(PokemonCache.Load(name));
                continue;
            }

            string url = $"https://pokeapi.co/api/v2/pokemon/{name}";
            UnityWebRequest www = UnityWebRequest.Get(url);
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string json = www.downloadHandler.text;

                // Parse les données principales
                PokemonData data = JsonUtility.FromJson<PokemonData>(json);

                // Récupération robuste de l'ID
                PokemonIdWrapper idWrapper = JsonUtility.FromJson<PokemonIdWrapper>(json);
                data.id = idWrapper.id;

                // Construction du sprite URL
                data.spriteUrl = $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{data.id}.png";

                pokemons.Add(data);
                PokemonCache.Save(name, data);

                Debug.Log($"[Téléchargé] {data.name} avec ID {data.id} et sprite {data.spriteUrl}");
            }
            else
            {
                Debug.LogError($"Erreur téléchargement {name} : {www.error}");
            }
        }

        onComplete?.Invoke();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            PokemonCache.ClearAllCache();
    }
}

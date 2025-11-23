using UnityEngine;
using System.IO;

public static class PokemonCache
{
    private static string GetPath(string name) =>
        Path.Combine(Application.persistentDataPath, $"pokemon_{name}.json");

    public static bool Exists(string name) =>
        File.Exists(GetPath(name));

    public static void Save(string name, PokemonData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(GetPath(name), json);
    }

    public static PokemonData Load(string name)
    {
        string json = File.ReadAllText(GetPath(name));
        return JsonUtility.FromJson<PokemonData>(json);
    }

    public static void ClearAllCache()
    {
        string[] cachedFiles = Directory.GetFiles(Application.persistentDataPath, "pokemon_*.json");

        foreach (string path in cachedFiles)
        {
            try
            {
                File.Delete(path);
                Debug.Log($"[Cache supprimé] {Path.GetFileName(path)}");
            }
            catch (IOException e)
            {
                Debug.LogError($"Erreur lors de la suppression du cache : {e.Message}");
            }
        }
    }

}
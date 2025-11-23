using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Linq;
using System.Collections;

public class PokemonDisplay : MonoBehaviour
{
    public Text nameText;
    public Text statsText;
    public Text abilityText;
    public Text movesText;
    public Image spriteImage;

    public void DisplayPokemon(PokemonData p)
    {
        nameText.text = p.name.ToUpper();

        // --- Stats ---
        string stats = "";
        foreach (var s in p.stats)
        {
            int value = PokemonLoader.CalculateStat(s.base_stat, s.stat.name == "hp");
            stats += $"{s.stat.name}: {value}\n";
        }
        statsText.text = stats;

        // --- Ability ---
        var ability = p.abilities.FirstOrDefault(a => !a.is_hidden)?.ability.name ?? "inconnu";
        abilityText.text = $"Talent : {ability}";

        // --- Moves (simplifié, compatible JsonUtility) ---
        var moves = p.moves
            .Select(m => m.move.name)
            .Distinct()
            .Take(4)
            .ToList();

        movesText.text = "Moves :\n" + string.Join("\n", moves);

        // --- Sprite ---
        StartCoroutine(LoadSprite(p.spriteUrl));
    }

    IEnumerator LoadSprite(string url)
    {
        if (string.IsNullOrEmpty(url))
            yield break;

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Texture2D tex = DownloadHandlerTexture.GetContent(www);
            spriteImage.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class PokemonDetailUI : MonoBehaviour
{
    public GameObject panel;
    public Image sprite;
    public TextMeshProUGUI title;
    public TextMeshProUGUI stats;

    public void Show(PokemonData data)
    {
        panel.SetActive(true);
        title.text = data.name.ToUpper();
        StartCoroutine(Utils.LoadSprite(data.spriteUrl, sprite));


        StringBuilder sb = new();
        foreach (var s in data.stats)
            sb.AppendLine($"{s.stat.name} : {s.base_stat}");

        stats.text = sb.ToString();
    }

    public void Hide() => panel.SetActive(false);
}
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PokemonCardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image spriteImage;
    [SerializeField] private Button selectButton;

    private PokemonData data;

    public void Setup(PokemonData d)
    {
        data = d;
        nameText.text = d.name;
        selectButton.onClick.AddListener(() => ShowDetails());

        StartCoroutine(LoadSprite(d.spriteUrl));  // ⬅ plus fiable
    }


    void ShowDetails()
    {
        // Appelle un autre UI pour afficher les stats
        Debug.Log($"Infos de {data.name} affichées");
    }

    IEnumerator LoadSprite(string url)
    {
        if (string.IsNullOrEmpty(url)) yield break;

        using (var www = UnityEngine.Networking.UnityWebRequestTexture.GetTexture(url))
        {
            yield return www.SendWebRequest();
            if (www.result == UnityEngine.Networking.UnityWebRequest.Result.Success)
            {
                var tex = UnityEngine.Networking.DownloadHandlerTexture.GetContent(www);
                spriteImage.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f);
            }
        }
    }
}
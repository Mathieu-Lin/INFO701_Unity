using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;

public class PokemonSlotUI : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image icon;
    public TextMeshProUGUI nameText;

    private int slotIndex;
    private Transform parent;
    private Canvas canvas;
    private RectTransform rect;

    private PokemonData currentData;
    private Color defaultColor = new Color(0.5f, 0.5f, 0.5f); // gris neutre

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        parent = transform.parent;
        canvas = FindAnyObjectByType<Canvas>();
    }

    public void Setup(PokemonData data, int index)
    {
        slotIndex = index;
        currentData = data;

        if (data != null)
        {
            nameText.text = data.name;
            string spriteUrl = data.spriteUrl;
            StartCoroutine(LoadSpriteSafe(spriteUrl));

        }
        else
        {
            nameText.text = "Vide";
            icon.sprite = null;
            icon.color = defaultColor;
        }
    }

    private IEnumerator LoadSpriteSafe(string url)
    {
        yield return new WaitUntil(() => gameObject.activeInHierarchy);
        yield return Utils.LoadSprite(url, icon);
        icon.color = Color.white; // reset couleur après chargement
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (currentData != null)
            PokemonUIManager.Instance.ShowDetails(slotIndex);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Transform targetTransform = Utils.GetClosestSlot(rect.position, parent);

        if (targetTransform != null && targetTransform != transform)
        {
            var targetSlot = targetTransform.GetComponent<PokemonSlotUI>();
            if (targetSlot != null)
            {
                int targetIndex = targetSlot.GetSlotIndex(); // ← méthode à ajouter
                PokemonTeamManager.Instance.Swap(slotIndex, targetIndex);
                PokemonUIManager.Instance.RefreshTeamUI();
            }
        }

        transform.SetParent(parent);
        rect.anchoredPosition = Vector2.zero;
    }

    public int GetSlotIndex()
{
    return slotIndex;
}
}
using UnityEngine;
using UnityEngine.UI;

public class UIButtonCloseDetailPanel : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            PokemonUIManager.Instance.DetailPanelClose();
        });
    }
}

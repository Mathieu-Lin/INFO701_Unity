using UnityEngine;
using UnityEngine.UI;

public class UIButtonOpenTeam : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            PokemonUIManager.Instance.TeamPanelOpen();
        });
    }
}

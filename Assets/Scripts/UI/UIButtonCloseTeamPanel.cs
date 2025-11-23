using UnityEngine;
using UnityEngine.UI;

public class UIButtonCloseTeamPanel : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            PokemonUIManager.Instance.TeamPanelClose();
        });
    }
}

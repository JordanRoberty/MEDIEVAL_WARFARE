using UnityEngine;
using UnityEngine.UI;

public class ChangeRuneButton : MonoBehaviour
{
    private InventoryItemIdentifier rune_identifier;
    private Button change_rune_button;

    private void Start()
    {
        rune_identifier = transform.parent.GetComponent<InventoryItemIdentifier>();
        change_rune_button = GetComponent<Button>();
        change_rune_button.onClick.AddListener(() => { GearsManager.Instance.start_rune_exchange(rune_identifier); });
    }
}

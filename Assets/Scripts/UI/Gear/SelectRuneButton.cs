using UnityEngine;
using UnityEngine.UI;

public class SelectRuneButton : MonoBehaviour
{
    private InventoryItemIdentifier rune_identifier;
    private Button select_rune_button;

    private void Start()
    {
        rune_identifier = transform.parent.GetComponent<InventoryItemIdentifier>();
        select_rune_button = GetComponent<Button>();
        select_rune_button.onClick.AddListener(() => { GearsManager.Instance.exchange_rune(rune_identifier); });
    }
}

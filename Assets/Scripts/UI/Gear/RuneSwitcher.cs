using UnityEngine;
using UnityEngine.UI;

public class RuneSwitcher : MonoBehaviour
{
    private InventoryItemIdentifier rune_identifier;
    private Button rune_switcher;

    private void Start()
    {
        rune_identifier = transform.parent.GetComponent<InventoryItemIdentifier>();
        rune_switcher = GetComponent<Button>();
        rune_switcher.onClick.AddListener(() => { GearsManager.Instance.open_runes_menu(rune_identifier); });
    }
}

using UnityEngine;
using UnityEngine.UI;

public class DeleteRune : MonoBehaviour
{
    private InventoryItemIdentifier rune_identifier;
    private Button remove_rune_button;

    private void Start()
    {
        rune_identifier = transform.parent.GetComponent<InventoryItemIdentifier>();
        remove_rune_button = GetComponent<Button>();
        remove_rune_button.onClick.AddListener(() => { GearsManager.Instance.remove_rune(rune_identifier); });
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.GameFoundation;
using UnityEngine.GameFoundation.Components;
using static GameFoundationUtils;

[RequireComponent(typeof(ItemView))]
public class AvailableRuneViewer : MonoBehaviour
{
    /*====== PRIVATE ======*/
    private string id;
    private ItemView item_viewer;
    private Button select_button;

    // Start is called before the first frame update
    private void Awake()
    {
        item_viewer = GetComponent<ItemView>();
        select_button = GetComponentInChildren<Button>();
        select_button.onClick.AddListener(() => { GearsManager.Instance.exchange_rune(id); });
    }

    public void init(InventoryItem rune)
    {
        id = rune.id;
        display_item_in_viewer(rune, item_viewer);
    }
}

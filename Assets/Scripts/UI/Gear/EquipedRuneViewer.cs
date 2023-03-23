using UnityEngine;
using UnityEngine.UI;
using UnityEngine.GameFoundation;
using UnityEngine.GameFoundation.Components;
using static GameFoundationUtils;

[RequireComponent(typeof(ItemView))]
public class EquipedRuneViewer : MonoBehaviour
{
    /*====== PRIVATE UI ======*/
    [SerializeField] private Button exchange_button;
    [SerializeField] private Button remove_button;

    /*====== PRIVATE ======*/
    private string id;
    private int slot;
    private ItemView item_viewer;

    private void Awake()
    {
        item_viewer = GetComponent<ItemView>();
        exchange_button.onClick.AddListener(() => { GearsManager.Instance.start_rune_exchange(slot); });
        remove_button.onClick.AddListener(() => { GearsManager.Instance.remove_rune(slot); });
    }

    public void init(InventoryItem rune, int rune_slot)
    {
        id = rune.id;
        slot = rune_slot;
        display_item_in_viewer(rune, item_viewer);
    }

}

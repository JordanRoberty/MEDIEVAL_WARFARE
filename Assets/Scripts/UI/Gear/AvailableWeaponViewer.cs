using UnityEngine;
using UnityEngine.UI;
using UnityEngine.GameFoundation;
using UnityEngine.GameFoundation.Components;
using static GameFoundationUtils;
using TMPro;

[RequireComponent(typeof(ItemView))]
public class AvailableWeaponViewer : MonoBehaviour
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
        select_button.onClick.AddListener(() => { GearsManager.Instance.exchange_weapon(id); });
    }

    public void init(InventoryItem weapon)
    {
        id = weapon.id;
        display_item_in_viewer(weapon, item_viewer);

        if (weapon.GetMutableProperty("equiped") == true)
        {
            select_button.interactable = false;
            select_button.GetComponentInChildren<TextMeshProUGUI>().text = "EQUIPED";
        }
    }

    public void init(InventoryItemDefinition weapon_definition)
    {
        item_viewer.SetDisplayName(weapon_definition.displayName);
        select_button.interactable = false;
    }
}

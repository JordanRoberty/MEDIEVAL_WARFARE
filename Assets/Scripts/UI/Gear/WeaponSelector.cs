using UnityEngine;
using UnityEngine.UI;
using UnityEngine.GameFoundation;
using UnityEngine.GameFoundation.Components;
using static GameFoundationUtils;
using TMPro;

[RequireComponent(typeof(ItemView))]
public class WeaponSelector : MonoBehaviour
{
    /*====== PUBLIC ======*/
    public string id { get; private set; }
    private Button select_button;

    /*====== PRIVATE UI ======*/

    /*====== PRIVATE ======*/
    private ItemView item_viewer;

    // Start is called before the first frame update
    private void Awake()
    {
        item_viewer = GetComponent<ItemView>();
        select_button = GetComponentInChildren<Button>();
        select_button.onClick.AddListener(() => {
            PlayerInfosManager.Instance.set_equiped_weapon(id);
            GearsManager.Instance.update_gear_menu();
        });
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

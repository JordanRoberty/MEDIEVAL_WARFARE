// Probably useless directives among the following
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.GameFoundation;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.UI;

public class GearDisplay : MonoBehaviour
{
    private GameObject itemDisplayPrefab;
    //private Transform contentTransform;

    void DisplayItems()
    {
        // Fill an array with all the items owned by the player
        List<InventoryItem> owned_items = new List<InventoryItem>();
        GameFoundationSdk.inventory.GetItems(owned_items);

        // Make sure the player has at least 1 item
        if(owned_items.Count == 0)
        {
            Debug.Log("No items owned");
            return;
        }

        // Loop over the owned items to add them to the Gear UI
        foreach (InventoryItem item in owned_items)
        {
            // Log the name of the item (for debugging purposes)
            Debug.Log("item name: " + item.definition.displayName);
            
            // Create a new UI element for the item
            //GameObject gear_item = Instantiate(itemDisplayPrefab); // TO UNCOMMENT
            //gear_item.transform.SetParent(GearDisplay); // TO UNCOMMENT

            // Add a Text component to the UI element
            //Text itemName = itemGO.AddComponent<Text>();
            //gear_item.transform.find("Item Name").gameObject = item.definition.displayName; // TO UNCOMMENT

            // Add the GF Item's image (a component?) to the gear item GO's icon
        //gear_item.transform = item.definition.displayImage; //FIXME
            //GetChild(int nupmero) // cf scripting API pur Transform (Child Count; Parent)
            // checker l'intérêt de getSibling
            // objet.transform.find("nom de l'enfant").gameObject
            // objet.transform.getChild(int numero)
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // Initialize the items' "label" (the UI element)
        itemDisplayPrefab = GameObject.FindWithTag("Gear Item");
        //itemDisplayPrefab.transform.SetParent(transform);

    }

    // Update is called once per frame
    void Update()
    {
        DisplayItems();
    }
}
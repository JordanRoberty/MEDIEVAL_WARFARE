// VGPT

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.GameFoundation;
using System.Linq;
using System.Threading.Tasks;

using UnityEngine.UI;
//using UnityEngine.GameFoundation.Data;

public class GearDisplay : MonoBehaviour
{
    private List<InventoryItem> _ownedItems;
    private Store _store;
    //private Inventory _inventory;

    void DisplayItems()
    {
        foreach (InventoryItem item in _ownedItems)
        {
            // Create a new UI element for the item
            GameObject itemGO = new GameObject();
            itemGO.transform.SetParent(transform);

            // Add a Image component to the itemGO
            Image itemImage = itemGO.AddComponent<Image>();
            itemImage.sprite = item.Definition.displayImage;

            // Add a Text component to the itemGO
            Text itemName = itemGO.AddComponent<Text>();
            itemName.text = item.Definition.displayName;
        }
    }

    void Start()
    {
        _store = GameFoundation.Store;

        // Get the list of owned items

        // v1
        auto _inventory = GameFoundation.Inventory;
        _ownedItems = _inventory.GetItems().Where(item => item.GetOwnership().IsOwned).ToList();

        // v2
        InventoryCatalog inventoryCatalog = InventoryCatalog.GetDefaultCatalog();
        List<InventoryItem> allItems = inventoryCatalog.GetItems();

        _ownedItems = allItems.Where(item => item.ownership.IsOwned).ToList();

    }

    // Update is called once per frame
    void Update()
    {
        DisplayItems();
    }
}*/

// V GF YT tuto

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.GameFoundation;
using UnityEngine.UI;

namespace _Demo._Scripts
{ 
    public class InventoryView : MonoBehaviour
    {
        public InventoryItemView prefab;
        public ScrollRect scrollRect;
        Changed in 1 asset
        Inventory View(MonoBehaviour)
        private List<InventoryItenView iterViews:
        Event function
        void Start()
        {
        itenViews = new List<InventoryItenView>();
        if (GameFoundationSdk.IsInitialized)
        1
        }
        else
            1
        SubcribeEvents():
        Setup():
        GameFoundationSdk.initialized Gane FoundationSdkoninitialized;
        }
        }
        Event function
        private void OnDestroy()
        {
            UnSubcribeEvents();
            GameFoundationSdk.initialized = GameFoundationSdk?ninitialized;
        }
        Frequently called 2 usages
        public void GameFoundationSdkoninitialized()
        f
        Debug.Log(age: GameFoundationSdk�ninitialized");
        UnSubcribeEvents();
        SubcribeEvents()
        Setup();
        }
        Frequently called 2 usages
        void UnSubcribeEvents()*/

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.GameFoundation;
using UnityEngine.UI;

public class GearDisplay : MonoBehaviour
{
    private List<InventoryItem> _ownedItems;
    private Store _store;

    void DisplayItems()
    {
        foreach (InventoryItem item in _ownedItems)
        {
            // Create a new UI element for the item
            GameObject itemGO = new GameObject();
            itemGO.transform.SetParent(transform);

            // Add a Image component to the itemGO
            Image itemImage = itemGO.AddComponent<Image>();
            itemImage.sprite = item.Definition.displayImage;

            // Add a Text component to the itemGO
            Text itemName = itemGO.AddComponent<Text>();
            itemName.text = item.Definition.displayName;
        }
    }

    void Start()
    {
        _store = GameFoundation.Store;

        InventoryCatalog inventoryCatalog = InventoryCatalog.GetDefaultCatalog();
        List<InventoryItem> allItems = inventoryCatalog.GetItems();

        _ownedItems = allItems.Where(item => item.ownership.IsOwned).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayItems();
    }
}*/

// V Copilot

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.GameFoundation;
using System.Linq;
using System.Threading.Tasks;

using UnityEngine.UI;

public class GearDisplay : MonoBehaviour
{
    *//*    private List<InventoryItem> inventory;
        private Store store;

        void DisplayItems()
        {
            inventory = GameFoundationSdk.inventory;
            store = GameFoundationSdk.catalog.store;

            var owned_items = inventory.items;

            foreach (var item in owned_items)
            {
                Debug.Log(item.key);
            }
        }

        void Start()
        {
            DisplayItems();
        }

        void Update()
        {
            DisplayItems();
        }*//*

    // Loop over all items in the inventory
    void DisplayItems()
    {
        foreach (InventoryItem item in GameFoundationSdk.inventory.items)
        {
            // Create a new UI element for the item
            GameObject itemGO = new GameObject();
            itemGO.transform.SetParent(transform);

            // Add a Image component to the itemGO
            Image itemImage = itemGO.AddComponent<Image>();
            itemImage.sprite = item.definition.displayImage;

            // Add a Text component to the itemGO
            Text itemName = itemGO.AddComponent<Text>();
            itemName.text = item.definition.displayName;
        }
    }

    void Start()
    {
        DisplayItems();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayItems();
    }
}*/

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
    //private List<InventoryItem> _ownedItems;
    //private Store _store;
    //private Inventory _inventory;
    private GameObject itemDisplayPrefab;
    //private Transform contentTransform;
    

    void DisplayItems()
    {

        // Fill an array with all the items owned by the player
        List<InventoryItem> target = new List<InventoryItem>();
        GameFoundationSdk.inventory.GetItems(target);

        //InventoryCatalog inventoryCatalog = InventoryCatalog.GetDefaultCatalog();
        //List<InventoryItem> allItems = inventoryManager.GetItems(ICollection<InventoryItem> target);

        //_ownedItems = allItems.Where(item => item.ownership.IsOwned).ToList();

        // Loop over the owned items to add them to the Gear UI
        // TODO: Make sure the target is not empty
        foreach (InventoryItem item in target)
        {
            // Get items
            // CP: var item = _inventory.GetItems().Where(item => item.GetOwnership().IsOwned).ToList();
            //int nb_of_items = GetItems(ICollection<InventoryItem> target = null, bool clearTarget = true);

            // Print the number of items
            //Debug.Log("Number of items: " + nb_of_items);

            // Log the name of the item
            Debug.Log("item name: " + item.definition.displayName);
            
            // Create a new UI element for the item
            GameObject gear_item = Instantiate(itemDisplayPrefab);
            gear_item.transform.SetParent(GearDisplay);

            // Add a Text component to the itemGO
            //Text itemName = itemGO.AddComponent<Text>();
            gear_item.transform.find("Item Name").gameObject = item.definition.displayName;

            // Add the GF Item's image (a component?) to the gear item GO's icon
            //gear_item.transform = item.definition.displayImage; //FIXME
            //GetChild(int nupmero) // cf scripting API pur Transform (Child Count; Parent)
            // checker l'intérêt de getSibling
            // objet.transform.find("nom de l'enfant").gameObject
            // objet.transform.getChild(int numero)



        }
    }
    

    void Start()
    {
        //_store = GameFoundation.Store;

        // Get the list of owned items

        // v1
        //auto _inventory = GameFoundation.Inventory;
        //_ownedItems = _inventory.GetItems().Where(item => item.GetOwnership().IsOwned).ToList();

        // v2
        //InventoryCatalog inventoryCatalog = InventoryCatalog.GetDefaultCatalog();
        //List<InventoryItem> allItems = inventoryCatalog.GetItems();

        //_ownedItems = allItems.Where(item => item.ownership.IsOwned).ToList();

        itemDisplayPrefab = GameObject.FindWithTag("Gear Item");
        //itemDisplayPrefab.transform.SetParent(transform);

    }

    // Update is called once per frame
    void Update()
    {
        DisplayItems();
    }
}
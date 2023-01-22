// VGPT

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.GameFoundation;
using System.Linq;
using System.Threading.Tasks;

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
            itemImage.sprite = item.GetDefinition().displayImage;

            // Add a Text component to the itemGO
            Text itemName = itemGO.AddComponent<Text>();
            itemName.text = item.GetDefinition().displayName;
        }
    }

    void Start()
    {
        _store = GameFoundation.Store;

        // Get the list of owned items

        // v1
/*        auto _inventory = GameFoundation.Inventory;
        _ownedItems = _inventory.GetItems().Where(item => item.GetOwnership().IsOwned).ToList();*/

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
}

// V2 (from GF YT tuto)

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
        Debug.Log(age: GameFoundationSdküninitialized");
        UnSubcribeEvents();
        SubcribeEvents()
        Setup();
        }
        Frequently called 2 usages
        void UnSubcribeEvents()*/
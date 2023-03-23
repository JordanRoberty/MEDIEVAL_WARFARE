using UnityEngine;
using UnityEngine.UI;

public class EmptyRuneSlotViewer : MonoBehaviour
{
    /*====== PRIVATE ======*/
    private int slot;
    private Button add_button;

    private void Start()
    {
        add_button = GetComponentInChildren<Button>();
        add_button.onClick.AddListener(() => { GearsManager.Instance.start_rune_exchange(slot); });
    }

    public void init(int rune_slot)
    {
        slot = rune_slot;
    }
}

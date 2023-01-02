using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Selector : MonoBehaviour
{
    [SerializeField]
    private Button left_button;
    [SerializeField]
    private Button right_button;
    private TMP_Dropdown dropdown;

    private void Awake()
    {
        dropdown = GetComponentInChildren<TMP_Dropdown>();
        dropdown.interactable = false;

        left_button.onClick.AddListener(delegate () { previous_option(); });
        right_button.onClick.AddListener(delegate () { next_option(); });
    }

    public void previous_option()
    {
        if (dropdown.value > 0)
        {
            dropdown.value--;
        }
    }

    public void next_option()
    {
        if (dropdown.value < dropdown.options.Count)
        {
            dropdown.value++;
        }
    }
}

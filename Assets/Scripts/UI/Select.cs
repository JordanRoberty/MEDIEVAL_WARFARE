using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class Select : MonoBehaviour
{
    protected TMP_Dropdown dropdown;

    [SerializeField]
    private Button left_button;
    [SerializeField]
    private Button right_button;

    protected virtual void Awake()
    {
        dropdown = GetComponentInChildren<TMP_Dropdown>();
        dropdown.interactable = false;

        left_button.onClick.AddListener(delegate () { previous_option(); });
        right_button.onClick.AddListener(delegate () { next_option(); });
    }

    public abstract void previous_option();

    public abstract void next_option();
}

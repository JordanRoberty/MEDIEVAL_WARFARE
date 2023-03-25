using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    [SerializeField] private Sprite full_shield;
    [SerializeField] private Sprite empty_shield;

    private Image image_renderer;

    private void Awake()
    {
        image_renderer = GetComponent<Image>();
        image_renderer.sprite = full_shield;
    }

    public void set_full()
    {
        image_renderer.sprite = full_shield;
    }

    public void set_empty()
    {
        image_renderer.sprite = empty_shield;
    }
}

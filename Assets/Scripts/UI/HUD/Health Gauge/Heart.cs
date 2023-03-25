using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    [SerializeField] private Sprite full_heart;
    [SerializeField] private Sprite empty_heart;

    private Image image_renderer;

    private void Awake()
    {
        image_renderer = GetComponent<Image>();
        image_renderer.sprite = full_heart;
    }

    public void set_full()
    {
        image_renderer.sprite = full_heart;
    }

    public void set_empty()
    {
        image_renderer.sprite = empty_heart;
    }
}

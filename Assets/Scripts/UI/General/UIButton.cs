using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private TextMeshProUGUI _button_text;
    [SerializeField] private RectTransform _button_text_transform;

    [SerializeField] private Color _default_color;
    [SerializeField] private Color _hover_color;
    [SerializeField] private Color _pressed_color;

    private Button _button;
    private Vector2 _initial_position;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _initial_position = _button_text_transform.anchoredPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _button_text.color = _hover_color;
        _button_text_transform.anchoredPosition = _initial_position;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _button_text.color = _default_color;
        _button_text_transform.anchoredPosition = _initial_position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _button_text.color = _pressed_color;
        _button_text_transform.anchoredPosition = _initial_position + new Vector2(0, -7);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _button_text.color = _hover_color;
        _button_text_transform.anchoredPosition = _initial_position;
    }
}

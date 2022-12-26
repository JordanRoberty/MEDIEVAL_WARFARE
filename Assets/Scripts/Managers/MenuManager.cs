using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : Singleton<MenuManager>
{
    [SerializeField]
    private Transform root_canvas;
    [SerializeField]
    private Menu initial_menu;

    private Dictionary<MenuId, Menu> menus = new Dictionary<MenuId, Menu>();
    private Stack<Menu> menu_stack = new Stack<Menu>();

    protected override void Awake()
    {
        base.Awake();

        foreach (Transform menu in root_canvas)
        {
            hide_menu(menu.GetComponent<Menu>());
            menus.Add(menu.GetComponent<Menu>().id, menu.GetComponent<Menu>());
        }
    }

    private void Start()
    {
        if (initial_menu != null)
        {
            push_menu(initial_menu);
        }
    }

    

    public void push_menu(Menu new_menu)
    {
        if (menu_stack.Count > 0)
        {
            Menu current_menu = menu_stack.Peek();

            if (!new_menu.is_modal)
            {
                hide_menu(current_menu);
            }
        }

        menu_stack.Push(new_menu);
        display_menu(new_menu);
        EventSystem.current.SetSelectedGameObject(new_menu.first_button_selected);
    }

    public void pop_menu()
    {
        if (menu_stack.Count > 1)
        {
            Menu previous_menu = menu_stack.Pop();
            hide_menu(previous_menu);

            Menu current_menu = menu_stack.Peek();
            
            display_menu(current_menu);
            
        }
        else
        {
            Debug.LogWarning("Trying to pop a menu but only 1 remains in the stack!");
        }
    }

    private void hide_menu(Menu menu)
    {
        menu.gameObject.SetActive(false);
    }

    private void display_menu(Menu menu)
    {
        menu.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(menu.first_button_selected);
    }
}

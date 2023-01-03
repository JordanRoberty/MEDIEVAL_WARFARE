using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Menu : MonoBehaviour
{
    [SerializeField]
    public MenuState id;
    [SerializeField]
    public bool is_modal = false;
    [SerializeField]
    public GameObject first_button_selected;

    [SerializeField]
    private UnityEvent pre_push_action;
    [SerializeField]
    private UnityEvent post_push_action;
    [SerializeField]
    private UnityEvent pre_pop_action;
    [SerializeField]
    private UnityEvent post_pop_action;

    public void enter()
    {
        /*=== PRE PUSH ACTIONS ===*/
        pre_push_action?.Invoke();

        /*====== TRANSITION ======*/

        /*=== POST PUSH ACTIONS ===*/
        post_push_action?.Invoke();
    }

    public void exit()
    {
        /*==== PRE POP ACTIONS ====*/
        pre_pop_action?.Invoke();

        /*====== TRANSITION ======*/

        /*=== POST POP ACTIONS ===*/
        post_pop_action?.Invoke();
    }
}

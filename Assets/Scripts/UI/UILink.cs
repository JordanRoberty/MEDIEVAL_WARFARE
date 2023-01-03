using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UILink : MonoBehaviour
{
    [SerializeField]
    public MenuState target;

    private Button link_button;

    private void Start()
    {
        link_button = GetComponent<Button>();
        link_button.onClick.AddListener(handle_click);
    }

    // Update is called once per frame
    private void handle_click()
    {
        MenuUIManager.Instance.set_state(target);
    }
}
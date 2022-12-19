using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MenuLink : MonoBehaviour
{
    [SerializeField]
    public Menu_Id target;

    private Button link_button;

    private void Start()
    {
        link_button = GetComponent<Button>();
        link_button.onClick.AddListener(OnLinkClicked);
    }

    // Update is called once per frame
    private void OnLinkClicked()
    {
        MenuManager.Instance.set_menu(target);
    }
}

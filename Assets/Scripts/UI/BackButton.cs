using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BackButton : MonoBehaviour
{
    private Button back_button;

    private void Start()
    {
        back_button = GetComponent<Button>();
        back_button.onClick.AddListener(handle_click);
    }

    // Update is called once per frame
    private void handle_click()
    {
        MenuManager.Instance.pop_menu();
    }
}

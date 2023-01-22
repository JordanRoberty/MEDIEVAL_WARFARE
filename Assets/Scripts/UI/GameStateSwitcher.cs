using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class GameStateSwitcher : MonoBehaviour
{
    [SerializeField]
    public GameState target;

    private Button switch_button;

    private void Start()
    {
        switch_button = GetComponent<Button>();
        switch_button.onClick.AddListener(handle_click);
    }

    // Update is called once per frame
    private void handle_click()
    {
        GameManager.Instance.set_state(target);
    }
}
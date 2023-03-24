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
        switch_button.onClick.AddListener(() => { GameManager.Instance.set_state(target); });
    }
}
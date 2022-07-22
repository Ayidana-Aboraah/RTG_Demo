using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static PlayerInput input;

	private void Awake() => input = new PlayerInput();

	private void OnEnable() => input.Enable();

	private void OnDisable() => input.Disable();
}

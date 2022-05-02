using UnityEngine;

public class InputManager : MonoBehaviour
{
    public PlayerInput input;
	
    public static InputManager instance;
	
    private void Awake()
	{
		input = new PlayerInput();

		DontDestroyOnLoad(gameObject);

		if(instance == null) instance = this;
		else Destroy(gameObject);
	}

	private void OnEnable()
	{
		input.Enable();
	}

	private void OnDisable()
	{
		input.Disable();
	}
}

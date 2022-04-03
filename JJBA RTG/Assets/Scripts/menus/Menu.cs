using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour
{
	public GameObject menu;
	public bool isOpen;

	#region Player Input
	PlayerInput input;
	private void Awake()
	{
		input = new PlayerInput();
	}

	private void OnEnable()
	{
		input.Enable();
	}

	private void OnDisable()
	{
		input.Disable();
	}
	#endregion

	public virtual void Start()
	{
		input.Menu.Pause.started += _ => Evaluate();
	}

	public virtual void Open()
	{
		Time.timeScale = 0f;
		isOpen = true;
	}

	public virtual void Close()
	{
		Time.timeScale = 1f;
		isOpen = false;
	}

	public virtual void Evaluate()
	{
		if (isOpen) Close();
		else Open();
		
		menu.SetActive(isOpen);
	}

	public void LoadScene(int idx)
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene(idx);
	}
	
	public void QuitGame()
	{
		Application.Quit();
	}
}

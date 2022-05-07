using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour
{
	public GameObject menu;

	internal virtual void Start()
	{
		FindObjectOfType<InputManager>().input.Menu.Pause.started += _ => Evaluate();
		menu.SetActive(false);
	}

	public virtual void Open()
	{
		Time.timeScale = 0f;
		menu.SetActive(true);
	}

	public virtual void Close()
	{
		Time.timeScale = 1f;
		menu.SetActive(false);
	}

	public virtual void Evaluate()
	{
		if (menu.activeSelf) Close();
		else Open();
	}

	public void LoadScene(int idx)
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene(idx);
	}

	public void LoadLockedScene(int scene){
		if (PrototypeProgression.CheckBit(scene))
			SceneManager.LoadScene(scene);
	}
	
	public void QuitGame()
	{
		Application.Quit();
	}
}
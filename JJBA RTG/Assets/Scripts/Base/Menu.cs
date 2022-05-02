using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour
{
	public GameObject menu;
	public bool isOpen;

	internal virtual void Start()
	{
		FindObjectOfType<InputManager>().input.Menu.Pause.started += _ => Evaluate();
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

	public void LoadLockedScene(int scene){
		if ((PrototypeProgression.completion & (1 << scene)) > 0) //Check bit position using the scene to select the position
            SceneManager.LoadScene(scene);
	}
	
	public void QuitGame()
	{
		Application.Quit();
	}
}

[System.Serializable]
public static class PrototypeProgression
{
    public static byte completion;

    public static void Completed(byte boss){
        completion |= boss;
    }
}
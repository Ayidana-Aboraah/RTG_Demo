using UnityEngine;

public class Action_Wheel : MonoBehaviour
{
    public GameObject wheel;
    void Start() =>
        InputManager.input.Menu.Wheel.started += _ => {
            wheel.SetActive(!wheel.activeSelf);
            if (wheel.activeSelf){
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
            }else{
               Cursor.lockState = CursorLockMode.Locked;
               Time.timeScale = 1; 
            }
        };
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(CanvasGroup))]
public class PauseMenuToggle : MonoBehaviour
{
    
    [SerializeField] GameObject Panel;
    private float curScale = 1f;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {

            TogglePanel();
            
            curScale = (curScale + 1f) % 2;
            Time.timeScale = curScale;

        }
    }

    private void TogglePanel()
    {

        Panel.SetActive(!Panel.activeSelf);

    }
}


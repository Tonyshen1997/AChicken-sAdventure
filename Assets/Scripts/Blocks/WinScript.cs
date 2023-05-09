using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour
{
    [SerializeField] GameObject winPanel;

    private void Update()
    {
        if (winPanel == null)
        {
            GameObject canvas = GameObject.Find("Canvas");
            winPanel = canvas.transform.Find("WinPanel").gameObject;
        }
        
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            toggleWinPanel();
        }
    }

    private void toggleWinPanel()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}

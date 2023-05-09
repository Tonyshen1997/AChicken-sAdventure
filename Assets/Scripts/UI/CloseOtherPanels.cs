using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseOtherPanels : MonoBehaviour
{
    [SerializeField] GameObject[] AnyPanel;
    //[SerializeField] GameObject panel;

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf == true)
        {
            TogglePanel();
        }
    }

    private void TogglePanel()
    {
        foreach (GameObject panel in AnyPanel)
        {
            panel.SetActive(false);
        }

    }

    //panel.SetActive(false);
}

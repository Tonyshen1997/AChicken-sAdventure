using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] float timeDuration = 1f;
    [SerializeField] GameObject text;
    [SerializeField] GameObject nextText;
    [SerializeField] GameObject player;
    private float timer;
    private bool IsIntro = false;

    // Start is called before the first frame update
    void Start()
    {
        timer = timeDuration;
        
    }

    private void Update()
    {
        PlayerScript playerController = player.GetComponent<PlayerScript>();
        if (playerController.lastSpawnName == "nothing") IsIntro = true;
        if (IsIntro && text.activeSelf)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                ToggleText();
            }
        }
    }

    private void ToggleText()
    {
        text.SetActive(!text.activeSelf);
        if (nextText != null)
        {
            nextText.SetActive(!nextText.activeSelf);
        }
        else
        {
            GameObject panel = gameObject.transform.parent.gameObject;
            panel.SetActive(!panel.activeSelf); 
        }
    }


}

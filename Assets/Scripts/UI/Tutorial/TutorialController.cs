using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] float timeDuration = 1f;

    [SerializeField] GameObject player;
    [SerializeField] GameObject[] texts;
    private float timer;
    private bool IsIntro = false;
    private int curText = 0;
   
    // Start is called before the first frame update
    void Start()
    {
        PlayerScript playerController = player.GetComponent<PlayerScript>();
        if (playerController.lastSpawnName == "CheckPoint") IsIntro = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsIntro) gameObject.SetActive(false);

        if (!IsIntro) return;

        timer += Time.deltaTime;
        if (timer >= timeDuration)
        {
            texts[curText].SetActive(false);
            timer = 0;
            curText++;
            if (curText == texts.Length) IsIntro = false;
        } else
        {
            texts[curText].SetActive(true);
        }

    }
}

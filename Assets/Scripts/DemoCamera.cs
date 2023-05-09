using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoCamera : MonoBehaviour
{
    public GameObject player;
    private PlayerScript playerController;
    private Vector3 offset;
    private bool follow = true;
    [SerializeField] Transform CatapultCutSceneCamPosition;
    

    void Start()
    {
        playerController = player.GetComponent<PlayerScript>();
        MoveToPlayer();
    }

    public void MoveToPlayer()
    {
        offset = new Vector3(-0.1f, 5.3f, -2.2f);
        if (SceneManager.GetActiveScene().name == "Level2" || SceneManager.GetActiveScene().name == "Level2Demo")
        {
            offset *= 3;
        }

        if (playerController.lastSpawnName == "MySpawnPoint (7)") ViewChange(1.5f);

        transform.position = player.transform.position + offset;
        follow = true;
        Time.timeScale = 1f;
    }

    public void MoveToPosition(Transform camPos)
    {
        Time.timeScale = 0f;
        follow = false;
        transform.position = camPos.position;
    }

    public void ViewChange(float magnitude)
    {
        offset *= magnitude;
        transform.position = player.transform.position + offset;
    }

    private void LateUpdate()
    {
        if (follow)
        {
            transform.position = player.transform.position + offset;
        }
        
    }
}

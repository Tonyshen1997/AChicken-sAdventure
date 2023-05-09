using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCollector : MonoBehaviour
{
    private int _coinCount = 0;
    public TextMeshProUGUI scoreText;
    public AudioSource coinSound;

    private void Start()
    {
        UpdateScoreText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            _coinCount++;
            UpdateScoreText();
            coinSound.Play();
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + _coinCount;
    }
}

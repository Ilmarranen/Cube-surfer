using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool isActive = false;
    public float speed = 20;
    [SerializeField] TextMeshProUGUI startText;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] ParticleSystem warpEffect;

    // Start is called before the first frame update
    void Start()
    {
        //Double check
        gameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //If we click when game is inactive and not after game over - we start the game
        if (Input.GetMouseButtonDown(0) && !isActive && !gameOverPanel.activeSelf)
        {
            isActive = true;
            warpEffect.Play();
            startText.gameObject.SetActive(false);
        }
    }

    //Actions required on game over
    public void GameOver()
    {
        isActive = false;
        //Stop special effects
        warpEffect.Stop();
        //Show game over UI
        gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        //Reload current scene (as we only have one)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

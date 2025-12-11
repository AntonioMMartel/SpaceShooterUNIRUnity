using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverCanvas;
    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        gameOverCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            if (Input.anyKeyDown) // Me da pereza poderosa hacerlo con el InputSystem (lo siento jefe!)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                gameOver = false;
                gameOverCanvas.SetActive(false);
                Time.timeScale = 1f;
            }
        }

    }

    public void GameIsOver()
    {
        gameOver = true;
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0f;
    }
}

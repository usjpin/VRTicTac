using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] buttonList;

    public GameObject gameOverPanel;
    public GameObject gameOverText;
    public GameObject playAgainPanel;
    public GameObject difficultyPanel;
    public GameObject difficultyText;

    private string playerSide;
    private int moveCount;
    private bool gameOver;

    private OpponentAI computer;
    private bool hardDifficulty;

    private void Awake()
    {
        SetGameControllerReferenceOnButtons();
        computer = new OpponentAI();
        hardDifficulty = false;
        RestartGame();
    }

    private void SetGameControllerReferenceOnButtons()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<ButtonComponent>().SetGameControllerReference(this);
        }
    }

    public string GetPlayerSide()
    {
        return playerSide;
    }

    public string GetText(GameObject gameObject)
    {
        return gameObject.GetComponent<TextMesh>().text;
    }

    private void ChangeSides()
    {
        playerSide = (playerSide == "X") ? "O" : "X";
    }

    public bool CheckWin()
    {
        if ((GetText(buttonList[0]) == playerSide && GetText(buttonList[1]) == playerSide && GetText(buttonList[2]) == playerSide) ||
            (GetText(buttonList[3]) == playerSide && GetText(buttonList[4]) == playerSide && GetText(buttonList[5]) == playerSide) ||
            (GetText(buttonList[6]) == playerSide && GetText(buttonList[7]) == playerSide && GetText(buttonList[8]) == playerSide) ||
            (GetText(buttonList[0]) == playerSide && GetText(buttonList[3]) == playerSide && GetText(buttonList[6]) == playerSide) ||
            (GetText(buttonList[1]) == playerSide && GetText(buttonList[4]) == playerSide && GetText(buttonList[7]) == playerSide) ||
            (GetText(buttonList[2]) == playerSide && GetText(buttonList[5]) == playerSide && GetText(buttonList[8]) == playerSide) ||
            (GetText(buttonList[0]) == playerSide && GetText(buttonList[4]) == playerSide && GetText(buttonList[8]) == playerSide) ||
            (GetText(buttonList[2]) == playerSide && GetText(buttonList[4]) == playerSide && GetText(buttonList[6]) == playerSide))
        {
            return true;
        }
        return false;
    }

    public void EndTurn()
    {
        if (gameOver) return;
        if (CheckWin())
        {
            GameOver(false);
        }
        moveCount++;
        if (moveCount == 9)
        {
            GameOver(true);
        }
        ChangeSides();
        OpponentMove();
    }

    private void OpponentMove()
    {
        if (gameOver) return;
        if (hardDifficulty)
        {
            buttonList[computer.GetMinimaxMove(buttonList)].GetComponentInParent<ButtonComponent>().SetSpace();
        }
        else
        {
            buttonList[computer.GetRandomMove(buttonList)].GetComponentInParent<ButtonComponent>().SetSpace();
        }
        if (CheckWin())
        {
            GameOver(false);
        }
        moveCount++;
        if (moveCount == 9)
        {
            GameOver(true);
        }
        ChangeSides();
    }

    private void GameOver(bool tie)
    {
        gameOver = true;
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<ButtonComponent>().StopInteraction();
        }
        if(tie)
        {
            gameOverText.GetComponent<TextMesh>().text = "It's a tie";
        }
        else
        {
            gameOverText.GetComponent<TextMesh>().text = playerSide + " wins!";
        }
        gameOverPanel.SetActive(true);
        playAgainPanel.SetActive(true);
        difficultyPanel.SetActive(true);
    }

    public void RestartGame()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<ButtonComponent>().ResetSpace();
        }
        moveCount = 0;
        playerSide = "X";
        gameOverPanel.SetActive(false);
        playAgainPanel.SetActive(false);
        difficultyPanel.SetActive(false);
        gameOver = false;
    }

    public void ToggleDifficulty()
    {
        hardDifficulty = !hardDifficulty;
        if (hardDifficulty)
        {
            difficultyText.GetComponent<TextMesh>().text = "Hard";
        }
        else
        {
            difficultyText.GetComponent<TextMesh>().text = "Easy";
        }
    }
}

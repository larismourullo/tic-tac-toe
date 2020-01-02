using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private const string userPlayer = "X";
    private const string computerPlayer = "O";
    private System.Random random;
    private string playerSide;
    private int count;
    public Text[] buttonList;
    public Text gameOverText;
    public GameObject restartButton;
    
    public GameController()
    {
        random = new System.Random();
    }

    private void Awake()
    {
        SetGameControllerReferenceOnButtons();
        playerSide = userPlayer;
        count = 0;
        restartButton.SetActive(false);
    }

    private void SetGameControllerReferenceOnButtons()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);
        }
    }

    public string GetPlayerSide()
    {
        return playerSide;
    }

    public void EndTurn()
    {
        count++;

        if (((buttonList[0].text == playerSide) && (buttonList[1].text == playerSide) && (buttonList[2].text == playerSide)) ||
             ((buttonList[3].text == playerSide) && (buttonList[4].text == playerSide) && (buttonList[5].text == playerSide)) ||
             ((buttonList[6].text == playerSide) && (buttonList[7].text == playerSide) && (buttonList[8].text == playerSide)) ||
             ((buttonList[0].text == playerSide) && (buttonList[3].text == playerSide) && (buttonList[6].text == playerSide)) ||
             ((buttonList[1].text == playerSide) && (buttonList[4].text == playerSide) && (buttonList[7].text == playerSide)) ||
             ((buttonList[2].text == playerSide) && (buttonList[5].text == playerSide) && (buttonList[8].text == playerSide)) ||
             ((buttonList[0].text == playerSide) && (buttonList[4].text == playerSide) && (buttonList[8].text == playerSide)) ||
             ((buttonList[2].text == playerSide) && (buttonList[4].text == playerSide) && (buttonList[6].text == playerSide)))
        {
            GameOver(playerSide);
            return;
        }
        else if (count > 8)
        {
            GameOver("Empate");
            return;
        }

        ChangeSides();

        if (playerSide == computerPlayer)
        {
            ComputerThink();
        }
    }

    private void ChangeSides()
    {
        playerSide = (playerSide == userPlayer) ? computerPlayer : userPlayer;
    }

    private void GameOver(string winningPlayer)
    {
        SetBoardInteractable(false);

        if (winningPlayer == "Empate")
        {
            SetGameOverText("Empate!");
        }
        else
        {
            SetGameOverText(playerSide + " Ganhou!");
        }

        restartButton.SetActive(true);
    }

    public void RestartGame()
    {
        playerSide = userPlayer;
        count = 0;

        SetGameOverText("");
        SetBoardInteractable(true);

        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].text = "";
        }

        restartButton.SetActive(false);
    }

    private void SetBoardInteractable(bool toogle)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = toogle;
        }
    }

    private void SetGameOverText(string valor)
    {
        gameOverText.text = valor;
    }

    public int VerifyWin(string player)
    {
        
        if ((buttonList[3].text == player && CurrentPlayerCanCheck(0) && buttonList[6].text == player) ||
            (buttonList[1].text == player && CurrentPlayerCanCheck(0) && buttonList[2].text == player) ||
            (buttonList[4].text == player && CurrentPlayerCanCheck(0) && buttonList[8].text == player))
        {
            return 0;
        }
            
        if ((buttonList[0].text == player && CurrentPlayerCanCheck(1) && buttonList[2].text == player) ||
            (buttonList[4].text == player && CurrentPlayerCanCheck(1) && buttonList[7].text == player))
        {
            return 1;
        }

        if ((buttonList[0].text == player && CurrentPlayerCanCheck(2) && buttonList[1].text == player) ||
            (buttonList[5].text == player && CurrentPlayerCanCheck(2) && buttonList[8].text == player) ||
            (buttonList[4].text == player && CurrentPlayerCanCheck(2) && buttonList[6].text == player))
        {
            return 2;
        }

        if ((buttonList[4].text == player && CurrentPlayerCanCheck(3) && buttonList[5].text == player) ||
            (buttonList[0].text == player && CurrentPlayerCanCheck(3) && buttonList[6].text == player))
        {
            return 3;
        }

        if ((buttonList[3].text == player && CurrentPlayerCanCheck(4) && buttonList[5].text == player) ||
            (buttonList[1].text == player && CurrentPlayerCanCheck(4) && buttonList[7].text == player) ||
            (buttonList[0].text == player && CurrentPlayerCanCheck(4) && buttonList[8].text == player) ||
            (buttonList[2].text == player && CurrentPlayerCanCheck(4) && buttonList[6].text == player))
        {
            return 4;
        }

        if ((buttonList[2].text == player && CurrentPlayerCanCheck(5) && buttonList[8].text == player) ||
            (buttonList[3].text == player && CurrentPlayerCanCheck(5) && buttonList[4].text == player))
        {
            return 5;
        }

        if ((buttonList[0].text == player && CurrentPlayerCanCheck(6) && buttonList[3].text == player) ||
            (buttonList[7].text == player && CurrentPlayerCanCheck(6) && buttonList[8].text == player) ||
            (buttonList[4].text == player && CurrentPlayerCanCheck(6) && buttonList[2].text == player))
        {
            return 6;
        }

        if ((buttonList[1].text == player && CurrentPlayerCanCheck(7) && buttonList[4].text == player) ||
            (buttonList[6].text == player && CurrentPlayerCanCheck(7) && buttonList[8].text == player))
        {
            return 7;
        }

        if ((buttonList[2].text == player && CurrentPlayerCanCheck(8) && buttonList[5].text == player) ||
            (buttonList[6].text == player && CurrentPlayerCanCheck(8) && buttonList[7].text == player) ||
            (buttonList[0].text == player && CurrentPlayerCanCheck(8) && buttonList[4].text == player))
        {
            return 8;
        }

        return 10;       
    }

    private bool IsFirstPlay()
    {
        return count == 1;
    }

    public void ComputerThink()
    {
        if (IsFirstPlay() &&
            (buttonList[0].text == userPlayer ||
            buttonList[2].text == userPlayer ||
            buttonList[6].text == userPlayer ||
            buttonList[8].text == userPlayer) &&
            CurrentPlayerCanCheck(4))
        {
            CheckButtonForCurrentPlayer(4);
            EndTurn();
            return;
        }

        if (IsFirstPlay() &&
            (buttonList[1].text == userPlayer ||
            buttonList[3].text == userPlayer ||
            buttonList[5].text == userPlayer ||
            buttonList[7].text == userPlayer))
        {
            CheckRandomButton(new int[5] { 0, 2, 4, 6, 8 });
            EndTurn();
            return;
        }

        if (IsFirstPlay() &&
            buttonList[4].text == userPlayer)
        {
            CheckRandomButton(new int[4] { 0, 2, 6, 8 });
            EndTurn();
            return;
        }

        // Verifica se o computador pode ganhar na próxima rodada
        int verifyComputer = VerifyWin(computerPlayer);

        if (verifyComputer != 10)
        {
            CheckButtonForCurrentPlayer(verifyComputer);
            EndTurn();
            return;
        }

        // Verifica se o usuário pode ganhar na próxima rodada
        int verifyUser = VerifyWin(userPlayer);

        if (verifyUser != 10)
        {
            CheckButtonForCurrentPlayer(verifyUser);
            EndTurn();
            return;
        }

        CheckRandomButton(new int[9] { 0, 1, 2, 3, 4, 5, 6, 7, 8 });
        EndTurn();
    }

    private void CheckRandomButton(int[] buttonIndexes)
    {
        int randomIndex = random.Next(0, buttonIndexes.Length);

        while (!CurrentPlayerCanCheck(randomIndex))
        {
            randomIndex = random.Next(0, buttonIndexes.Length);
        }

        CheckButtonForCurrentPlayer(randomIndex);
    }

    private void CheckButtonForCurrentPlayer(int i)
    {
        Color blueColor = new Color();
        ColorUtility.TryParseHtmlString(htmlString: "#16BFBC", color: out blueColor);
        buttonList[i].color = blueColor;
        buttonList[i].text = computerPlayer;
        buttonList[i].GetComponentInParent<Button>().interactable = false;
    }

    private bool CurrentPlayerCanCheck(int i)
    {
        return string.IsNullOrEmpty(buttonList[i].text);
    }
}

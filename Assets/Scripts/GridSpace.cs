using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour {
    public Button button;
    public Text buttonText;
    private GameController gameController;

    public void SetGameControllerReference(GameController controller)
    {
        gameController = controller;
    }

    public void SetSpace()
    {
        buttonText.text = gameController.GetPlayerSide();

        if (gameController.GetPlayerSide() == "X")
        {
            Color redColor = new Color();
            ColorUtility.TryParseHtmlString(htmlString: "#CC362E", color: out redColor);
            buttonText.color = redColor;
        }

        button.interactable = false;
        gameController.EndTurn();
    }    

}

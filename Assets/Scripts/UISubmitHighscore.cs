using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISubmitHighscore : MonoBehaviour {
    [SerializeField]
    HighScoresGateway gateway;

    [SerializeField]
    InputField playerName;

    [SerializeField]
    Button btn;

    string refName = "Local Minimum";

    public void OnNameChange()
    {
        playerName.text = gateway.SecureName(playerName.text, refName.Length);
        btn.interactable = playerName.text.Trim().Length > 0;
    }

    public void Submit()
    {

    }
}

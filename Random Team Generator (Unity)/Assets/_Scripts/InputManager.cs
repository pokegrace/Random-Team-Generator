using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

// this script is used to handle button onclick events and input fields
public class InputManager : MonoBehaviour {

	// public variables
	public InputField teamNumField;
    public InputField playerNumField;
	public InputField teamNameField;
    public InputField playerNameField;
	public Text infoText;
    public Color teamColor;
    public Color playerColor;
    public int teamNumber = 0;
    public int playerNumber = 0;

    public Button numberButton;

    public bool teamFull = false;
    public bool playerFull = false;

    // private variables
	private string teamName;
    private string playerName;
	private int currentTeamNum = 0;
    private int currentPlayerNum = 0;
    
    // to actually pass text into the scroll list
    [SerializeField]
    private TextManager textManager;

    // event trigger for number fields
    public void OnNumFieldClick()
    {
        TouchScreenKeyboard.Open("", TouchScreenKeyboardType.NumberPad, true, false, false, true);
    }

    // event trigger for name fields
    public void OnNameFieldClick()
    {
        TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, true, false, false, true);
    }

    public void numberSubmit()
    {
        // converting text entry to int and storing in variable
        teamNumber = Convert.ToInt32(teamNumField.text);
        playerNumber = Convert.ToInt32(playerNumField.text);

        // if there are less players than teams
        if (teamNumber > playerNumber)
            infoText.text = "You cannot have more teams than players.";
		else
        {
			infoText.text = "You have " + teamNumber + " teams and " + playerNumber + " players!";
            // disable number button after first confirmation
            teamNumField.interactable = false;
            playerNumField.interactable = false;
        }
	}
    
	public void teamNameSubmit()
	{
        // if number input fields are empty
        if (teamNumber == 0 || teamNumField.text == "" || playerNumber == 0 || playerNumField.text == "")
        {
            infoText.text = "Please enter how many teams and players.";
        }
        // if user hasn't inputted a name into the input field
        else if (currentTeamNum < teamNumber && teamNameField.text == "")
        {
            infoText.text = "Please enter a team name.";
        }
        // if user hasn't yet exceeded the inputted team number
        else if (currentTeamNum < teamNumber && teamNameField.text != "")
        {
            teamName = teamNameField.text;
            infoText.text = "Team " + (currentTeamNum + 1) + ": " + teamName;

            // clearing input field
            teamNameField.Select();
            teamNameField.text = "";

            // sending teamName to scroll view
            textManager.LogTeam(teamName, teamColor, false);

            currentTeamNum++;
            if (currentTeamNum >= teamNumber)
            {
                teamNameField.interactable = false;
                teamFull = true;
            }
        }
        else
        {
            infoText.text = "You have no more teams to name!";
        }
	}

    public void playerNameSubmit()
    {
        // if number input fields are empty
        if (teamNumber == 0 || teamNumField.text == "" || playerNumber == 0 || playerNumField.text == "")
        {
            infoText.text = "Please enter how many teams and players.";
        }
        // if user hasn't inputted a name into the input field
        else if (currentPlayerNum < playerNumber && playerNameField.text == "")
        {
            infoText.text = "Please enter a player name.";
        }
        // if user hasn't yet exceeded the inputted team number
        else if (currentPlayerNum < playerNumber && playerNumField.text != "")
        {
            playerName = playerNameField.text;
            infoText.text = "Player " + (currentPlayerNum + 1) + ": " + playerName;

            // clearing input field
            playerNameField.Select();
            playerNameField.text = "";

            // sending playerName to scroll view
            textManager.LogPlayer(playerName, playerColor, false);

            currentPlayerNum++;
            if(currentPlayerNum >= playerNumber)
            {
                playerNameField.interactable = false;
                playerFull = true;
            }
        }
        else
        {
            infoText.text = "You have no more players to name!";
        }
    }
    
    public void restartScreen()
    {
        SceneManager.LoadScene("Main");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

// script to shuffle the lists and redistribute into teams
// TODO: create reshuffle function
public class TeamGenerator : MonoBehaviour {
    
    [SerializeField] private InputManager inputManager;
    [SerializeField] private TextManager textManager;
    [SerializeField] private GameObject textTemplate;
    [SerializeField] private GameObject teamListContent;
    [SerializeField] private Button shuffleButton;

    private Queue<string> shuffledPlayers;
    private List<string> finalizedList;

    // creating random number generator
    private System.Random r = new System.Random();

    private void Start()
    {
        shuffledPlayers = new Queue<string>();
        finalizedList = new List<string>();
    }

    public void shufflePlayers()
    {
        // only continue with code if teams and players are full
        if (!inputManager.teamFull)
            inputManager.infoText.text = "Please finish naming your teams.";
        else if (!inputManager.playerFull)
            inputManager.infoText.text = "Please finish naming your players.";
        else
        {
            int totalPlayers = textManager.stringPlayers.Count;
            for(int i = 0; i < totalPlayers; ++i)
            {
                int index = r.Next(textManager.stringPlayers.Count - 1);

                shuffledPlayers.Enqueue(textManager.stringPlayers[index].ToString());
                textManager.stringPlayers.RemoveAt(index);
            }
            OrganizeLists();
            shuffleButton.interactable = false;
        }
    }

    // setup team/players to print
    private void OrganizeLists()
    {
        int numTeams = textManager.stringTeams.Count;
        // each list will have team as head and players as following elements
        List<string>[] lists = new List<string>[numTeams];

        // add team name as head of each list
        for(int i = 0; i < numTeams; ++i)
        {
            lists[i] = new List<string>();
            lists[i].Add(textManager.stringTeams[i].ToString());
        }

        // add players to each list
        int j = 0;
        while(shuffledPlayers.Count > 0)
        {
            lists[j].Add(shuffledPlayers.Dequeue());

            // iterating through lists
            if (j < numTeams - 1)
                ++j;
            else
                j = 0;
        }

        // add lists together, then call PrintList()
        for (int k = 0; k < lists.Length; ++k)
        {
            finalizedList.AddRange(lists[k]);
        }

        PrintList();
    }

    private void PrintList()
    {
        // clearing viewport text
        foreach(Transform t in teamListContent.transform)
        {
            Destroy(t.gameObject);
        }

        // i to track team name to color
        int i = 0;
        // instantiate text objects into scroll rect
        foreach(string s in finalizedList)
        {
            if(i < textManager.stringTeams.Count && s.Equals(textManager.stringTeams[i]))
            {
                textManager.LogTeam(s, inputManager.teamColor, true);
                ++i;
            }
            else
            {
                textManager.LogPlayer(s, inputManager.playerColor, true);
            }
        }

        inputManager.infoText.text = "Here are your teams!";
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// from c00pala on youtube; Scroll Menu Pt 4
public class TextManager : MonoBehaviour {

	[SerializeField] private GameObject textTemplate;
	[SerializeField] private GameObject teamListContent;

    public ArrayList stringTeams;
    public ArrayList stringPlayers;

    void Start()
    {
        stringTeams = new ArrayList();
        stringPlayers = new ArrayList();
    }

    public void LogTeam(string textString, Color textColor, bool printing)
    {
        GameObject teamText = Instantiate(textTemplate, teamListContent.transform);

        teamText.GetComponent<TextLogItem>().SetText(textString, textColor);

        // adding text to our string array if name is inputted for first time
        if (!printing)
            stringTeams.Add(textString);
    }

    public void LogPlayer(string textString, Color textColor, bool printing)
    {
        GameObject playerText = Instantiate(textTemplate, teamListContent.transform);

        playerText.GetComponent<TextLogItem>().SetText(textString, textColor);

        // adding text to our string array if name is inputted for first time
        if (!printing)
            stringPlayers.Add(textString);
    }
}
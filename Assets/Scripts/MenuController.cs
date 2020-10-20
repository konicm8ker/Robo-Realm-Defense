using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    [SerializeField] Button playButton = null;
    [SerializeField] Button helpButton = null;
    [SerializeField] Button creditsButton = null;
    [SerializeField] GameObject backButton = null;
    [SerializeField] Image textPanel = null;
    [SerializeField] Text helpText = null;
    [SerializeField] Text creditsText = null;

    void Start()
    {
        // Button Index: Play(0), Help(1), Credits(2), Back(3)

        // Play button clicked
        playButton.onClick.AddListener(StartGame);
        // Help button clicked
        helpButton.onClick.AddListener(() => ShowTextDisplay(1));
        // Credits button clicked
        creditsButton.onClick.AddListener(() => ShowTextDisplay(2));
        // Back button clicked
        backButton.GetComponent<Button>().onClick.AddListener(HideTextDisplay);
    }

    private void StartGame()
    {
        // Load the game level
        SceneManager.LoadScene(1);
    }

    private void ShowTextDisplay(int index)
    {
        // Show text panel with help text
        textPanel.enabled = true;
        if(index == 1) { helpText.enabled = true; }
        if(index == 2) { creditsText.enabled = true; }
        // Show back button
        backButton.SetActive(true);
        // Disable menu buttons
        MenuButtonsInteraction(playButton, false);
        MenuButtonsInteraction(helpButton, false);
        MenuButtonsInteraction(creditsButton, false);
        return;
        
    }

    private void HideTextDisplay()
    {
        // Hide text panel with text
        textPanel.enabled = false;
        helpText.enabled = false;
        creditsText.enabled = false;
        // Hide back button
        backButton.SetActive(false);
        // Enable menu buttons
        MenuButtonsInteraction(playButton, true);
        MenuButtonsInteraction(helpButton, true);
        MenuButtonsInteraction(creditsButton, true);
        return;
    }

    private void MenuButtonsInteraction(Button button, bool value)
    {
        button.interactable = value;
        return;
    }

}

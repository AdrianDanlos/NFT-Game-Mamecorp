using UnityEngine;

public class ChooseFirstFighter : MonoBehaviour
{
    // Handles UI buttons
    public GameObject fighterNameInput;

    // scripts
    ChooseFirstFighterUI chooseFirstFighterUI;

    private void Awake()
    {
        chooseFirstFighterUI = GameObject.Find("UIManager").GetComponent<ChooseFirstFighterUI>();
    }

    public void OnSelectFighter()
    {
        switch (transform.name)
        {
            case "Container_Fighter_Left":
                SetFighter(transform.Find("Fighter_Left").GetComponent<FighterSkinData>());
                chooseFirstFighterUI.EnableLeftFighterHighlight();
                break;
            case "Container_Fighter_Mid":
                SetFighter(transform.Find("Fighter_Mid").GetComponent<FighterSkinData>());
                chooseFirstFighterUI.EnableMidFighterHighlight();
                break;
            case "Container_Fighter_Right":
                SetFighter(transform.Find("Fighter_Right").GetComponent<FighterSkinData>());
                chooseFirstFighterUI.EnableRightFighterHighlight();
                break;
        }
    }

    public void MoveToNextState()
    {
        switch (FirstPlayTempData.state.ToString())
        {
            case "FIGHTER":
                chooseFirstFighterUI.ChooseFighter();
                break;
            case "NAME":
                chooseFirstFighterUI.CheckName();
                break;
            case "COUNTRY":
                chooseFirstFighterUI.CheckFlag();
                break;
        }
    }

    public void MoveToPreviousState()
    {
        switch (FirstPlayTempData.state.ToString())
        {
            case "NAME":
                chooseFirstFighterUI.BackToChooseFighter();
                break;
            case "COUNTRY":
                chooseFirstFighterUI.BackToName();
                break;
        }
    }

    private void SetFighter(FighterSkinData fighterSkin)
    {
        FirstPlayTempData.skinName = fighterSkin.skinName;
        FirstPlayTempData.species = fighterSkin.species;
    }

    // used on set focus on input
    public void ResetRegexText()
    {
        chooseFirstFighterUI.regexText.gameObject.SetActive(false);
        chooseFirstFighterUI.regexText.text = "";
    }

    public void GetFlagClicked()
    {
        // turn off check of last flag
        // turn on check on this flag
        FirstPlayTempData.countryFlag = transform.name;
    }
}

using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ControllGameMap : MonoBehaviour
{
    public ViewGameMap view;
    private InputField attackTank, defendTank, moveTank, deployTank;
    private Dropdown card1, card2, card3;
    public ModelGameMap model;
    private StateControl state;

     private void Awake()//prepara i componenti da cui prendere gli input
    {
        //InputField
        attackTank = GameObject.Find("InputFieldAttackTank").GetComponent<InputField>();
        defendTank = GameObject.Find("InputFieldDefendTank").GetComponent<InputField>();
        moveTank = GameObject.Find("InputFieldMoveTank").GetComponent<InputField>();
        deployTank = GameObject.Find("InputFieldDeployTank").GetComponent<InputField>();

        //Dropdown
        card1 = GameObject.Find("DropdownCard1").GetComponent<Dropdown>();
        card2 = GameObject.Find("DropdownCard2").GetComponent<Dropdown>();
        card3 = GameObject.Find("DropdownCard3").GetComponent<Dropdown>();
    }

    public int getDeployTank()
    {
        return int.Parse(deployTank.text);
    }

    public int getAttackTank()
    {
        return int.Parse(attackTank.text);
    }

    // metodi dei button

    public void onClickDeploy()
    {
        List<string> error = state.getMissingData();

        if (error.Count != 0)
        {
            // PopupError
        }
        else
        {
            string errorAction = state.action();
            if(errorAction.Equals(""))
            {
                // Aggiorna i dati dell'interfaccia
            }
            else
            {
                // PopupError
            }
        }
    }

    public void onClickAttack()
    {
        model.setTankAttacker(attackTank.text);
        state.action();
    }

    public void onClickMove()
    {
        model.move(moveTank.text);
    }

    public void onClickNextPhase()
    {
        state = state.nextPhase();
    }

    public void onClickDefend()
    {
        model.startBattle(defendTank.text);
    }

    public void onClickPassTurn()
    {
        model.pass();
    }

    public void onClickQuit()
    {
        model.quit();
    }

    public void onClickExit()
    {
        model.exit();
    }

    public void onClickClosePopup()
    {
        model.closePopup();
    }

    public void onClickShowCards()
    {
        model.showCards();
    }

    public void onClickUseCard()
    {
        model.useCards(card1.options[card1.value].text,
                        card2.options[card2.value].text,
                        card3.options[card3.value].text);
    }

    public void onClickCloseCard()
    {
        model.closeCard();
    }

    public void showError(string error)
    {
        view.showError(error);
    }

    public void updateLogEvent(List<string> message)
    {
        view.updateLogEvent(message);
    }

    public void updateTextPlayerData(string data)
    {
        view.updateTextPlayerData(data);
    }

    public void updateDeployRemain(int countStartDeploy)
    {
        view.updateDeployRemain("" + countStartDeploy);
    }

    public void updatePhase(string message)
    {
        view.updatePhase(message);
    }

    public void updateLandText(string data)
    {
        view.updateLandText(data);
    }

    public void updateSelected(int nSelected, string firstLand, string secondLand)
    {
        if (nSelected == 1)
            view.updateSingleSelected(firstLand);
        else if (nSelected == 2)
            view.updateTwoSelected(firstLand, secondLand);
        else
            return;
            
    }

    public void prepareView()
    {
        view.prepareView();
    }

    public void drawMap(List<StateData> data)
    {
        view.drawMap(data);
    }

    public void changeCanvasOption(string phase)
    {
        view.changeCanvasOption(phase);
    }

    public void showCards(List<string> cards)
    {
        view.showCards(cards);
    }

    public void handleButtonClicked(string buttonClicked)
    {
        if (buttonClicked.Equals("Quit"))
            view.showConfirmQuit();
        else if (buttonClicked.Equals("Exit"))
            Debug.Log("Do something");
        else if (buttonClicked.Equals("Popup"))
            view.closePopup();
        else if (buttonClicked.Equals("Card"))
            view.closeCard();
        else
            return;
    }
}
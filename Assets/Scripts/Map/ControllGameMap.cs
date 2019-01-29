using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ControllGameMap : MonoBehaviour
{

    private InputField attackTank, defendTank, moveTank, deployTank;
    private Dropdown card1, card2, card3;
    public ModelGameMap model;

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

    // metodi dei button

    public void onClickDeploy()
    {
        model.deploy(deployTank.text);
    }

    public void onClickAttack()
    {
        model.setTankAttacker(attackTank.text);
    }

    public void onClickMove()
    {
        model.move(moveTank.text);
    }

    public void onClickNextPhase()
    {
        model.nextPhase();
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
}
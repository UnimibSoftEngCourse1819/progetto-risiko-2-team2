using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ControllGameMap : MonoBehaviour
{

    private InputField attackTank, defendTank, moveTank, deployTank;
    public ModelGameMap model;

     private void Awake()//prepara i componenti da cui prendere gli input
    {
        //InputField
        attackTank = GameObject.Find("InputFieldAttackTank").GetComponent<InputField>();
        defendTank = GameObject.Find("InputFieldDefendTank").GetComponent<InputField>();
        moveTank = GameObject.Find("InputFieldMoveTank").GetComponent<InputField>();
        deployTank = GameObject.Find("InputFieldDeployTank").GetComponent<InputField>();
    }

    // metodi dei button

    public void onClickDeploy()
    {
        model.addTroup(deployTank.text);
    }

    public void onClickAttack()
    {
        model.attack(attackTank.text);
    }

    public void onClickMove()
    {
        model.move(moveTank.text);
    }

    public void onNextPhase()
    {
        model.nextPhase();
    }

    public void onClickDefend()
    {
        model.defend(defendTank);
    }

    public void onClickPassTurn(){
        model.pass();
    }
}
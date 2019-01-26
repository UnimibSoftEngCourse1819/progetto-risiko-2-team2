
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lobby_script : MonoBehaviour
{

    private string nomeUtente;
    private InputField input;
    private NetworkManager net;
  

    private void Awake()
    {
        input = GameObject.Find("InputField").GetComponent<InputField>();
        input.ActivateInputField();
        net = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }
    public void GetNome(string nome)
    {
     
        nomeUtente = nome;
        Debug.Log("nome cambiato "+nomeUtente);
    }
    public void OnMouseDown()
    {
        if(!string.IsNullOrEmpty(nomeUtente)) // chiamo la network manager
        {
            net.Partenza();
            // Debug.Log("il mio nome è "+nomeUtente);
            SceneManager.LoadScene("Waiting_Room");


        }
        else
        {
            Debug.Log("non hai inserito il nome");
        }
    }



}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HuggingFace.API;
using TMPro;
using UnityEngine.Networking;

public class ChatManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public Button sendButton;
    public Transform chatContent;
    public GameObject playerMessagePrefab;
    public GameObject aiMessagePrefab;

    [SerializeField] private string API = "PLease Enter Your API Key Here";

    private void Start()
    {
        sendButton.onClick.AddListener(OnSendButtonClicked);
    }

    private void OnSendButtonClicked()
    {
        string userMessage = inputField.text;
        if (!string.IsNullOrEmpty(userMessage))
        {
            DisplayMessage(playerMessagePrefab, userMessage);
            inputField.text = "";
            SendMessageToAI(userMessage);
        }
    }

    private void DisplayMessage(GameObject prefab, string message)
    {
        GameObject messageObject = Instantiate(prefab, chatContent);
        TextMeshProUGUI messageText = messageObject.GetComponentInChildren<TextMeshProUGUI>();
        messageText.text = message;
    }

    private void SendMessageToAI(string message)
    {
        Message mess = new Message
        {
            inputs = new Inputs
            {
               // past_user_inputs = new List<string> { "Hello!" },
                //generated_responses = new List<string> { "Hi there! How can I assist you today?" },
                text = message
            }
        };
        var jsonData = JsonUtility.ToJson(mess);
        Debug.Log(jsonData);
        UnityWebRequest request = new UnityWebRequest("https://api-inference.huggingface.co/models/microsoft/DialoGPT-large", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + API); 
        
        StartCoroutine(SendRequest(request));
    }
    private IEnumerator SendRequest(UnityWebRequest request)
    {
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error while accessing AI: " + request.error);
            DisplayMessage(aiMessagePrefab,("Error while accessing AI: " + request.error));
        }
        else
        {
           
            //Get Response
            string response = request.downloadHandler.text;
            //View response
            Debug.Log(response);
            OnAIResponse(response);
        }
    }

    private void OnAIResponse(string response)
    {
        DisplayMessage(aiMessagePrefab, response);
    }

    private void OnError(string error)
    {
        Debug.LogError("Ошибка при обращении к AI: " + error);
    }
    
    
}
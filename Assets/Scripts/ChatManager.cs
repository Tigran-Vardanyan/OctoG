using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using HuggingFace.API;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.Networking;

public class ChatManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public Button sendButton;
    public Transform chatContent;
    public GameObject playerMessagePrefab;
    public GameObject aiMessagePrefab;
    public GameObject loadingMessagePrefab;
    public string modelUrl = "https://huggingface.co/facebook/blenderbot-400M-distill";

    [SerializeField] private string API = "PLease Enter Your API Key Here";


    private GameObject loadingIndicator;
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
        loadingIndicator = Instantiate(loadingMessagePrefab, chatContent);
        Inputs mess = new Inputs()
        {
            inputs = message
        };
        var jsonData = JsonUtility.ToJson(mess);
        Debug.Log(jsonData);
        UnityWebRequest request = new UnityWebRequest(modelUrl, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + API); 
        
        StartCoroutine(SendRequest(request));
    }
    private IEnumerator SendRequest(UnityWebRequest request)
    {
        yield return request.SendWebRequest();
        Destroy(loadingIndicator);
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
            List<Answere> answer = JsonConvert.DeserializeObject<List<Answere>>(response);
            
            OnAIResponse(answer?[0].generated_text);
        }
        
    }

    private void OnAIResponse(string response)
    {
        DisplayMessage(aiMessagePrefab, response);
    }
}
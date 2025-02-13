# AI Chat Application

This Unity-based application integrates with Hugging Face's NLP model to facilitate real-time chat interactions between users and an AI.

## Features

- **User Input Handling**: Captures and processes user messages.
- **AI Response Generation**: Utilizes Hugging Face's BlenderBot model to generate AI responses.
- **Dynamic UI Updates**: Displays user and AI messages within the chat interface.
- **Loading Indicators**: Shows a loading message while awaiting AI responses.

## Prerequisites

- **Unity**: Ensure you have Unity installed. This project was developed using Unity version 2022.3.50f1.
- **Hugging Face API Key**: Obtain an API key from [Hugging Face](https://huggingface.co/) to access the model.

## Usage
Start the Application:

Press the Play button in the Unity Editor to run the application.
Interact with the Chat:

Type your message into the input field.
Click the "Send" button to receive a response from the AI.

## Project Structure
Scripts:

- **ChatManager.cs:** Manages user input, sends requests to the AI model, and updates the chat UI.
Inputs.cs and Answere.cs: Define data structures for handling AI interactions.
Prefabs:

- **PlayerMessagePrefab:** UI template for displaying user messages.
- **AIMessagePrefab:** UI template for displaying AI responses.
- **LoadingMessagePrefab:** UI template for displaying a loading indicator during AI processing.

## Acknowledgements
- 1.Hugging Face for providing the BlenderBot model.
- 2.Newtonsoft.Json for JSON parsing.
- 3.TextMeshPro for advanced text rendering in Unity.

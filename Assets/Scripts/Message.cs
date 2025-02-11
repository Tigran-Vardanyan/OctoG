using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Inputs
{
    public List<string> past_user_inputs;
    public List<string> generated_responses;
    public string text;
}

[Serializable]
public class Message
{
    public Inputs inputs;
}
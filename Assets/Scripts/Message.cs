using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Inputs
{
    public List<string> past_user_inputs;
    public List<string> generated_responses;
    public string inputs;
}

[Serializable]
public class Message
{
    public Inputs input;
}
[Serializable]
public class Answere
{
    public string generated_text;
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Scenario
{
    public string king_id { get; set; }
    public string event_id { get; set; }
    public string king_name { get; set; }
    public string events { get; set; }
    public string advise { get; set; }
    public Answer answer { get; set; }
    public Hint hint { get; set; }
    public Reaction reaction { get; set; }
    public Effect effect { get; set; }
}
[Serializable]
public class Answer
{
    public string agree { get; set; }
    public string disagree { get; set; }
}

[Serializable]
public class Hint
{
    public string agree { get; set; }
    public string disagree { get; set; }
}
[Serializable]
public class Reaction
{
    public string agree { get; set; }
    public string disagree { get; set; }
}
[Serializable]
public class Effect
{
    public string agree { get; set; }
    public string disagree { get; set; }
}

[Serializable]
public class Scenarios
{
    public Scenario[] scenarios;
}
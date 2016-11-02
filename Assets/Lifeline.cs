using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lifeline : IXmlNode {

    public List<Method> Methods = new List<Method>();

    public Lifeline(string id, string name)
    {
        this.Id = id;
        this.Name = name;
    }
	
}

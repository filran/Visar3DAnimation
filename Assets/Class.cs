using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Class : IXmlNode {

    public Dictionary<string, Class> Relationship = new Dictionary<string, Class>();

    public Class(string id, string name)
    {
        this.Id = id;
        this.Name = name;
    }
	
}

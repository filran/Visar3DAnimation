using UnityEngine;
using System.Collections;

public abstract class IXmlNode {

    public string Name { get; set; }
    public string Id { get; set; }

    public string IdSource { get; set; }
    public string IdTarget { get; set; }
    public float PtStartY { get; set; }
    public int Seqno { get; set; }
}



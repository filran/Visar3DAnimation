//Este script deve ser aplicado ao LineRenderer

using UnityEngine;
using System.Collections;

public class AnimateMethod : MonoBehaviour {

    private LineRenderer Line;
    private float Dist;
    private float Counter;
    private float LineDrawSpeed = 6f;

    public Vector3 Origin;
    public Vector3 Destination;
    public bool Animate = false;

    // Use this for initialization
    void Start()
    {
        Line = this.GetComponent<LineRenderer>();
        Line.SetPosition(0, Origin);
        Line.SetWidth(.2f, .2f);

        Dist = Vector3.Distance(Origin, Destination);
    }

    // Update is called once per frame
    void Update()
    {
        ExecutarAnimacao();
    }

    void ExecutarAnimacao()
    {
        if (Animate)
        {
            if (Counter < Dist)
            {
                Counter += .1f / LineDrawSpeed;
                float x = Mathf.Lerp(0, Dist, Counter);
                Vector3 pointAlonfLine = x * Vector3.Normalize(Destination - Origin) + Origin;
                Line.SetPosition(1, pointAlonfLine);
            }
        }
    }
}

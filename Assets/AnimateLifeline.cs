using UnityEngine;
using System.Collections;

public class AnimateLifeline : MonoBehaviour {

    public Material Red { get; set; }
    public Material Blue { get; set; }

    public int duration = 5;
    public Renderer rend;

	// Use this for initialization
	void Start () {
        rend = this.transform.FindChild("Cube").GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        //float lerp = Mathf.PingPong(Time.time, duration) / duration;
        //rend.material.Lerp(Blue, Red, lerp);

        if ((int)Time.time > 1)
        {
            float lerp = duration / (int)Time.time;

            while (lerp > 1)
            {
                rend.material.Lerp(Blue, Red, lerp);
            }
        }
    }

    #region PRIVATE METHODS



    #endregion

    #region PUBLIC METHODS



    #endregion
}

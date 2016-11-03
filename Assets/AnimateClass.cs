using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimateClass : MonoBehaviour
{

    private float Lerp = 0;
    private Color AlpaOne;
    private Color AlpZero;
    private bool Animate = false;
    private string Direction;

    public float Speed = 2f;
    public Renderer Rend;
    public List<GameObject> Relationships = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        Rend = this.transform.FindChild("Cube").GetComponent<Renderer>();
        AlpaOne = new Color(Rend.material.color.r, Rend.material.color.g, Rend.material.color.b, 1);
        AlpZero = new Color(Rend.material.color.r, Rend.material.color.g, Rend.material.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        ExectuarAnimacao();
    }

    #region PRIVATE METHODS

    void ExectuarAnimacao()
    {
        if (Animate)
        {
            Lerp += Speed * Time.deltaTime;

            if (Direction.Equals("right"))
            {
                Rend.material.color = Color.Lerp(new Color(Rend.material.color.r, Rend.material.color.g, Rend.material.color.b, Rend.material.color.a), AlpaOne, Lerp);
                
                this.gameObject.transform.FindChild("Text").gameObject.SetActive(true);

                foreach(GameObject r in Relationships)
                {
                    r.GetComponent<AnimateLine>().Animar("right");
                }

                if (Rend.material.color.a.Equals(1))
                {
                    this.Animate = false;
                }
            }

            if (Direction.Equals("left"))
            {
                Rend.material.color = Color.Lerp(AlpaOne, AlpZero, Lerp);
                
                this.gameObject.transform.FindChild("Text").gameObject.SetActive(false);

                foreach (GameObject r in Relationships)
                {
                    r.GetComponent<AnimateLine>().Animar("left");
                }

                if (Rend.material.color.a.Equals(0))
                {
                    this.Animate = false;
                }
            }
        }
        else
        {
            Lerp = 0;
        }
    }

    #endregion

    #region PUBLIC METHODS

    public void Animar(string direction)
    {
        this.Animate = true;
        this.Direction = direction;
    }

    #endregion
}

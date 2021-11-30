using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class obstacle : MonoBehaviour
{
    private float time;
    private float startTime;
    private bool condition = false;


    // Start is called before the first frame update
    void Start()
    {
        time = Random.Range(1f, 3f);

        startTime = time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!condition)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            if (time <= 0)
            {
                this.transform.DOMove(new Vector2(this.transform.position.x, this.transform.position.y + .5f), 1, false);

                time = startTime;
                condition = true;
            }
        }

        if (condition)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            if (time <= 0)
            {
                this.transform.DOMove(new Vector2(this.transform.position.x, this.transform.position.y - .5f), 1, false);

                time = startTime;
                condition = false;
            }
        }

    }

    
}

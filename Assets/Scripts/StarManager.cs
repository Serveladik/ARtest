using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : StarMechanic
{
    //public Sprite playSound;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Collider2D collider2d;
    //[SerializeField] bool touched = false;
    
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        collider2d = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Touched();
        //OnMouseDown();

    }


    public IEnumerator StarSpawning()
    {
        yield return new WaitForSecondsRealtime(1);
        Instantiate(gameObject, startStartPos,Quaternion.identity);
        yield return new WaitForSecondsRealtime(1);
    }

    public void Touched()
    {
       if (Input.GetMouseButtonDown(0)) 
       {
       }
    }

    public void OnMouseDown()
    {
        
        Debug.Log("YES");
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMechanic : MonoBehaviour
{
    [SerializeField] GameObject[] PathList;
    //public GameObject star;
    protected Vector3 startStartPos; 
    [SerializeField] float speed;
    float moveTime;
    Vector3 CurrentPath;
    int currentPath;
    void Start()
    {
        CheckPath();
    }
    void CheckPath()
    {
        startStartPos = transform.position; 
        //star = GetComponent<GameObject>();
        CurrentPath = PathList[currentPath].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        StarMovement();
    }

    void StarMovement()
    {
        
        if(transform.position!=CurrentPath)
        {
            transform.position = Vector3.MoveTowards(transform.position,CurrentPath,speed/1000f);
        }
        else
        {
            if(currentPath < PathList.Length-1)
            {
                currentPath++;
                CheckPath();
            }
            else
            {
                currentPath=-1;
            }
        }
        
    }



}


namespace GoogleARCore
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Input = InstantPreviewInput;
    
    public class StarManager : StarMechanic
    {
        //public Sprite playSound;
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private Collider collider;
        //[SerializeField] bool touched = false;


        void Start()
        {
            sprite = gameObject.GetComponent<SpriteRenderer>();
            collider = gameObject.GetComponent<BoxCollider>();
        }
    
        // Update is called once per frame
        void Update()
        {
            //Touched();
            OnPointerDown();
    
        }
    
    
        public IEnumerator StarSpawning()
        {
            yield return new WaitForSecondsRealtime(1);
            Instantiate(gameObject, startStartPos,Quaternion.identity);
            yield return new WaitForSecondsRealtime(1);
        }
    
        public void Touched()
        {
         
        }
    
        void OnPointerDown()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
 
                var wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                var touchPosition = new Vector2(wp.x, wp.y);

                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.GetTouch(0).position), out hit))
                {
                    Debug.Log(hit.transform.name);
                    if (hit.collider == collider)
                    {
                        Destroy(this.gameObject);
                    }
                }
            }
        }
    }
}
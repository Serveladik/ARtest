
namespace GoogleARCore.Examples.HelloAR
{
    using System.Collections.Generic;
    using GoogleARCore;
    using GoogleARCore.Examples.Common;
    using UnityEngine;
    using UnityEngine.EventSystems;

#if UNITY_EDITOR
    
    using Input = InstantPreviewInput;
#endif

    
    public class ARController : MonoBehaviour
    {
        public InstantPlacementMenu InstantPlacementMenu;
        public GameObject InstantPlacementPrefab;
        public Camera FirstPersonCamera;
        public GameObject GameObjectVerticalPlanePrefab;
        public GameObject GameObjectHorizontalPlanePrefab;
        public GameObject GameObjectPointPrefab;

        public bool targetExist = false;
        bool foundHit = false;
        TrackableHit hit;

        public void Awake()
        {
            //Application.targetFrameRate = 60;
        }

        
        public void Update()
        {
            CheckHitAndBuild();
            PlaceObjectInInstantPreview();
        }
        void CheckHitAndBuild()
        {
            if(!targetExist)
            if (foundHit)
            {
                GameObject prefab;
                if (hit.Trackable is InstantPlacementPoint)
                    {
                        prefab = InstantPlacementPrefab;
                    }
                    else if (hit.Trackable is FeaturePoint)
                    {
                        prefab = GameObjectPointPrefab;
                    }
                    else if (hit.Trackable is DetectedPlane)
                    {
                        DetectedPlane detectedPlane = hit.Trackable as DetectedPlane;
                        if (detectedPlane.PlaneType == DetectedPlaneType.Vertical)
                        {
                            prefab = GameObjectVerticalPlanePrefab;
                        }
                        else
                        {
                            prefab = GameObjectHorizontalPlanePrefab;
                        }
                    }
                    else
                    {
                        prefab = GameObjectHorizontalPlanePrefab;
                    }
                var gameObject = Instantiate(prefab, hit.Pose.position, hit.Pose.rotation);
                var anchor = hit.Trackable.CreateAnchor(hit.Pose);
                gameObject.transform.parent = anchor.transform;

                if (hit.Trackable is InstantPlacementPoint)
                {
                    gameObject.GetComponentInChildren<InstantPlacementEffect>().InitializeWithTrackable(hit.Trackable);
                }
                targetExist=true;
            }
        }
        void PlaceObjectInInstantPreview()
        {
            Touch touch;
            if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
            {
                return;
            }

            if (InstantPlacementMenu.IsInstantPlacementEnabled())
            {
                foundHit = Frame.RaycastInstantPlacement(touch.position.x, touch.position.y, 1.0f, out hit);
            }
            else
            {
                TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
                    TrackableHitFlags.FeaturePointWithSurfaceNormal;
                foundHit = Frame.Raycast(
                    touch.position.x, touch.position.y, raycastFilter, out hit);
            }
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="HelloARController.cs" company="Google LLC">
//
// Copyright 2020 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------

namespace GoogleARCore.Examples.HelloAR
{
    using System.Collections.Generic;
    using GoogleARCore;
    using GoogleARCore.Examples.Common;
    using UnityEngine;
    using UnityEngine.EventSystems;

#if UNITY_EDITOR
    // Set up touch input propagation while using Instant Preview in the editor.
    using Input = InstantPreviewInput;
#endif

    /// <summary>
    /// Controls the AR example.
    /// </summary>
    public class ARController : MonoBehaviour
    {
        /// <summary>
        /// The Depth Setting Menu.
        /// </summary>
        public DepthMenu DepthMenu;

        /// <summary>
        /// The Instant Placement Setting Menu.
        /// </summary>
        public InstantPlacementMenu InstantPlacementMenu;

        /// <summary>
        /// A prefab to place when an instant placement raycast from a user touch hits an instant
        /// placement point.
        /// </summary>
        public GameObject InstantPlacementPrefab;

        /// <summary>
        /// The first-person camera being used to render the passthrough camera image (i.e. AR
        /// background).
        /// </summary>
        public Camera FirstPersonCamera;

        /// <summary>
        /// A prefab to place when a raycast from a user touch hits a vertical plane.
        /// </summary>
        public GameObject GameObjectVerticalPlanePrefab;

        /// <summary>
        /// A prefab to place when a raycast from a user touch hits a horizontal plane.
        /// </summary>
        public GameObject GameObjectHorizontalPlanePrefab;

        /// <summary>
        /// A prefab to place when a raycast from a user touch hits a feature point.
        /// </summary>
        public GameObject GameObjectPointPrefab;

        /// <summary>
        /// The rotation in degrees need to apply to prefab when it is placed.
        /// </summary>
        private const float _prefabRotation = 180.0f;

        /// <summary>
        /// True if the app is in the process of quitting due to an ARCore connection error,
        /// otherwise false.
        /// </summary>
        private bool _isQuitting = false;
        public bool targetExist = false;
        bool foundHit = false;
        TrackableHit hit;
        public void Awake()
        {
            //Application.targetFrameRate = 60;
        }

        
        public void Update()
        {
            
            
            // If the player has not touched the screen, we are done with this update.
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

            // Raycast against the location the player touched to search for planes.


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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using UnityEngine.EventSystems;
using GoogleARCore.Examples.Common;

namespace NoogleARCore.Examples.HelloAR
{
    using Input = InstantPreviewInput;

public class BuildObject : MonoBehaviour
{
    

    public DepthMenu DepthMenu;
    public InstantPlacementMenu InstantPlacementMenu;
    public GameObject InstantPlacementPrefab;
    public Camera FirstPersonCamera;
    private const float _prefabRotation = 180.0f;

    void Awake()
    {
        Application.targetFrameRate = 60;
    }
    void Update()
    {
        
        
    }
}
}

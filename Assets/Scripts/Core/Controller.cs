using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Core.BoardNS;

namespace Core
{
    public class Controller : MonoBehaviour
    {
        private Vector3 firstTouchPosition;
        public static Controller instance;
        private bool draggingMode;
        private bool permission = true;
        private bool isControllerOn = false;
        private Vector3 firstPosition;
        private Camera mainCamera;
        private Limits limist;
        [SerializeField] private GameObject movethat;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        public void MoveWithLimits(Vector3 offset, Vector3 screenPoint)
        {
            if (firstPosition.x + offset.x > limist.maxX || firstPosition.z + offset.z > limist.maxZ || 
                firstPosition.x + offset.x < limist.minX || firstPosition.z + offset.z < limist.minZ)
            {
                firstPosition = movethat.transform.position;
                firstTouchPosition = screenPoint;
            }
            else
            {
                var newPosition = firstPosition + new Vector3(offset.x, 0, offset.z);
                movethat.transform.position = newPosition;
            }
        }

        public void TurnOnController()
        {
            isControllerOn = true;
            if(mainCamera == null)
                mainCamera = Camera.main;
        }

        public void TurnOffController()
        {
            isControllerOn = false;
            draggingMode = false;
        }

        private void MouseDown()
        {
            var screenPoint = Input.mousePosition;
            screenPoint.z = 60;
            screenPoint = mainCamera.ScreenToWorldPoint(screenPoint);
            firstTouchPosition = screenPoint;
            firstPosition = movethat.transform.position;
            draggingMode = true;
        }

        private void MouseMove()
        {
            if (draggingMode)
            {
                var screenPoint = Input.mousePosition;
                screenPoint.z = 60;
                screenPoint = mainCamera.ScreenToWorldPoint(screenPoint);
                var offset = screenPoint - firstTouchPosition;
                MoveWithLimits(offset, screenPoint);
            }
        }

        private void MouseUp()
        {
            if (draggingMode)
            {
                var screenPoint = Input.mousePosition;
                screenPoint.z = 60;
                screenPoint = mainCamera.ScreenToWorldPoint(screenPoint);
                var offset = screenPoint - firstTouchPosition;
                MoveWithLimits(offset, screenPoint);

            }
            draggingMode = false;
        }

        private void Update()
        {
            if (isControllerOn)
            {
                RunController();
            }
        }

        private bool IsPointerOverUIObject()
        {
            var eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }

        private void RunController()
        {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_STANDALONE_WIN

            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (Input.GetMouseButtonDown(0) && permission)
                {
                    MouseDown();
                }

                if (Input.GetMouseButton(0) && permission)
                {
                    MouseMove();

                }

                if (Input.GetMouseButtonUp(0) && permission)
                {
                    MouseUp();
                }

            }
#endif

            if (!EventSystem.current.IsPointerOverGameObject() && !IsPointerOverUIObject())
            {
                if (Input.touchCount > 0)
                {
                    var touch = Input.GetTouch(0);
                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            if (permission)
                            {
                                MouseDown();
                            }
                            break;

                        case TouchPhase.Moved:
                            if (permission)
                            {
                                MouseMove();
                            }
                            break;

                        case TouchPhase.Ended:
                            if (permission)
                            {
                                MouseUp();
                            }
                            break;
                    }
                }
            }
        }

        public Limits Limist
        {
            get => limist;
            set => limist = value;
        }
    }
}
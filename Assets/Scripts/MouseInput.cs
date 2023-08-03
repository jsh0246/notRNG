using System;
using System.Collections;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Camera Camera;
    [SerializeField] private RectTransform SelectionBox;
    [SerializeField] private LayerMask UnitLayers;
    [SerializeField] private LayerMask FloorLayers;
    [SerializeField] private float DragDelay = 0.1f;

    private float MouseDownTime;
    private Vector2 StartMousePosition;

    private Battle battle;

    private void Start()
    {
        battle = GameObject.FindObjectOfType<Battle>();
    }

    private void Update()
    {
        HandleSelectionInputs();
        HandleMovementInputs();

        print(SelectionManager.Instance.AvailableUnits.Count + " " + SelectionManager.Instance.SelectedUnits.Count);
    }

    private void HandleMovementInputs()
    {
        // 일단 p1만 움직여봐
        if (Input.GetMouseButtonUp(1) && SelectionManager.Instance.SelectedUnits.Count > 0)
        {
            if (Physics.Raycast(Camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo, Mathf.Infinity, FloorLayers))
            {
                // MOVE
                Move(hitInfo.point);
            }
        }
    }

    private void Move(Vector3 point)
    {
        foreach (RSPObject unit in SelectionManager.Instance.SelectedUnits)
        {
            //RSPObject rsp = unit.GetComponent<RSPObject>();

            unit.MoveTo(point);
        }
    }

    // Shift 키를 누르고 여러개를 한번에 계속 선택은 안되는듯, 구현하기
    private void HandleSelectionInputs()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectionBox.sizeDelta = Vector2.zero;
            SelectionBox.gameObject.SetActive(true);
            StartMousePosition = Input.mousePosition;
            MouseDownTime = Time.time;
        }
        else if (Input.GetMouseButton(0) && MouseDownTime + DragDelay < Time.time)
        {
            ResizeSelectionBox();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            SelectionBox.sizeDelta = Vector2.zero;
            SelectionBox.gameObject.SetActive(false);

            if (Physics.Raycast(Camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo, Mathf.Infinity, UnitLayers) && hitInfo.collider.TryGetComponent<RSPObject>(out RSPObject unit))
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    if (SelectionManager.Instance.IsSelected(unit))
                    {
                        SelectionManager.Instance.Deselect(unit);
                    }
                    else
                    {
                        SelectionManager.Instance.Select(unit);
                    }
                }
                else
                {
                    SelectionManager.Instance.DeselectAll();
                    SelectionManager.Instance.Select(unit);
                }
            }
            else if (MouseDownTime + DragDelay > Time.time)
            {
                SelectionManager.Instance.DeselectAll();
            }

            MouseDownTime = 0;
        }
    }

    private void ResizeSelectionBox()
    {
        float width = Input.mousePosition.x - StartMousePosition.x;
        float height = Input.mousePosition.y - StartMousePosition.y;


        SelectionBox.anchoredPosition = StartMousePosition + new Vector2(width / 2, height / 2);
        SelectionBox.sizeDelta = new Vector2(Math.Abs(width), Mathf.Abs(height));

        Bounds bounds = new Bounds(SelectionBox.anchoredPosition, SelectionBox.sizeDelta);

        for (int i = 0; i < SelectionManager.Instance.AvailableUnits.Count; i++)
        {
            if (UnitIsInSelectionBox(Camera.WorldToScreenPoint(SelectionManager.Instance.AvailableUnits[i].transform.position), bounds))
            {
                SelectionManager.Instance.Select(SelectionManager.Instance.AvailableUnits[i]);
            }
            else
            {
                SelectionManager.Instance.Deselect(SelectionManager.Instance.AvailableUnits[i]);
            }
        }
    }

    private bool UnitIsInSelectionBox(Vector2 Position, Bounds Bounds)
    {
        return Position.x > Bounds.min.x && Position.x < Bounds.max.x && Position.y > Bounds.min.y && Position.y < Bounds.max.y;
    }
}

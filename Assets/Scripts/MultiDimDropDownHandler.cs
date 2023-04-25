using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using System.Linq;

public class MultiDimDropDownHandler : MonoBehaviour
{
    [Tooltip("Interactable to which the press events are being routed. Defaults to the object of the component.")]
    public Interactable routingTarget;
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private List<Interactable> menuButtons = new List<Interactable> { };
    [SerializeField]
    private List<MeshRenderer> menuQuadRenderers = new List<MeshRenderer> { };

    private List<Interactable> enabledMenuButtons = new List<Interactable> { };
    private ButtonConfigHelper mainConfigHelper;


    private void Start()
    {
        if (routingTarget == null)
        {
            routingTarget = GetComponent<Interactable>();
        }
        if (mainConfigHelper == null)
        {
            mainConfigHelper = GetComponent<ButtonConfigHelper>();
            mainConfigHelper.MainLabelText = "Select Dimensions";
        }

        for (int i = 0; i < menuButtons.Count; i++)
        {
            int x = i;
            menuButtons[i].OnClick.AddListener(delegate { MenuButtonSelected(x); });
            ButtonConfigHelper configHelper = menuButtons[i].GetComponent<ButtonConfigHelper>();
            menuQuadRenderers[i].enabled = false;
        }

        SelectNewButton(0);
        SelectNewButton(1);
        SelectNewButton(2);
    }

    private bool MenuIsOpen()
    {
        return menu.activeSelf;
    }

    private bool CanRouteInput()
    {
        return routingTarget != null && routingTarget.IsEnabled;
    }

    public void OnDropDownPress()
    {
        if (CanRouteInput())
        {
            if (MenuIsOpen())
            {
                menu.SetActive(false);
            }
            else
            {
                menu.SetActive(true);
            }
        }
    }

    private void SelectNewButton(int n)
    {
        Interactable buttonToSelect = menuButtons[n];
        if (!enabledMenuButtons.Contains(buttonToSelect))
        {
            enabledMenuButtons.Add(menuButtons[n]);
            menuQuadRenderers[n].enabled = true;
        }
        if (MenuIsOpen())
        {
            menu.SetActive(false);
        }
    }

    private void MenuButtonSelected(int n)
    {
        if (CanRouteInput())
        {
            if (enabledMenuButtons.Contains(menuButtons[n]))
            {
                DeselectOldBotton(n);
            }
            else
            {
                SelectNewButton(n);
            }         
        }
    }

    private void DeselectOldBotton(int n)
    {
        Interactable buttonToDelete = menuButtons[n];
        if (enabledMenuButtons.Contains(buttonToDelete))
        {
            enabledMenuButtons.Remove(menuButtons[n]);
            menuQuadRenderers[n].enabled = false;
        }
        if (MenuIsOpen())
        {
            menu.SetActive(false);
        }
    }
}

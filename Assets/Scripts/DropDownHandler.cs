using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class DropDownHandler : MonoBehaviour
{
    [Tooltip("Interactable to which the press events are being routed. Defaults to the object of the component.")]
    public Interactable routingTarget;
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private Interactable[] menuButtons;
    [SerializeField]
    private MeshRenderer[] menuQuadRenderer;

    private Interactable enabledMenuButton;
    private MeshRenderer enabledMenuQuadRenderer;
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
        }

        for (int i = 0; i < menuButtons.Length; i++)
        {
            int x = i;
            menuButtons[i].OnClick.AddListener(delegate { MenuButtonSelected(x); });
            ButtonConfigHelper configHelper = menuButtons[i].GetComponent<ButtonConfigHelper>();
            menuQuadRenderer[i].enabled = false;
        }
        enabledMenuButton = menuButtons[0];
        enabledMenuQuadRenderer = menuQuadRenderer[0];

        SelectNewButton();
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

    private void SelectNewButton()
    {
        ButtonConfigHelper configHelper = enabledMenuButton.GetComponent<ButtonConfigHelper>();
        enabledMenuQuadRenderer.enabled = true;

        mainConfigHelper.MainLabelText = configHelper.MainLabelText;

        if (MenuIsOpen())
        {
            menu.SetActive(false);
        }
    }

    private void MenuButtonSelected(int n)
    {
        if (CanRouteInput())
        {
            DeselectOldBotton();
            enabledMenuButton = menuButtons[n];
            enabledMenuQuadRenderer = menuQuadRenderer[n];
            SelectNewButton();
        }
    }

    private void DeselectOldBotton()
    {
        if (enabledMenuButton)
        {
            enabledMenuQuadRenderer.enabled = false;
        }
    }
}

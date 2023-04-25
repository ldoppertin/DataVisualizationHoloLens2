using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using u2vis;
using DataSetHandling;

public class DisplayDataMenuHandler : MonoBehaviour
{
    [Tooltip("Button Interactables to which the press events are being routed.")]
    public Interactable[] menuButtons;
    [Tooltip("BaseVisualizationView where data should be plotted. Must be given, has no default.")]
    public BaseVisualizationView visualizationView;
    [Tooltip("The data columns that can possibly be displayed by button presses." +
            "Note: Should not exceed the number of columns the data object has!")]
    public int[] dataColumnsToDisplay;
    public int defaultEnabledButtonIndex = 0;

    private AbstractDataProvider dataProvider;
    private GenericDataPresenter presenter;
    private DataDimension[] dimensions;
    private DataDimension dimension;

    private void Awake()
    {
        if (presenter == null)
        {
            presenter = visualizationView.Presenter;
        }
        dataProvider = presenter.DataProvider;
        dimensions = new DataDimension[dataProvider.Data.Count];

        InitButtonData();
    }


    private void InitButtonData()
    {
        for (int i = 0; i < dataProvider.Data.Count; i++)
        {
            dimensions[i] = dataProvider.Data[i];
        }

        for (int i = 0; i < menuButtons.Length; i++)
        {
            if (dimensions.Length < dataColumnsToDisplay[i])
            {
                Debug.Log($"The given data index ({dataColumnsToDisplay[i]}) for the dropdown button {i} cannot be larger than the number of data dimensions ({dimensions.Length})!");
            }
            int x = i;
            menuButtons[i].OnClick.AddListener(delegate { MenuButtonSelected(x); });

            dimension = dimensions[dataColumnsToDisplay[i]];

            ButtonConfigHelper configHelper = menuButtons[i].GetComponent<ButtonConfigHelper>();
            configHelper.MainLabelText = dimension.Name;

        }
        // show default buttons data
        MenuButtonSelected(defaultEnabledButtonIndex);
    }


    public void MenuButtonSelected(int n)
    {
        presenter.SetDimensionAtIndex(dataColumnsToDisplay[n], 1);
    }
    
}

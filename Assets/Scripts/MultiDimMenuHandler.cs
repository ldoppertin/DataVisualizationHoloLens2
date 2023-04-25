using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using u2vis;
using DataSetHandling;
using System.Linq;

public class MultiDimMenuHandler : MonoBehaviour
{
    [Tooltip("Button Interactables to which the press events are being routed.")]
    public Interactable[] menuButtons;
    [Tooltip("BaseVisualizationView where data should be plotted. Must be given, has no default.")]
    public BaseVisualizationView visualizationView;
    [Tooltip("The data columns that can possibly be displayed by button presses." +
            "Note: Should not exceed the number of columns the data object has!")]
    public int[] possibleDataColumnsToDisplay;
    public int[] defaultEnabledButtonIndeces = new int[] { 0, 1, 2 };

    private AbstractDataProvider dataProvider;
    private MultiDimDataPresenter presenter;
    private DataDimension[] dimensions;
    private List<int> dimensionsToDisplay = new List<int> { 1, 2, 3 };  // must be one more than button defaults becouse of timepoint dimension

    private void Awake()
    {
        if (presenter == null)
        {
            presenter = (MultiDimDataPresenter)visualizationView.Presenter;
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
            if (dimensions.Length < possibleDataColumnsToDisplay[i])
            {
                Debug.Log($"The given data index ({possibleDataColumnsToDisplay[i]}) for the dropdown button {i} cannot be larger than the number of data dimensions ({dimensions.Length})!");
            }
            int x = i;
            menuButtons[i].OnClick.AddListener(delegate { MenuButtonSelected(x); });

            DataDimension dimension = dimensions[possibleDataColumnsToDisplay[i]];

            ButtonConfigHelper configHelper = menuButtons[i].GetComponent<ButtonConfigHelper>();
            configHelper.MainLabelText = dimension.Name;

        }
        // show default buttons data
        presenter.Initialize(dataProvider, presenter.SelectedMinItem, presenter.SelectedMaxItem, dimensionsToDisplay.ToArray());

    }


    public void MenuButtonSelected(int n_data_column)
    {
        int corresponding_dim_to_data = n_data_column + 1;
        if (dimensionsToDisplay.Contains(corresponding_dim_to_data))
        {
            dimensionsToDisplay.Remove(corresponding_dim_to_data);
        }
        else
        {
            dimensionsToDisplay.Add(corresponding_dim_to_data);
        }
        presenter.Initialize(dataProvider, presenter.SelectedMinItem, presenter.SelectedMaxItem, dimensionsToDisplay.ToArray());
    }

}

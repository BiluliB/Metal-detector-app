using Magnetify.ViewModels;
using System.Diagnostics;

namespace Magnetify.Views;

public partial class HomePage : ContentPage
{
    private readonly HomeViewModel _viewModel;

    public HomePage(HomeViewModel viewModel)
    {
        _viewModel = viewModel;
        InitializeComponent();
        BindingContext = _viewModel;
    }

    /// <summary>
    /// Page appearing, check and act
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Debug.WriteLine("Home page appearing, checking and acting!");
        App.IsHome = true;
        _viewModel.DisableSleep();
        _viewModel.CheckAndAct();
    }

    /// <summary>
    /// Page disappearing, stop any actors
    /// </summary>
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        Debug.WriteLine("Home page disappearing, running Stop");
        _viewModel.EnableSleep();
        _viewModel.Stop();
        App.IsHome = false;
    }
}
using MedVault.Data.Repositories;
using MedVault.ViewModels;

namespace MedVault;

public partial class MainPage : ContentPage
{
    public MedicinesViewModel _viewModel;

    public MainPage(MedicinesViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    internal IMedicineService MedicineService { get; }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.OnActivated();
    }
}



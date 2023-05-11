using CommunityToolkit.Mvvm.Input;
using MedVault.Data.Models;
using MedVault.Data.Repositories;
using MedVault.Pages;
using Microsoft.Datasync.Client;

namespace MedVault.ViewModels;

public partial class MedicinesViewModel : BaseViewModel
{
    private readonly IMedicineService _service;

    public MedicinesViewModel(IMedicineService service)
    {
        _service = service;
        Title = "Rx List";
    }

    public async void OnActivated()
    {
        await RefreshAsync();
    }

    public ConcurrentObservableCollection<Medicine> Medicines { get; } = new();

    [RelayCommand]
    public async Task RefreshAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            await _service.RefreshMedicinesAsync();
            var medicines = await _service.GetMedicinesAsync();
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                Medicines.ReplaceAll(medicines);
            });

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    public async Task GoToDetails(Medicine medicine)
    {
        if (medicine is null)
            return;

        await Shell.Current.GoToAsync(nameof(MedicineDetailsPage), true, new Dictionary<string, object>
        {
            {"Medicine", medicine }
        });
    }

    [RelayCommand]
    public async Task GoToCreate()
    {
        await AppShell.Current.GoToAsync(nameof(EditMedicinePage), true);
    }

}


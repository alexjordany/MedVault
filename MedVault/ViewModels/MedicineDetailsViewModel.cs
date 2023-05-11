using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MedVault.Data.Models;
using MedVault.Pages;

namespace MedVault.ViewModels;

[QueryProperty(nameof(Medicine), "Medicine")]
public partial class MedicineDetailsViewModel : BaseViewModel
{
    public MedicineDetailsViewModel()
    {
    }

    [ObservableProperty]
    Medicine medicine;

    [RelayCommand]
    async Task GoToEdition(Medicine medicine)
    {
        if (medicine is null)
            return;

        await Shell.Current.GoToAsync(nameof(EditMedicinePage), true, new Dictionary<string, object>
        {
            {"Medicine", medicine }
        });
    }
}


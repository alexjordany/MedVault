using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MedVault.Data.Models;
using MedVault.Data.Repositories;

namespace MedVault.ViewModels;

[QueryProperty(nameof(Medicine), "Medicine")]
public partial class EditMedicineViewModel : BaseViewModel
{
    [ObservableProperty]
    Medicine medicine = new Medicine();

    private readonly IMedicineService _service;

    public EditMedicineViewModel(IMedicineService service)
    {
        _service = service;
    }

    [RelayCommand]
    public async void SaveChanges()
    {
        try
        {
            await _service.SaveMedicineAsync(Medicine);
            await Shell.Current.DisplayAlert("Saved", "Medicine data writted successfully", "OK");

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
    }

}


using MedVault.ViewModels;

namespace MedVault.Pages;

public partial class MedicineDetailsPage : ContentPage
{
    public MedicineDetailsPage(MedicineDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}

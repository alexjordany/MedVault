using MedVault.ViewModels;

namespace MedVault.Pages;

public partial class EditMedicinePage : ContentPage
{
    public EditMedicinePage(EditMedicineViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}

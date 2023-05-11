using MedVault.Pages;

namespace MedVault;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(MedicineDetailsPage), typeof(MedicineDetailsPage));
        Routing.RegisterRoute(nameof(EditMedicinePage), typeof(EditMedicinePage));
    }
}


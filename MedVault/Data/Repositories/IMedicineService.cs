using System;
using MedVault.Data.Models;

namespace MedVault.Data.Repositories;

public interface IMedicineService
{
    Task<IEnumerable<Medicine>> GetMedicinesAsync();
    Task RefreshMedicinesAsync();
    Task RemoveMedicinesAsync(Medicine medicine);
    Task SaveMedicineAsync(Medicine medicine);
}


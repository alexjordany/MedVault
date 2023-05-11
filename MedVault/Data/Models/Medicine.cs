using System;
using Microsoft.Datasync.Client;

namespace MedVault.Data.Models;

public class Medicine : DatasyncClientData
{
    public string MedicineName { get; set; }
    public int MedicineQuantity { get; set; }
    public DateTime MedicineExpiration { get; set; }
    public string MedicineDescription { get; set; }
}


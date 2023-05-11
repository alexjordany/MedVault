using System;
namespace MedVault.Data;

public static class Constants
{
    public const string ServiceUri = "https://medicinemanagementdatasyncserver.azurewebsites.net";
    public static readonly string OfflineDb = FileSystem.CacheDirectory + "/offline.db";
}


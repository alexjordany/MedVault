using System;
using MedVault.Data.Models;
using Microsoft.Datasync.Client;
using Microsoft.Datasync.Client.SQLiteStore;

namespace MedVault.Data.Repositories;

public class RemoteMedicineRepository : IMedicineService
{
    private DatasyncClient _client = null;
    private IOfflineTable<Medicine> _table = null;
    private bool _initialized = false;
    private readonly SemaphoreSlim _asyncLock = new(1, 1);
    public Func<Task<AuthenticationToken>> TokenRequestor;

    public RemoteMedicineRepository()
    {
        TokenRequestor = null;
    }

    private async Task InitializeAsync()
    {
        if (_initialized)
            return;
        try
        {
            await _asyncLock.WaitAsync();
            if (_initialized)
                return;

            var connectionString = new UriBuilder { Scheme = "file", Path = Constants.OfflineDb, Query = "?mode=rwc" }.Uri.ToString();

            var store = new OfflineSQLiteStore(connectionString);
            store.DefineTable<Medicine>();
            var options = new DatasyncClientOptions
            {
                HttpPipeline = Array.Empty<HttpMessageHandler>(),
                OfflineStore = store
            };

            _client = TokenRequestor == null
                ? new DatasyncClient(Constants.ServiceUri, options)
                : new DatasyncClient(Constants.ServiceUri, new GenericAuthenticationProvider(TokenRequestor), options);
            await _client.InitializeOfflineStoreAsync();

            _table = _client.GetOfflineTable<Medicine>();

            _initialized = true;
        }

        catch (Exception)
        {
            throw;
        }
        finally
        {
            _asyncLock.Release();
        }
    }

    public async Task<IEnumerable<Medicine>> GetMedicinesAsync()
    {
        await InitializeAsync();
        return await _table.ToListAsync();
    }

    public async Task RefreshMedicinesAsync()
    {
        await InitializeAsync();
        await _table.PushItemsAsync();
        await _table.PullItemsAsync();
        return;
    }

    public async Task RemoveMedicinesAsync(Medicine medicine)
    {
        if (medicine is null)
        {
            throw new ArgumentNullException(nameof(medicine));
        }
        if (medicine.Id is null)
            return;

        await InitializeAsync();
        await _table.DeleteItemAsync(medicine);
    }

    public async Task SaveMedicineAsync(Medicine medicine)
    {
        if (medicine is null)
            throw new ArgumentNullException(nameof(medicine));

        await InitializeAsync();


        if (medicine.Id is null)
        {
            await _table.InsertItemAsync(medicine);
        }
        else
        {
            await _table.ReplaceItemAsync(medicine);
        }
    }
}


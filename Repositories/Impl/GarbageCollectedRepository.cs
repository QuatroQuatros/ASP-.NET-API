using System.Data;
using GestaoDeResiduos.Exceptions;
using GestaoDeResiduos.Infra;
using GestaoDeResiduos.Models;
using GestaoDeResiduos.ViewModels.Responses;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace GestaoDeResiduos.Repositories.Impl;

public class GarbageCollectedRepository : Repository<GarbageCollectedModel>, IGarbageCollectedRepository
{
    private readonly IDistrictRepository _districtRepository;
    private readonly IStateRepository _stateRepository;
    private readonly IRegionRepository _regionRepository;
    private readonly IGarbageCollectionTypeRepository _garbageCollectionTypeRepository;

    public GarbageCollectedRepository(DatabaseContext context, IDistrictRepository districtRepository, IGarbageCollectionTypeRepository garbageCollectionTypeRepository, IStateRepository stateRepository, IRegionRepository regionRepository) : base(context)
    {
        _districtRepository = districtRepository;
        _stateRepository = stateRepository;
        _regionRepository = regionRepository;
        _garbageCollectionTypeRepository = garbageCollectionTypeRepository;
    }

    public async Task<TrashResultState> GetStateMoreTrashAsync(int? stateId = null, int? collectionTypeId = null)
    {
        
        var query = _context.GarbageCollected
            .Include(gc => gc.CollectionDay)
            .ThenInclude(cd => cd.Street)
            .ThenInclude(s => s.District)
            .ThenInclude(d => d.Region)
            .ThenInclude(r => r.State)
            .Include(gc => gc.CollectionDay.GarbageCollectionType)
            .AsQueryable();

        if (stateId.HasValue)
        {
            try
            {
                await _stateRepository.GetByIdAsync(stateId ?? stateId.Value);

            }catch (NotFoundException e)
            {
                throw new NotFoundException("Estado não encontrado.");
            }
            
            query = query.Where(gc => gc.CollectionDay.Street.District.Region.State.Id == stateId.Value);
        }

        if (collectionTypeId.HasValue)
        {
            await CheckIfCollectionTypeExists(collectionTypeId.Value);  
            query = query.Where(gc => gc.CollectionDay.GarbageCollectionType.Id == collectionTypeId.Value);
        }

        var result = await query
            .GroupBy(gc => new 
            { 
                EstadoNome = gc.CollectionDay.Street.District.Region.State.Name, 
                ColetaNome = gc.CollectionDay.GarbageCollectionType.Name 
            })
            .Select(g => new 
            {
                QuantidadeLixo = g.Sum(gc => gc.Amount),
                NomeEstado = g.Key.EstadoNome,
                NomeColeta = g.Key.ColetaNome
            })
            .OrderByDescending(g => g.QuantidadeLixo)
            .FirstOrDefaultAsync();

        return result != null ? new TrashResultState
        {
            QuantidadeLixo = result.QuantidadeLixo,
            NomeEstado = result.NomeEstado,
            NomeColeta = result.NomeColeta
        } : null; 
    }
    
    public async Task<TrashResultRegion> GetRegionMoreTrashAsync(int? regionId = null, int? collectionTypeId = null)
    {
        var query = _context.GarbageCollected
            .Include(gc => gc.CollectionDay)
            .ThenInclude(cd => cd.Street)
            .ThenInclude(s => s.District)
            .ThenInclude(d => d.Region)
            .Include(gc => gc.CollectionDay.GarbageCollectionType)
            .AsQueryable();

        if (regionId.HasValue)
        {
            try
            {
                await _regionRepository.GetByIdAsync(regionId ?? regionId.Value);

            }catch (NotFoundException _)
            {
                throw new NotFoundException("Região não encontrada.");
            }
            
            query = query.Where(gc => gc.CollectionDay.Street.District.Region.Id == regionId.Value);
        }

        if (collectionTypeId.HasValue)
        {
            await CheckIfCollectionTypeExists(collectionTypeId.Value);  
            query = query.Where(gc => gc.CollectionDay.GarbageCollectionType.Id == collectionTypeId.Value);
        }

        var result = await query
            .GroupBy(gc => new
            {
                RegiaoNome = gc.CollectionDay.Street.District.Region.Name,
                ColetaNome = gc.CollectionDay.GarbageCollectionType.Name
            })
            .Select(g => new
            {
                QuantidadeLixo = g.Sum(gc => gc.Amount),
                NomeRegiao = g.Key.RegiaoNome,
                NomeColeta = g.Key.ColetaNome
            })
            .OrderByDescending(g => g.QuantidadeLixo)
            .FirstOrDefaultAsync();

        return result != null ? new TrashResultRegion
        {
            QuantidadeLixo = result.QuantidadeLixo,
            NomeRegiao = result.NomeRegiao,
            NomeColeta = result.NomeColeta
        } : null;
    }
    
    public async Task<TrashResultNeighborhood> GetNeighborhoodMoreTrashAsync(int? districtId = null, int? collectionTypeId = null)
    {
        var query = _context.GarbageCollected
            .Include(gc => gc.CollectionDay)
            .ThenInclude(cd => cd.Street)
            .ThenInclude(s => s.District)
            .Include(gc => gc.CollectionDay.GarbageCollectionType)
            .AsQueryable();

        if (districtId.HasValue)
        {
            try
            {
                await _districtRepository.GetByIdAsync(districtId ?? districtId.Value);

            }catch (NotFoundException _)
            {
                throw new NotFoundException("Bairro não encontrado.");
            }
            query = query.Where(gc => gc.CollectionDay.Street.District.Id == districtId.Value);
        }

        if (collectionTypeId.HasValue)
        {
            await CheckIfCollectionTypeExists(collectionTypeId.Value);  
            query = query.Where(gc => gc.CollectionDay.GarbageCollectionType.Id == collectionTypeId.Value);
        }

        var result = await query
            .GroupBy(gc => new
            {
                BairroName = gc.CollectionDay.Street.District.Name,
                ColetaName = gc.CollectionDay.GarbageCollectionType.Name
            })
            .Select(g => new
            {
                QuantidadeLixo = g.Sum(gc => gc.Amount),
                NomeBairro = g.Key.BairroName,
                NomeColeta = g.Key.ColetaName
            })
            .OrderByDescending(g => g.QuantidadeLixo)
            .FirstOrDefaultAsync();

        return result != null ? new TrashResultNeighborhood
        {
            QuantidadeLixo = result.QuantidadeLixo,
            NomeBairro = result.NomeBairro,
            NomeColeta = result.NomeColeta
        } : null;
    }
    
    private async Task CheckIfCollectionTypeExists(int collectionTypeId)
    {
        try
        {
            await _garbageCollectionTypeRepository.GetByIdAsync(collectionTypeId);

        }catch (NotFoundException _)
        {
            throw new NotFoundException("Tipo de coleta não encontrado.");
        }
    }
}





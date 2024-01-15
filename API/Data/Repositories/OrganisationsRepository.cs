using API.Entities;
using API.Interfaces;

namespace API.Data.Repositories;

public sealed class OrganisationsRepository : IOrganisationsRepository
{
    private readonly AuthenticationContext _context;

    public OrganisationsRepository(AuthenticationContext context)
    {
        _context = context;
    }

    public void Add(Organisation organisation)
    {
        _context.Organisations.Add(organisation);
    }
}
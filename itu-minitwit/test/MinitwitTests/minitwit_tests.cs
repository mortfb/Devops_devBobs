using Chirp.Core;
using Chirp.Infrastructure.Chirp.Repositories;
using Chirp.Infrastructure.Chirp.Services;
namespace new_test;

public class Minitwit_Tests
{
    // Setup for integration tests
    private IAuthorRepository? _authorrepo;
    private ICheepRepository? _cheeprepo;
    private IFollowRepository? _followrepo;
    private TestUtilities? _utils;
    private CheepDbContext? _context;
    private IChirpService? _service;
    
    public async Task InitializeAsync()
    {
        _utils = new TestUtilities();
        _context = await _utils.CreateInMemoryDb();
        _authorrepo = new AuthorRepository(_context); 
        _cheeprepo = new CheepRepository(_context);
        _followrepo = new FollowRepository(_context);
        _service = new ChirpService(_cheeprepo, _authorrepo, _followrepo);
    }


    public Task DisposeAsync()
    {
        if (_utils == null)
        {
            return Task.CompletedTask;
        }
        _utils.CloseConnection();
        return Task.CompletedTask;
    }
    
    
    [Fact]
    public async Task TestAddCheep()
    {
        if (_utils == null || _cheeprepo == null || _authorrepo == null)
        {
            return;
        }
        var cheepsBefore = await _cheeprepo.GetCheepsFromAuthor(0, "Mellie Yost");
        
        var author = await _authorrepo.GetAuthorByName("Mellie Yost");

        if (author != null)
        {
            await _cheeprepo.AddCheep("hejj", author);
        
            var cheepsAfter = await _cheeprepo.GetCheepsFromAuthor(0, "Mellie Yost");
        
            Assert.True(cheepsBefore.Count != cheepsAfter.Count);
        
            await _utils.CloseConnection();
        }
    }
}

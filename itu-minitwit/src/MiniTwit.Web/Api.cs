using Chirp.Infrastructure.Chirp.Repositories;
using Chirp.Infrastructure.DataTransferObjects;

namespace Chirp.Web;

public static class Api
{
    public static void MapProductEndpoints(this WebApplication app)
    {
        app.MapGet("/fllws/{username}", 
            (string username, IFollowRepository followRepository) => followRepository.GetFollowed(username));
    }
}
using Chirp.Infrastructure.Chirp.Repositories;
using Chirp.Infrastructure.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace Chirp.Web;

public static class Api
{
    public record FollowRequest(string? Follow, string? Unfollow);
    public static void MapProductEndpoints(this WebApplication app)
    {
        app.MapGet("/fllws/{username}",
            (string username,[FromQuery (Name = "latest")] int? latests,[FromQuery (Name = "no")] int? no, IFollowRepository followRepository) =>
            {
                return followRepository.GetFollowed(username);
            });
        
        app.MapPost("/fllws/{username}",
            (string username, [FromQuery (Name = "latest")] int? latests ,[FromBody] FollowRequest request, IFollowRepository followRepository) =>
            {
                if (!string.IsNullOrEmpty(request.Follow))
                {
                    followRepository.AddFollowing(username,request.Follow);
                    return Results.Ok($"Following {request.Follow}");
                }

                if (!string.IsNullOrEmpty(request.Unfollow))
                {
                    followRepository.RemoveFollowing(username,request.Unfollow);
                    return Results.Ok($"Unfollowing {request.Unfollow}");
                }
                
                return Results.BadRequest("Must specify either 'Follow' or 'Unfollow'");
            });
    }
}
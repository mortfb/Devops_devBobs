using Chirp.Infrastructure.Chirp.Repositories;
using Chirp.Infrastructure.DataTransferObjects;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Chirp.Web;

//API, made (shoddily) after the specification in the Stub API.
public static class Api
{
    private static int Latest;
    public record FollowRequest(string? Follow, string? Unfollow);
    public record MessageRequest(string Content);
    public record SignUpRequest(string Username, string Email, string Pwd);
    
    //TODO: For the whole Api, 'latests' (Optional: latest value to update) and 'no' (Optional: no limits result count) is not implemented.
    //TODO: Authorization is (possibly?) not handled. At least not handled here, should not be part of a minimal API, specified elsewere.
    public static void MapProductEndpoints(this WebApplication app)
    {
        app.MapGet("/fllws/{username}",
            (string username,[FromQuery (Name = "latest")] int? latests,[FromQuery (Name = "no")] int? no, IFollowRepository followRepository) =>
            {
                if (latests.HasValue)
                    Latest = latests.Value;
                return followRepository.GetFollowed(username);
            });
        
        app.MapPost("/fllws/{username}",
            (string username, [FromQuery (Name = "latest")] int? latests ,[FromBody] FollowRequest request, IFollowRepository followRepository) =>
            {
                if (latests.HasValue)
                    Latest = latests.Value;
                
                if (!string.IsNullOrEmpty(request.Follow))
                {
                    return followRepository.AddFollowing(username,request.Follow);
                }

                if (!string.IsNullOrEmpty(request.Unfollow))
                {
                    return followRepository.RemoveFollowing(username,request.Unfollow);
                }
                
                return Task.FromResult(Results.BadRequest("Must specify either 'Follow' or 'Unfollow'"));
            });
        
        app.MapGet("/latest",() => new {Latest});
        
        app.MapGet("/msgs",
            ([FromQuery (Name = "latest")] int? latests,[FromQuery (Name = "no")] int? no, ICheepRepository  cheepRepository) =>
            {
                if (latests.HasValue)
                    Latest = latests.Value;
                return cheepRepository.GetCheeps(0);
            });
        
        app.MapGet("/msgs/{username}",
            (string username,[FromQuery (Name = "latest")] int? latests,[FromQuery (Name = "no")] int? no, ICheepRepository cheepRepository) =>
            {
                if (latests.HasValue)
                    Latest = latests.Value;
                return cheepRepository.GetAllCheepsFromAuthor(username);
            });
        
        app.MapPost("/msgs/{username}",
            (string username,[FromQuery (Name = "latest")] int? latests,[FromBody] MessageRequest msgRequest, ICheepRepository cheepRepository, IAuthorRepository authorRepository) =>
            {
                if (latests.HasValue)
                    Latest = latests.Value;
                var author = authorRepository.GetAuthorByName(username).Result;
                if (author != null)
                    return cheepRepository.AddCheep(msgRequest.Content, author);
                else
                {
                    return Task.FromResult(Results.BadRequest("Author does not exist"));
                }
            });
        
        //TODO: Set the password of the new author
        app.MapPost("/register",
            ([FromQuery (Name = "latest")] int? latests,[FromBody] SignUpRequest request, IAuthorRepository authorRepository) =>
            {
                if (latests.HasValue)
                    Latest = latests.Value;
                return authorRepository.CreateAuthor(request.Username, request.Email);
            });
    }
}
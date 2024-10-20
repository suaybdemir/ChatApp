namespace ChatAppServer.WebAPI.Dtos
{
    public sealed record SendMessageDto(
        string UserId,
        string ToUserId,
        string Message);
    
}

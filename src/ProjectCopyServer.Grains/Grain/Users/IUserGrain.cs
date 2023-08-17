using Orleans;
using ProjectCopyServer.Users.Dto;

namespace ProjectCopyServer.Grains.Grain.Users;

public interface IUserGrain : IGrainWithGuidKey
{

    Task<GrainResultDto<UserGrainDto>> UpdateUserAsync(UserGrainDto input);

    Task<GrainResultDto<UserGrainDto>> GetUserAsync();
    
    Task<GrainResultDto<UserGrainDto>> SaveUserSourceAsync(UserSourceInput userSourceInput);

}
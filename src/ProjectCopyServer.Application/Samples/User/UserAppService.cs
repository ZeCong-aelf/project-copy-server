using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ProjectCopyServer.Common.Dtos;
using ProjectCopyServer.Samples.User.Provider;
using ProjectCopyServer.Samples.Users;
using ProjectCopyServer.Samples.Users.Dto;
using ProjectCopyServer.Users.Index;
using Volo.Abp.ObjectMapping;

namespace ProjectCopyServer.Samples.User;

public class UserAppService : ProjectCopyServerAppService, IUserAppService
{
    private readonly ILogger<UserAppService> _logger;
    private readonly IUserWriteProvider _userWriteProvider;
    private readonly IUserQueryProvider _userQueryProvider;
    private readonly IObjectMapper _objectMapper;

    public UserAppService(
        IUserWriteProvider userWriteProvider,
        ILogger<UserAppService> logger,
        IObjectMapper objectMapper, IUserQueryProvider userQueryProvider)

    {
        _userWriteProvider = userWriteProvider;
        _logger = logger;
        _objectMapper = objectMapper;
        _userQueryProvider = userQueryProvider;
    }

    public async Task<UserDto> AddUserAsync(UserSourceInput userSourceInput)
    {
        var userGrainDto = _objectMapper.Map<UserSourceInput, UserGrainDto>(userSourceInput);
        return await _userWriteProvider.SaveUserAsync(userGrainDto);
    }

    public async Task<UserDto> GetById(string userId)
    {
        var pager = await _userQueryProvider.QueryUserPagerAsync(new UserQueryRequestDto(0, 1)
        {
            UserId = Guid.Parse(userId)
        });
        return pager.Data.IsNullOrEmpty() ? null : _objectMapper.Map<UserIndex, UserDto>(pager.Data.First());
    }

    public async Task<PageResultDto<UserDto>> QueryUserPagerAsync(UserQueryRequestDto requestDto)
    {
        var idxPager = await _userQueryProvider.QueryUserPagerAsync(requestDto);
        return new PageResultDto<UserDto>(idxPager.TotalRecordCount, idxPager.Data
            .Select(idx => _objectMapper.Map<UserIndex, UserDto>(idx)).ToList());

    }
}
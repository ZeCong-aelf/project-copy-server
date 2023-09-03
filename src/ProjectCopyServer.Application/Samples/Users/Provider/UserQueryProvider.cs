using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AElf.Indexing.Elasticsearch;
using Nest;
using ProjectCopyServer.Common.Dtos;
using ProjectCopyServer.Samples.Users.Dto;
using ProjectCopyServer.Users.Dto;
using ProjectCopyServer.Users.Index;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ObjectMapping;

namespace ProjectCopyServer.Samples.Users.Provider;

public interface IUserQueryProvider
{
    Task<PageResultDto<UserIndex>> QueryUserPagerAsync(UserQueryRequestDto requestDto);
}

public class UserQueryProvider : IUserQueryProvider, ISingletonDependency
{
    private readonly NESTRepository<UserIndex, Guid> _userIndexResp;
    private readonly IObjectMapper _objectMapper;

    public UserQueryProvider(NESTRepository<UserIndex, Guid> userIndexResp, IObjectMapper objectMapper)
    {
        _userIndexResp = userIndexResp;
        _objectMapper = objectMapper;
    }

    /// <summary>
    ///     query method of provider output is UserIndex type,
    ///     outer method may convert UserIndex to another class
    /// </summary>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    public async Task<PageResultDto<UserIndex>> QueryUserPagerAsync(UserQueryRequestDto requestDto)
    {
        var mustQuery = new List<Func<QueryContainerDescriptor<UserIndex>, QueryContainer>>() { };

        if (requestDto.UserId != Guid.Empty)
            mustQuery.Add(q => q.Term(i => i.Field(f => f.Id).Value(requestDto.UserId)));

        
        QueryContainer Filter(QueryContainerDescriptor<UserIndex> f) => f.Bool(b => b.Must(mustQuery));
        IPromise<IList<ISort>> Sort(SortDescriptor<UserIndex> s) => s.Descending(t => t.UpdateTime);

        var (totalCount, notifyRulesIndices) = await _userIndexResp.GetSortListAsync(Filter, sortFunc: Sort);
        return new PageResultDto<UserIndex>(totalCount, notifyRulesIndices);
    }
}
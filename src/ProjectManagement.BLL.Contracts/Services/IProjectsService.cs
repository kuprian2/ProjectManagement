using System;
using ProjectManagement.BLL.Contracts.Dto;
using ProjectManagement.BLL.Contracts.Services.Base;
using System.Collections.Generic;

namespace ProjectManagement.BLL.Contracts.Services
{
    public interface IProjectsService : IService<ProjectDto>
    {
        IEnumerable<ProjectDto> GetByKeyword(string keyword);

        event EventHandler<EventArgs> Created;
    }
}

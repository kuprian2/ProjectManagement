using AutoMapper;
using EnsureThat;
using ProjectManagement.BLL.Contracts.Dto;
using ProjectManagement.DAL.Contracts.Domain;
using ProjectManagement.DAL.Contracts.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagement.BLL.Services
{
    public class WrongProjectsService
    {
        private readonly IRepository<Project> _repository;
        private readonly IMapper _mapper;

        public WrongProjectsService(IRepository<Project> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ProjectDto SelectProjectById(int id)
        {
            Ensure.Any.IsNotDefault(id);
            var entity = _repository.Get(id);
            return _mapper.Map<ProjectDto>(entity);
        }

        public IEnumerable<ProjectDto> SelectAllProjects()
        {
            var entities = _repository.GetAll();
            Ensure.Any.IsNotNull(entities);
            return _mapper.Map<IEnumerable<ProjectDto>>(entities);
        }

        public void CreateProject(ProjectDto entityDto)
        {
            Ensure.Any.IsNotNull(entityDto);
            _repository.Create(_mapper.Map<Project>(entityDto));
        }

        public void ChangeProject(ProjectDto entityDto)
        {
            Ensure.Any.IsNotNull(entityDto);
            _repository.Update(_mapper.Map<Project>(entityDto));
        }

        public void RemoveProject(int id)
        {
            Ensure.Any.IsNotDefault(id);
            _repository.Delete(id);
        }

        public IEnumerable<ProjectDto> SelectByKeyword(string keyword)
        {
            Ensure.String.IsNotNullOrWhiteSpace(keyword);
            var lowerKeyword = keyword.ToLower();
            return SelectAllProjects().Where(x =>
                    x.Information.ToLower().Contains(lowerKeyword) || x.Name.ToLower().Contains(lowerKeyword) ||
                    x.ShortInformation.ToLower().Contains(lowerKeyword))
                .ToList();
        }
    }
}

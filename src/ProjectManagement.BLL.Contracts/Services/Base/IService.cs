using ProjectManagement.BLL.Contracts.Dto.Base;
using System.Collections.Generic;

namespace ProjectManagement.BLL.Contracts.Services.Base
{
    public interface IService<TEntityDto> where TEntityDto : EntityDto
    {
        TEntityDto Get(int id);

        IEnumerable<TEntityDto> GetAll();

        void Create(TEntityDto entityDto);

        void Update(TEntityDto entityDto);

        void Delete(int id);
    }
}

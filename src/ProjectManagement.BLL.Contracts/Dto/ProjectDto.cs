using ProjectManagement.BLL.Contracts.Dto.Base;
using System;

namespace ProjectManagement.BLL.Contracts.Dto
{
    public class ProjectDto : EntityDto
    {
        public string Name { get; set; }

        public string Information { get; set; }

        public string ShortInformation { get; set; }

        public DateTime CreatureDate { get; set; }

    }
}

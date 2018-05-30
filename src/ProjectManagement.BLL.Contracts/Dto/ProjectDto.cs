using ProjectManagement.BLL.Contracts.Dto.Base;
using System;

namespace ProjectManagement.BLL.Contracts.Dto
{
    /// <summary>
    /// Dto type for projects on current model.
    /// </summary>
    public class ProjectDto : EntityDto
    {
        public string Name { get; set; }

        public string Information { get; set; }

        public string ShortInformation { get; set; }

        public DateTime CreatureDate { get; set; }

    }
}

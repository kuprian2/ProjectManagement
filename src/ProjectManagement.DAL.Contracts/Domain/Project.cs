using ProjectManagement.DAL.Contracts.Domain.Base;
using System;

namespace ProjectManagement.DAL.Contracts.Domain
{
    public class Project : Entity
    {
        public string Name { get; set; }

        public string Information { get; set; }

        public string ShortInformation { get; set; }

        public DateTime CreatureDate { get; set; }
    }
}

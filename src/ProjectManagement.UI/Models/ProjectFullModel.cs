using System;

namespace ProjectManagement.UI.Models
{
    /// <summary>
    /// Model of Project to show.
    /// </summary>
    public class ProjectFullModel
    {
        public string Name { get; set; }

        public string Information { get; set; }

        public string ShortInformation { get; set; }

        public DateTime CreatureDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using ProjectManagement.BLL.Services;
using ProjectManagement.DAL.Contracts.Domain;
using ProjectManagement.DAL.Contracts.Repositories;

namespace ProjectManagement.BLL.Tests
{
    [TestFixture]
    public class ProjectsServiceTests
    {
        private Mock<IRepository<Project>> _projectRepositoryMock;

        private ProjectsService _testingProjectsService;

        private static readonly List<Project> Projects = new List<Project>
        {
            new Project
            {
                Id = 1,
                Name = "Project1",
                CreatureDate = new DateTime(2001, 1, 1),
                Information = "Info1",
                ShortInformation = "Short info1"
            },
            new Project
            {
                Id = 2,
                Name = "Project2",
                CreatureDate = new DateTime(2002, 2, 2),
                Information = "Info2",
                ShortInformation = "Short info2"
            },
            new Project
            {
                Id = 3,
                Name = "Project3",
                CreatureDate = new DateTime(2003, 3, 3),
                Information = "Info3",
                ShortInformation = "Short info3"
            },
            new Project
            {
                Id = 4,
                Name = "Project4",
                CreatureDate = new DateTime(2004, 4, 4),
                Information = "Info4",
                ShortInformation = "Short info4"
            },
            new Project
            {
                Id = 5,
                Name = "Project5",
                CreatureDate = new DateTime(2005, 5, 5),
                Information = "Info5",
                ShortInformation = "Short info5"
            },
        };

        [Test]
        public void GetAll_ShouldReturnExpectedResult_Always()
        {
            _projectRepositoryMock = new Mock<IRepository<Project>>(MockBehavior.Strict);
            _projectRepositoryMock.Setup(p => p.GetAll()).Returns(new List<Project>(Projects));
            
            _testingProjectsService = new ProjectsService(_projectRepositoryMock.Object, AutoMapper.Mapper.Instance);
            var result = _testingProjectsService.GetAll();

            result.Count().Should().Be(Projects.Count);
        }
    }
}

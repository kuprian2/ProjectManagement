﻿using AutoMapper;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using ProjectManagement.BLL.Configuration;
using ProjectManagement.BLL.Contracts.Dto;
using ProjectManagement.BLL.Services;
using ProjectManagement.DAL.Contracts.Domain;
using ProjectManagement.DAL.Contracts.Repositories;
using System;
using System.Collections.Generic;

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
            }
        };
        private static readonly List<ProjectDto> ProjectDtos = new List<ProjectDto>
        {
            new ProjectDto
            {
                Id = 1,
                Name = "Project1",
                CreatureDate = new DateTime(2001, 1, 1),
                Information = "Info1",
                ShortInformation = "Short info1"
            },
            new ProjectDto
            {
                Id = 2,
                Name = "Project2",
                CreatureDate = new DateTime(2002, 2, 2),
                Information = "Info2",
                ShortInformation = "Short info2"
            },
            new ProjectDto
            {
                Id = 3,
                Name = "Project3",
                CreatureDate = new DateTime(2003, 3, 3),
                Information = "Info3",
                ShortInformation = "Short info3"
            }
        };

        private static readonly List<int> ExistingIdentifiers = new List<int> { 0, 1, 2};
        private static readonly List<int> NonExistingIdentifiers = new List<int> { -1, 7, 3 };

        private static IEnumerable<TestCaseData> CorrectIdentifiersCases { get; set; } = new List<TestCaseData>
        {
            new TestCaseData(ExistingIdentifiers[0]),
            new TestCaseData(ExistingIdentifiers[1]),
            new TestCaseData(ExistingIdentifiers[2]),
        };
        private static IEnumerable<TestCaseData> IncorrectIdentifiersCases { get; set; } = new List<TestCaseData>
        {
            new TestCaseData(NonExistingIdentifiers[0]),
            new TestCaseData(NonExistingIdentifiers[1]),
            new TestCaseData(NonExistingIdentifiers[2]),
        };

        private readonly IMapper _mapper;

        public ProjectsServiceTests()
        {
            _mapper = new MapperConfiguration(cfg =>
                cfg.AddProfile(new MappingProfile()))
                .CreateMapper();
        }

        [Test]
        public void GetAll_ShouldReturnExpectedResult_Always()
        {
            _projectRepositoryMock = new Mock<IRepository<Project>>(MockBehavior.Strict);
            _projectRepositoryMock.Setup(p => p.GetAll()).Returns(new List<Project>(Projects));
            _testingProjectsService = new ProjectsService(_projectRepositoryMock.Object, _mapper);

            var result = _testingProjectsService.GetAll();

            result.Should().BeEquivalentTo(ProjectDtos);
        }

        [TestCaseSource(typeof(ProjectsServiceTests), nameof(CorrectIdentifiersCases))]
        public void Get_ShouldReturnExpectedResult_WhenExistingId(int id)
        {
            _projectRepositoryMock = new Mock<IRepository<Project>>(MockBehavior.Strict);
            _projectRepositoryMock.Setup(p => p.Get(id)).Returns(Projects.Find(x => x.Id == id));
            _testingProjectsService = new ProjectsService(_projectRepositoryMock.Object, _mapper);

            var result = _testingProjectsService.Get(id);

            result.Should().BeEquivalentTo(ProjectDtos.Find(x => x.Id == id));
        }

        [TestCaseSource(typeof(ProjectsServiceTests), nameof(IncorrectIdentifiersCases))]
        public void Get_ShouldReturnNull_WhenNonExistingId(int id)
        {
            _projectRepositoryMock = new Mock<IRepository<Project>>(MockBehavior.Strict);
            Project nullProject = null;
            _projectRepositoryMock.Setup(p => p.Get(id)).Returns(nullProject);
            _testingProjectsService = new ProjectsService(_projectRepositoryMock.Object, _mapper);

            var result = _testingProjectsService.Get(id);

            result.Should().BeNull();
        }

        [TestCaseSource(typeof(ProjectsServiceTests), nameof(CorrectIdentifiersCases))]
        public void Delete_ShouldReduceRepositoryCapacityCountByOne_WhenExistingId(int id)
        {
            var deleted = false;
            _projectRepositoryMock = new Mock<IRepository<Project>>(MockBehavior.Strict);
            _projectRepositoryMock.Setup(p => p.Delete(id)).Callback(() =>
            {
                //current stub
                deleted = Projects.Exists(x => x.Id == id);
            });
            _testingProjectsService = new ProjectsService(_projectRepositoryMock.Object, _mapper);

            _testingProjectsService.Delete(id);

            deleted.Should().BeTrue();
        }

        [TestCaseSource(typeof(ProjectsServiceTests), nameof(IncorrectIdentifiersCases))]
        public void Delete_ShouldNotChangeRepository_WhenNonExistingId(int id)
        {
            var deleted = false;
            _projectRepositoryMock = new Mock<IRepository<Project>>(MockBehavior.Strict);
            _projectRepositoryMock.Setup(p => p.Delete(id)).Callback(() =>
            {
                //current stub
                deleted = Projects.Exists(x => x.Id == id);
            });
            _testingProjectsService = new ProjectsService(_projectRepositoryMock.Object, _mapper);

            _testingProjectsService.Delete(id);

            deleted.Should().BeFalse();
        }
    }
}

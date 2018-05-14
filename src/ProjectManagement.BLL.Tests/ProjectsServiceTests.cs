using AutoMapper;
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

        private static object[] EntitiesExistingIdentifiersCases = new object[]
        {
            new object[]
            {
                new Project
                {
                    Id = 1,
                    Name = "Project11111",
                    CreatureDate = new DateTime(2001, 1, 1),
                    Information = "Info11111",
                    ShortInformation = "Short info11111"
                },
                new ProjectDto
                {
                    Id = 1,
                    Name = "Project11111",
                    CreatureDate = new DateTime(2001, 1, 1),
                    Information = "Info11111",
                    ShortInformation = "Short info11111"
                }
            },
            new object[]
            {
                new Project
                {
                    Id = 2,
                    Name = "Project22222",
                    CreatureDate = new DateTime(2002, 2, 2),
                    Information = "Info22222",
                    ShortInformation = "Short info22222"
                },
                new ProjectDto
                {
                    Id = 2,
                    Name = "Project11111",
                    CreatureDate = new DateTime(2002, 2, 2),
                    Information = "Info22222",
                    ShortInformation = "Short info22222"
                }
            }
        };
        private static object[] EntitiesNonExistingIdentifiersCases = new object[]
        {
            new object[]
            {
                new Project
                {
                    Id = 4,
                    Name = "Project4",
                    CreatureDate = new DateTime(2004, 4, 4),
                    Information = "Info4",
                    ShortInformation = "Short info4"
                },
                new ProjectDto
                {
                    Id = 4,
                    Name = "Project4",
                    CreatureDate = new DateTime(2004, 4, 4),
                    Information = "Info4",
                    ShortInformation = "Short info4"
                }
            },
            new object[]
            {
                new Project
                {
                    Id = 5,
                    Name = "Project55555",
                    CreatureDate = new DateTime(2005, 5, 5),
                    Information = "Info55555",
                    ShortInformation = "Short info55555"
                },
                new ProjectDto
                {
                    Id = 5,
                    Name = "Project55555",
                    CreatureDate = new DateTime(2005, 5, 5),
                    Information = "Info55555",
                    ShortInformation = "Short info55555"
                }
            }
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

        [TestCaseSource(typeof(ProjectsServiceTests), nameof(EntitiesExistingIdentifiersCases))]
        public void Update_ShouldChangeItemInRepository_WhenExistingIdInRepository(Project project, ProjectDto projectDto)
        {
            var updated = false;
            _projectRepositoryMock = new Mock<IRepository<Project>>(MockBehavior.Strict);
            _projectRepositoryMock.Setup(p => p.Update(project)).Callback(() =>
            {
                updated = Projects.Exists(x => x.Id == project.Id);
            });
            _testingProjectsService = new ProjectsService(_projectRepositoryMock.Object, _mapper);

            _testingProjectsService.Update(projectDto);

            updated.Should().BeTrue();
        }

        [TestCaseSource(typeof(ProjectsServiceTests), nameof(EntitiesNonExistingIdentifiersCases))]
        public void Update_ShouldNotChangeRepository_WhenNonExistingIdInRepository(Project project, ProjectDto projectDto)
        {
            var updated = false;
            _projectRepositoryMock = new Mock<IRepository<Project>>(MockBehavior.Strict);
            _projectRepositoryMock.Setup(p => p.Update(project)).Callback(() =>
            {
                updated = Projects.Exists(x => x.Id == project.Id);
            });
            _testingProjectsService = new ProjectsService(_projectRepositoryMock.Object, _mapper);

            _testingProjectsService.Update(projectDto);

            updated.Should().BeFalse();
        }

        [TestCaseSource(typeof(ProjectsServiceTests), nameof(EntitiesExistingIdentifiersCases))]
        public void Create_ShouldNotChangeRepository_WhenExistingIdInRepository(Project project, ProjectDto projectDto)
        {
            var created = false;
            _projectRepositoryMock = new Mock<IRepository<Project>>(MockBehavior.Strict);
            _projectRepositoryMock.Setup(p => p.Create(project)).Callback(() =>
            {
                created = !Projects.Exists(x => x.Id == project.Id);
            });
            _testingProjectsService = new ProjectsService(_projectRepositoryMock.Object, _mapper);

            _testingProjectsService.Create(projectDto);

            created.Should().BeTrue();
        }

        [TestCaseSource(typeof(ProjectsServiceTests), nameof(EntitiesNonExistingIdentifiersCases))]
        public void Create_ShouldCreateNewItemInRepository_WhenNonExistingIdInRepository(Project project, ProjectDto projectDto)
        {
            var created = false;
            _projectRepositoryMock = new Mock<IRepository<Project>>(MockBehavior.Strict);
            _projectRepositoryMock.Setup(p => p.Create(project)).Callback(() =>
            {
                created = !Projects.Exists(x => x.Id == project.Id);
            });
            _testingProjectsService = new ProjectsService(_projectRepositoryMock.Object, _mapper);

            _testingProjectsService.Create(projectDto);

            created.Should().BeFalse();
        }
    }
}

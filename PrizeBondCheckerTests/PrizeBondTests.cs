using Application.Models.TestModel;
using Autofac.Extras.Moq;
using Domain.Prizebond;
using Infrastructure.Repository.Base;
using Moq;
using PrizeBondChecker.Services;
using Shouldly;

namespace PrizeBondCheckerTests
{
    public class Tests
    {
        private AutoMock _mock;   
        private BondManagement _bondManagement;
        private Mock<IRepository<UserPrizebonds>> _dbContextMock;
        [OneTimeSetUp]  // run once for all setup
        public void OneTimeSetUp()
        {
            _mock = AutoMock.GetLoose();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _mock?.Dispose();
        }

        [SetUp] // Run each time before test run
        public void Setup()
        {
            _dbContextMock = _mock.Mock<IRepository<UserPrizebonds>>();
            _bondManagement = _mock.Create<BondManagement>(); //create actual instances
        }

        [TearDown]
        public void Cleanup()
        {
            _dbContextMock?.Reset(); // Dispose mock objects
        }

        //[Test]
        //public async Task AddBond_EntityMissing_ThrowsException()
        //{
        //    var result = await _prizebondService.AddBondToList();
        //    //Assert.Pass();
        //}

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void CreateBond_SerialMissing_ThrowsException(string title)
        {
            // Arrange
            var bondId = 102101;
            var collectionDate = new DateTime(2022, 01, 10);

            // Act
            Should.Throw<InvalidOperationException>(() =>
                _bondManagement.AddBond(bondId, title, collectionDate));
        }

        [Test]
        public void CreateBond_ValidBondValues_CreateBond()
        {
            // Arrange
            var bondId = 102101;
            var title = "BD";
            var collectionDate = new DateTime(2022, 01, 10);

            // Act
            _bondManagement.AddBond(bondId, title, collectionDate);
        }

        [Test]
        public void CreateActualBond_ValidBondValues_CreateActualBond()
        {
            // Arrange
            var bondId = "102101";
            var serial = "BD";
            var userId = new Guid();

            // create dynamic implementation on the fly
            _dbContextMock.Setup(x=> x.InsertOne(It.Is<UserPrizebonds>(y=> y.UserId == userId
             && y.BondId == bondId && y.Serial == serial))).Verifiable();

            // Act
            _bondManagement.AddActualBond(bondId, serial, userId);

            // Assert
            _dbContextMock.Verify();
        }
    }
}
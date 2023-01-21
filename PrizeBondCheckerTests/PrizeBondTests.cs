using Application.Models.TestModel;
using PrizeBondChecker.Services;
using Shouldly;

namespace PrizeBondCheckerTests
{
    public class Tests
    {
        private PrizebondService _prizebondService;
        [SetUp]
        public void Setup()
        {
            //_prizebondService = prizebondService;
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
            //var title = string.Empty;
            var collectionDate = new DateTime(2022, 01, 10);

            // Act
            var bondManagement = new BondManagement();
            Should.Throw<InvalidOperationException>(() =>
                bondManagement.AddBond(bondId, title, collectionDate));
        }

        public void CreateBond_ValidBondValues_CreateBond()
        {
            // Arrange
            var bondId = 102101;
            var title = "BD";
            var collectionDate = new DateTime(2022, 01, 10);

            // Act
            var bondManagement = new BondManagement();
 
            bondManagement.AddBond(bondId, title, collectionDate);
        }
    }
}
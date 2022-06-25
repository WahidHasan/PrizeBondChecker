using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using PrizeBondChecker.Controllers;
using PrizeBondChecker.Domain.Prizebond;
using PrizeBondChecker.Services;
using PrizeBondChecker.Test.MockData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PrizeBondChecker.Test.systems.controllers
{
    public class TestPrizebondController
    {
        [Fact]
        public async Task GetALLPrizebond_ShouldReturn200Status()
        {
            var prizebondService = new Mock<IPrizebondService>();
            prizebondService.Setup(_ => _.GetAllAsync()).ReturnsAsync(PrizebondMockdata.GetAll());
            var sut = new PrizebondController(prizebondService.Object);

            //ACT
            var result = await sut.Get();

            Assert.IsType<List<Prizebond>>(result);
        }
    }
}

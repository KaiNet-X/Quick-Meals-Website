using Microsoft.AspNetCore.Http;
using Moq;
using QuickMeals.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Quickmeals_test
{
    //cannot mock extention methods, cannot sucsessfilly unit test
    public class AuthenticationHandlerTest
    {
        //wont work due to extension method
        [Fact]
        public void ReturnsAnnonymousUserIfNull()
        {
            Mock<ISession> session = new Mock<ISession>();
            session.Setup(s => s.GetObject<User>("USER")).Returns(delegate () { return null; });

            User user = AuthenticationHandler.CurrentUser(session.Object);

            Assert.Equal(new User { Username = "", Password = "", RoleID = 0 }, user);
        }
    }
}

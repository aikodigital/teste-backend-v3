using Domain.Enums;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UnitTests.ValueObjects
{
    public class TypesTests
    {
        [Test]
        public void ShouldReturnCorrectType()
        {
            var typeString = "Tragedy";
            var type = Enum.Parse<Types>(typeString);

            Assert.Equals(Types.Tragedy, type);
        }

        [Test]
        public void ShouldReturnCorrectTypeForComedy()
        {
            var typeString = "Comedy";
            var type = Enum.Parse<Types>(typeString);   

            Assert.Equals(Types.Comedy, type);
        }

        [Test]
        public void ShouldReturnCorrectTypeForHistory()
        {
            var typeString = "Drama";
            var type = Enum.Parse<Types>(typeString);

            Assert.Equals(Types.History, type);
        }
    }
}

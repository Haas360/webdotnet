using System.Collections.Generic;
using NUnit.Framework;
using Umbraco.Core.Persistence.FaultHandling;
using Webdotnet.Custom.Core.Helpers;

namespace Webdotnet.Tests.ExtensionsSpecs
{
    [TestFixture]
    public class EnumerableIsNullOrEmptyExtensionTests
    {
        [Test]
        public void ShouldReturnFalseIfListIsEmpty()
        {
            var emptyList = new List<string>();
            Assert.IsTrue(emptyList.IsNullOrEmpty());
        }
        [Test]
        public void ShouldReturnFalseIfNull()
        {
            List<string> emptyList = null;
            Assert.IsTrue(emptyList.IsNullOrEmpty());
        }
        [Test]
        public void ShouldReturnTrueIfListContainsSomething()
        {
            var list = new List<int>() {1,2};
            Assert.IsFalse(list.IsNullOrEmpty());
        }
    }
    [TestFixture]
    public class EnumerableIsNotNullOrEmptyExtensionTests
    {
        [Test]
        public void ShouldReturnTrueIfListIsEmpty()
        {
            var emptyList = new List<string>();
            Assert.IsFalse(emptyList.IsNotNullOrEmpty());
        }
        [Test]
        public void ShouldReturnTrueIfNull()
        {
            List<string> emptyList = null;
            Assert.IsFalse(emptyList.IsNotNullOrEmpty());
        }
        [Test]
        public void ShouldReturnFalseIfListContainsSomething()
        {
            var list = new List<int>() { 1, 2 };
            Assert.IsTrue(list.IsNotNullOrEmpty());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalyticHierarchyProcessDSS.Entities;
using AnalyticHierarchyProcessDSS.WolframEngine;
using AnalyticHierarchyProcessDSS.WolframEngine.Mathematica;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnalyticHierarchyProcessDSS.Tests.EigenVectorTests
{
    [TestClass]
    public class EigenVectorTests
    {
        [TestMethod]
        public void ToStringReturnsCorrectResult()
        {
            // arrange
            var vector = new EigenVector(1, 2, 34, 0.25);
            
            // act
            string expected = "{1, 2, 34, 0.25}";
            string actual = vector.ToString();

            // assert
            Assert.AreEqual(actual,expected);
        }

        [TestMethod]
        public void ToStringEmptyArraySuccess()
        {
            // arrange
            var vector = new EigenVector();

            // act
            string expected = "{}";
            string actual = vector.ToString();

            // assert
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void GetRealUnit()
        {
            // arrange
            var complexNumber = new ComplexNumber("-0.05895753568897655 - 0.6943956705921231*I");

            //// act
            double expected = -0.05895753568897655;
            double actual = complexNumber.Real;

            //// assert
            Assert.AreEqual(actual, expected);
        }
    }
}

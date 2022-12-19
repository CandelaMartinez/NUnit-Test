using Loans.Domain.Applications;
using NUnit.Framework;
using System;

namespace Loans.Test
{
    [TestFixture]
    public class LoanTermShould
    {
        [Test]
        [Ignore("Reason why I am ignoring this test.")]
        public void ReturnTermInMonths()
        {
            //system under test
            var sut = new LoanTerm(1);

            Assert.That(sut.ToMonths(), Is.EqualTo(12),
                "months should be 12 * number of years");


        }

        [Test]
        public void StoreYears()
        {
            var sut = new LoanTerm(1);
            Assert.That(sut.Years, Is.EqualTo(1));
        }

        [Test]
        public void RespectValueEquality()
        {
            var a = new LoanTerm(1);
            var b = new LoanTerm(1);

            Assert.That(a, Is.EqualTo(b));
        }

        [Test]
        public void RespectValueInequality()
        {
            var a = new LoanTerm(1);
            var b = new LoanTerm(2);

            Assert.That(a, Is.Not.EqualTo(b));
        }
        [Test]
        public void Double()
        {
            double a = 1.0 / 3.0;

            Assert.That(a, Is.EqualTo(0.33).Within(0.004));

            Assert.That(a, Is.EqualTo(0.33).Within(10).Percent);

        }

        [Test]
        public void NotAllowZeroYears()
        {
            Assert.That(() => new LoanTerm(0),
                                Throws.TypeOf<ArgumentOutOfRangeException>());

            Assert.That(() => new LoanTerm(0),
                               Throws.TypeOf<ArgumentOutOfRangeException>()
                                        .With
                                        .Matches<ArgumentOutOfRangeException>(
                                            ex => ex.ParamName == "years"
                                   )
                               );


        }

    }
}

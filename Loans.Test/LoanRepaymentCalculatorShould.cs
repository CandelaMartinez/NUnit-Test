using Loans.Domain.Applications;
using NUnit.Framework;

namespace Loans.Test
{
    public class LoanRepaymentCalculatorShould
    {
        [Test]
        [TestCase(200_000, 6.5, 30, 1264.14)]
        [TestCase(200_000, 10, 30, 1755.14)]
        public void CalculateCorrectMonthlyRepayment(decimal principal,
                                                     decimal interestRate,
                                                     int termInYears,
                                                     decimal expectedMonthlyPayment)
        {
            var sut = new LoanRepaymentCalculator();

            var monthlyPayment = sut.CalculateMonthlyRepayment(
                                    new LoanAmount("USD", principal),
                                    interestRate, new LoanTerm(termInYears));

            Assert.That(monthlyPayment, Is.EqualTo(expectedMonthlyPayment));
        }


        [Test]
        [TestCase(200_000, 6.5, 30, ExpectedResult = 1264.14)]
        [TestCase(200_000, 10, 30, ExpectedResult= 1755.14)]
        public decimal CalculateCorrectMonthlyRepayment_Simplified(decimal principal,
                                                   decimal interestRate,
                                                   int termInYears)
        {
            var sut = new LoanRepaymentCalculator();

            return sut.CalculateMonthlyRepayment(
                                    new LoanAmount("USD", principal),
                                    interestRate, new LoanTerm(termInYears));

        }

        [Test]
        [TestCaseSource(typeof(MonthlyRepaymentTestData), "TestCases")]
        public decimal CalculateCorrectMonthlyRepayment_centralized(decimal principal,
                                                   decimal interestRate,
                                                   int termInYears)
        {
            var sut = new LoanRepaymentCalculator();

            return sut.CalculateMonthlyRepayment(
                                    new LoanAmount("USD", principal),
                                    interestRate, new LoanTerm(termInYears));

        }

        [Test]
        [Ignore("Pending check for decimal formatting")]
        [TestCaseSource(typeof(MonthlyRepaymentCsvData), "GetTestCases", new object[] { "Data.csv"})]
        public void CalculateCorrectMonthlyRepayment_Cvs(decimal principal,
                                                   decimal interestRate,
                                                   int termInYears,
                                                   decimal expectedMonthlyPayment)
        {
            var sut = new LoanRepaymentCalculator();

            var monthlyPayment = sut.CalculateMonthlyRepayment(
                                    new LoanAmount("USD", principal),
                                    interestRate, 
                                    new LoanTerm(termInYears));

            Assert.That(monthlyPayment, Is.EqualTo(expectedMonthlyPayment));
        }

        [Test]
        [Sequential]
        public void CalculateCorrectMonthlyRepayment_Combinatorial(
            [Values(100_000, 200_000, 500_000)] decimal principal,
            [Values(6.5,10,20)] decimal interestRate,
            [Values(10,20,30)] int termInYears
            )
        {
            var sut = new LoanRepaymentCalculator();

            var monthlyPayment = sut.CalculateMonthlyRepayment(
                new LoanAmount("USD", principal),
                interestRate,
                new LoanTerm(termInYears));
        }

        [Test]
        public void CalculateCorrectMonthlyRepayment_Range(
          [Range(50_000, 1_000_000, 50_000)] decimal principal,
          [Range(0.5, 20.00, 0.5)] decimal interestRate,
          [Values(10, 20, 30)] int termInYears
          )
        {
            var sut = new LoanRepaymentCalculator();

            var monthlyPayment = sut.CalculateMonthlyRepayment(
                new LoanAmount("USD", principal),
                interestRate,
                new LoanTerm(termInYears));
        }

    }
}

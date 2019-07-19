using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericsExercise
{
    [TestFixture]
    public class EnumMapperTests
    {
        [Test]
        public void ReturnCorrectGenderWhenIntIsPassed()
        {
            Gender result = EnumMapper.MapValueToEnum<Gender, int>(1);

            Assert.AreEqual(Gender.Male, result);
        }

        [Test]
        public void ReturnCorrectGenderWhenStringIsPassed()
        {
            Gender result = EnumMapper.MapValueToEnum<Gender, string>("Female");

            Assert.IsTrue(Gender.Female == result);
        }

        [Test]
        public void ReturnCorrectWeekdayWhenIntIsPassed()
        {
            Weekday result = EnumMapper.MapValueToEnum<Weekday, int>(4);

            Assert.AreEqual(Weekday.Friday, result);
        }

        [Test]
        public void ReturnCorrectWeekdayWhenStringIsPassed()
        {
            Weekday result = EnumMapper.MapValueToEnum<Weekday, string>("Monday");

            Assert.AreEqual(Weekday.Monday, result);
        }

        [Test]
        public void ThrowExceptionWhenWrongStringPassedToGender()
        {
            string wrongValue = "erhgrw";

            Assert.Throws<Exception>(() => EnumMapper.MapValueToEnum<Gender, string>(wrongValue));
        }

        [Test]
        public void ThrowExceptionWhenWrongStringPassedToWeekday()
        {
            string wrongValue = "ygukmj";

            Assert.Throws<Exception>(() => EnumMapper.MapValueToEnum<Weekday, string>(wrongValue));
        }
    }
}

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericsExercise
{
    [TestFixture]
    class EnumMapperTests
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

            Assert.AreEqual("Female", result);
        }

        [Test]
        public void ReturnCorrectWeekdayWhenIntIsPassed()
        {
            Gender result = EnumMapper.MapValueToEnum<Gender, int>(4);

            Assert.AreEqual("Friday", result);
        }

        [Test]
        public void ReturnCorrectWeekdayWhenStringIsPassed()
        {
            Gender result = EnumMapper.MapValueToEnum<Gender, string>("Monay");

            Assert.AreEqual("Monday", result);
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

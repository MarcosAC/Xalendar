﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Xalendar.Api.Extensions;
using Xalendar.Api.Models;

namespace Xalendar.Api.Tests.Models
{
    [TestFixture]
    public class MonthContainerTests
    {
        [Test]
        public void MonthContainerShouldContainsDaysOfMonth()
        {
            var dateTime = DateTime.Now;

            var monthContainer = new MonthContainer(dateTime);

            Assert.IsNotEmpty(monthContainer.Days);
        }

        [Test]
        public void MonthContainerCanSelectDay()
        {
            var dateTime = new DateTime(2020, 7, 9);
            var monthContainer = new MonthContainer(dateTime);
            var selectedDay = new Day(dateTime, true);

            monthContainer.SelectDay(selectedDay);

            Assert.AreEqual(selectedDay, monthContainer.GetSelectedDay());
        }

        [Test]
        public void MonthContainerShouldNotContainsEvents()
        {
            var dateTime = new DateTime(2020, 7, 9);
            var monthContainer = new MonthContainer(dateTime);

            Assert.IsFalse(monthContainer._month.Days.Any(day => day.HasEvents));
        }

        [Test]
        public void MonthContainerShouldContainsEvents()
        {
            var dateTime = new DateTime(2020, 7, 9);
            var monthContainer = new MonthContainer(dateTime);
            var events = new List<Event>
            {
                new Event(1, "Name event", dateTime, dateTime, false)
            };

            monthContainer.AddEvents(events);

            Assert.IsTrue(monthContainer._month.Days.Any(day => day.HasEvents));
        }

        [Test]
        public void MonthContainerShouldHaveAName()
        {
            var dateTime = new DateTime(2020, 7, 9);
            var monthContainer = new MonthContainer(dateTime);

            var result = monthContainer.GetName();
            var dateTimeName = dateTime.ToString("MMMM");
            Assert.AreEqual(dateTimeName, result);
        }

        [Test]
        public void MonthContainerShouldNavigateToNextMonth()
        {
            var dateTime = new DateTime(2020, 12, 9);
            var monthContainer = new MonthContainer(dateTime);

            monthContainer.Next();

            var dateTimeName = monthContainer._month.MonthDateTime.ToString("MMMM");
            Assert.AreEqual(dateTimeName, monthContainer.GetName());
        }

        [Test]
        public void MonthContainerShouldNavigateToPreviousMonth()
        {
            var dateTime = new DateTime(2021, 1, 1);
            var monthContainer = new MonthContainer(dateTime);

            monthContainer.Previous();

            var dateTimeName = monthContainer._month.MonthDateTime.ToString("MMMM");
            Assert.AreEqual(dateTimeName, monthContainer.GetName());
        }
    }
}

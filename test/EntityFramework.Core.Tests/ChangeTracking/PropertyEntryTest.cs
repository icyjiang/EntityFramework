// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.Data.Entity.ChangeTracking;
using Microsoft.Data.Entity.Internal;
using Microsoft.Data.Entity.Metadata;
using Moq;
using Xunit;

namespace Microsoft.Data.Entity.Tests.ChangeTracking
{
    public class PropertyEntryTest
    {
        #region NonGeneric PropertyEntry Tests

        [Fact]
        public void Members_check_arguments()
        {
            Assert.Equal(
                Strings.ArgumentIsEmpty("name"),
                Assert.Throws<ArgumentException>(() => new PropertyEntry(new Mock<StateEntry>().Object, "")).Message);
            Assert.Equal(
                "stateEntry",
                // ReSharper disable once AssignNullToNotNullAttribute
                Assert.Throws<ArgumentNullException>(() => new PropertyEntry(null, "Kake")).ParamName);
        }

        [Fact]
        public void Can_get_name()
        {
            var stateEntryMock = CreateStateEntryMock(new Mock<IProperty>());

            Assert.Equal("Monkey", new PropertyEntry(stateEntryMock.Object, "Monkey").Name);
        }

        [Fact]
        public void Can_get_current_value()
        {
            var stateEntryMock = CreateStateEntryMock(new Mock<IProperty>());
            stateEntryMock.Setup(m => m[It.IsAny<IProperty>()]).Returns("Chimp");

            Assert.Equal("Chimp", new PropertyEntry(stateEntryMock.Object, "Monkey").CurrentValue);
        }

        [Fact]
        public void Can_set_current_value()
        {
            var property = new Mock<IProperty>();
            var stateEntryMock = CreateStateEntryMock(property);

            new PropertyEntry(stateEntryMock.Object, "Monkey").CurrentValue = "Chimp";

            stateEntryMock.VerifySet(m => m[property.Object] = "Chimp");
        }

        [Fact]
        public void Can_set_current_value_to_null()
        {
            var property = new Mock<IProperty>();
            var stateEntryMock = CreateStateEntryMock(property);

            new PropertyEntry(stateEntryMock.Object, "Monkey").CurrentValue = null;

            stateEntryMock.VerifySet(m => m[property.Object] = null);
        }

        [Fact]
        public void Can_get_original_value()
        {
            var stateEntryMock = CreateStateEntryMock(new Mock<IProperty>());
            stateEntryMock.Setup(m => m.OriginalValues[It.IsAny<IProperty>()]).Returns("Chimp");

            Assert.Equal("Chimp", new PropertyEntry(stateEntryMock.Object, "Monkey").OriginalValue);
        }

        [Fact]
        public void Can_set_original_value()
        {
            var property = new Mock<IProperty>();
            var stateEntryMock = CreateStateEntryMock(property);

            var sideCarMock = new Mock<Sidecar>();
            stateEntryMock.Setup(m => m.OriginalValues).Returns(sideCarMock.Object);

            new PropertyEntry(stateEntryMock.Object, "Monkey").OriginalValue = "Chimp";

            sideCarMock.VerifySet(m => m[property.Object] = "Chimp");
        }

        [Fact]
        public void Can_set_original_value_to_null()
        {
            var property = new Mock<IProperty>();
            var stateEntryMock = CreateStateEntryMock(property);

            var sideCarMock = new Mock<Sidecar>();
            stateEntryMock.Setup(m => m.OriginalValues).Returns(sideCarMock.Object);

            new PropertyEntry(stateEntryMock.Object, "Monkey").OriginalValue = null;

            sideCarMock.VerifySet(m => m[property.Object] = null);
        }

        [Fact]
        public void IsModified_delegates_to_state_object()
        {
            var propertyMock = new Mock<IProperty>();
            var stateEntryMock = CreateStateEntryMock(propertyMock);
            stateEntryMock.Setup(m => m.IsPropertyModified(propertyMock.Object)).Returns(true);

            var propertyEntry = new PropertyEntry(stateEntryMock.Object, "Monkey");

            Assert.True(propertyEntry.IsModified);
            stateEntryMock.Verify(m => m.IsPropertyModified(propertyMock.Object));

            propertyEntry.IsModified = true;

            stateEntryMock.Verify(m => m.SetPropertyModified(propertyMock.Object, true));
        }
        
        #endregion

        #region Generic PropertyEntry Tests

        [Fact]
        public void Members_check_arguments_generic()
        {
            Assert.Equal(
                Strings.ArgumentIsEmpty("name"),
                Assert.Throws<ArgumentException>(() => new PropertyEntry<Random, int>(new Mock<StateEntry>().Object, "")).Message);
            Assert.Equal(
                "stateEntry",
                // ReSharper disable once AssignNullToNotNullAttribute
                Assert.Throws<ArgumentNullException>(() => new PropertyEntry<Random, int>(null, "Kake")).ParamName);
        }

        [Fact]
        public void Can_get_name_generic()
        {
            var stateEntryMock = CreateStateEntryMock(new Mock<IProperty>());

            Assert.Equal("Monkey", new PropertyEntry<Random, string>(stateEntryMock.Object, "Monkey").Name);
        }

        [Fact]
        public void Can_get_current_value_generic()
        {
            var stateEntryMock = CreateStateEntryMock(new Mock<IProperty>());
            stateEntryMock.Setup(m => m[It.IsAny<IProperty>()]).Returns("Chimp");

            Assert.Equal("Chimp", new PropertyEntry<Random, string>(stateEntryMock.Object, "Monkey").CurrentValue);
        }

        [Fact]
        public void Can_set_current_value_generic()
        {
            var property = new Mock<IProperty>();
            var stateEntryMock = CreateStateEntryMock(property);

            new PropertyEntry<Random, string>(stateEntryMock.Object, "Monkey").CurrentValue = "Chimp";

            stateEntryMock.VerifySet(m => m[property.Object] = "Chimp");
        }

        [Fact]
        public void Can_set_current_value_to_null_generic()
        {
            var property = new Mock<IProperty>();
            var stateEntryMock = CreateStateEntryMock(property);

            new PropertyEntry<Random, string>(stateEntryMock.Object, "Monkey").CurrentValue = null;

            stateEntryMock.VerifySet(m => m[property.Object] = null);
        }

        [Fact]
        public void Can_get_original_value_generic()
        {
            var stateEntryMock = CreateStateEntryMock(new Mock<IProperty>());
            stateEntryMock.Setup(m => m.OriginalValues[It.IsAny<IProperty>()]).Returns("Chimp");

            Assert.Equal("Chimp", new PropertyEntry<Random, string>(stateEntryMock.Object, "Monkey").OriginalValue);
        }

        [Fact]
        public void Can_set_original_value_generic()
        {
            var property = new Mock<IProperty>();
            var stateEntryMock = CreateStateEntryMock(property);

            var sideCarMock = new Mock<Sidecar>();
            stateEntryMock.Setup(m => m.OriginalValues).Returns(sideCarMock.Object);

            new PropertyEntry<Random, string>(stateEntryMock.Object, "Monkey").OriginalValue = "Chimp";

            sideCarMock.VerifySet(m => m[property.Object] = "Chimp");
        }

        [Fact]
        public void Can_set_original_value_to_null_generic()
        {
            var property = new Mock<IProperty>();
            var stateEntryMock = CreateStateEntryMock(property);

            var sideCarMock = new Mock<Sidecar>();
            stateEntryMock.Setup(m => m.OriginalValues).Returns(sideCarMock.Object);

            new PropertyEntry<Random, string>(stateEntryMock.Object, "Monkey").OriginalValue = null;

            sideCarMock.VerifySet(m => m[property.Object] = null);
        }

        [Fact]
        public void IsModified_delegates_to_state_object_generic()
        {
            var propertyMock = new Mock<IProperty>();
            var stateEntryMock = CreateStateEntryMock(propertyMock);
            stateEntryMock.Setup(m => m.IsPropertyModified(propertyMock.Object)).Returns(true);

            var propertyEntry = new PropertyEntry<Random, string>(stateEntryMock.Object, "Monkey");

            Assert.True(propertyEntry.IsModified);
            stateEntryMock.Verify(m => m.IsPropertyModified(propertyMock.Object));

            propertyEntry.IsModified = true;

            stateEntryMock.Verify(m => m.SetPropertyModified(propertyMock.Object, true));
        }
        
        #endregion

        #region Helper Functions

        private static Mock<StateEntry> CreateStateEntryMock(Mock<IProperty> propertyMock)
        {
            propertyMock.Setup(m => m.Name).Returns("Monkey");

            var entityTypeMock = new Mock<IEntityType>();
            entityTypeMock.Setup(m => m.GetProperty("Monkey")).Returns(propertyMock.Object);

            var stateEntryMock = new Mock<StateEntry>();
            stateEntryMock.Setup(m => m.EntityType).Returns(entityTypeMock.Object);
            return stateEntryMock;
        } 

        #endregion
    }
}

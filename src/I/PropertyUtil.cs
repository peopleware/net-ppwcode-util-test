// Copyright 2017 by PeopleWare n.v..
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

namespace PPWCode.Util.Test.I
{
    /// <summary>
    ///     Class with utility methods for use in tests.
    /// </summary>
    public class PropertyUtil
    {
        /// <summary>
        ///     Test the validity of a value of a property.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This test method calls the getter of a validity property on a subject under test.
        ///         The getter of a validity of a property should have the appropriate contract.
        ///         If the contract fails, the test will fail.
        ///     </para>
        ///     <para>
        ///         <paramref name="subject" /> <strong>must</strong> have a property with name
        ///         <paramref name="propertyName" />.
        ///     </para>
        /// </remarks>
        /// <typeparam name="C">The type of the subject under test.</typeparam>
        /// <param name="subject">
        ///     The subject (this) under test. Never null.
        ///     This object should not violate preconditions for the getter of the validity of a property under test.
        /// </param>
        /// <param name="propertyName">
        ///     The name (without the extension "Valid") of the validity property
        ///     to test the getter of.
        /// </param>
        public static void TestPropertyValid<C>(C subject, string propertyName)
            where C : class
        {
            Contract.Requires(subject != null);
            Contract.Requires(!string.IsNullOrEmpty(propertyName));
            Contract.Requires(subject.GetType().GetProperties().Count(propInfo => propInfo.Name == propertyName + "Valid") == 1);

            PropertyInfo propertyInfo = subject.GetType().GetProperties().Single(propInfo => propInfo.Name == propertyName + "Valid");
            propertyInfo.GetValue(subject);
        }

        /// <summary>
        ///     Test the setter of a property.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This test method calls the setter of a given property with given value on
        ///         a subject under test. The setter should have the appropriate contract. If the contract fails,
        ///         the test will fail.
        ///     </para>
        ///     <para>
        ///         <paramref name="subject" /> <strong>must</strong> have a property with name
        ///         <paramref name="propertyName" />.
        ///     </para>
        /// </remarks>
        /// <typeparam name="C">The type of the subject under test.</typeparam>
        /// <typeparam name="P">The type of the property under test.</typeparam>
        /// <param name="subject">
        ///     The subject (this) under test. Never null.
        ///     This object should not violate preconditions for the setter under test.
        /// </param>
        /// <param name="propertyName">The name of the property to test the setter of.</param>
        /// <param name="propertyValue">
        ///     The new value of the property to be set by this test.
        ///     This value should not violate preconditions for the setter under test.
        /// </param>
        public static void TestPropertySet<C, P>(C subject, string propertyName, P propertyValue)
            where C : class
        {
            Contract.Requires(subject != null);
            Contract.Requires(!string.IsNullOrEmpty(propertyName));
            Contract.Requires(subject.GetType().GetProperties().Count(propInfo => propInfo.Name == propertyName) == 1);

            PropertyInfo propertyInfo = subject.GetType().GetProperties().Single(propInfo => propInfo.Name == propertyName);
            propertyInfo.SetValue(subject, propertyValue);
        }
    }
}
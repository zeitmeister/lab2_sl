using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomDatastructures.Core;
using CustomDatastructures.Tests;

namespace CustomCollections.Tests
{
    [TestClass()]
    public class ObservableListTests : TestClass<IObservableList<object>, IObservableList>
    {
        public ObservableListTests() : base()
        {
            Initialize();
        }

        public ObservableListTests(IObservableList<object> instance, Type actualType)
            : base(instance, actualType)
        {
            Initialize();
        }

        /* 
         * Start tests
         */

        [TestInitialize]
        public void Test_Initialize()
        {
            base.Initialize();
        }

        [TestCleanup]
        public void TearDown()
        {
            Instance = null;
        }

        [TestMethod()]
        public void Implementing_IObservableList()
        {
            Assert.IsInstanceOfType(Instance, typeof(IObservableList<object>));
        }

        [TestMethod]
        public void Test_Contains()
        {
            var testObj = new Object();

            bool result = Instance.Contains(testObj);

            Assert.IsFalse(result);

            Instance.Add(testObj);

            result = Instance.Contains(testObj);

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void Test_Add()
        {
            var testObj = new Object();

            Instance.Add(testObj);

            var result = Instance.Contains<object>(testObj);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_Remove()
        {
            var testObj = new Object();

            Instance.Add(testObj);
            Instance.Remove(testObj);

            var result = Instance.Contains<object>(testObj);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Add_Throws_Correct_Exception()
        {
            var testObj = new Object();

            Instance.BeforeChange +=
                (sender, args) => args.RejectOperation();

            var ex = Assert.ThrowsException<InvalidOperationException>(() => Instance.Add(testObj));

            Assert.IsInstanceOfType(ex, typeof(InvalidOperationException));
        }

        [TestMethod]
        public void Remove_Reject_Throws_Correct_Exception()
        {
            var testObj = new Object();
            // Add it before try to remove it otherwise 
            // another exception will be thrown
            Instance.Add(testObj);
            Instance.BeforeChange += (sender, args) => args.RejectOperation();

            try
            {
                Instance.Remove(testObj);
            }
            catch (InvalidOperationException e)
            {
                if (e.GetType().BaseType == typeof(InvalidOperationException))
                {
                    return;

                }
                else
                {
                    Assert.Fail("When an operation is rejected, the type of the exception should be a subtype of InvalidOperationException.");
                    return;
                }
            }
            Assert.Fail("When an operation is rejected, the Remove method should throw an exception.");
        }

        [TestMethod]
        public void Remove_Nonexistent_Throws_Correct_Exception()
        {
            var testObj = new Object();

            // Checks that the thrown exception is or inherits from InvalidOperationException
            var ex = Assert.ThrowsException<InvalidOperationException>(() => Instance.Remove(testObj));
        }

        [TestMethod]
        public void Test_TryAdd_Success()
        {
            var testObj = new Object();

            var operationSuccess = Instance.TryAdd(testObj);

            var result = Instance.SingleOrDefault(o => o == testObj);

            Assert.AreEqual(testObj, result);
            Assert.IsTrue(operationSuccess);
        }

        [TestMethod]
        public void Test_TryAdd_Failure()
        {
            var testObj = new Object();

            Instance.BeforeChange +=
                (sender, args) => args.RejectOperation();

            var operationSuccess = Instance.TryAdd(testObj);

            var result = Instance.SingleOrDefault(o => o == testObj);

            Assert.AreEqual(null, result);
            Assert.IsFalse(operationSuccess);
        }

        [TestMethod]
        public void Test_TryRemove_Success()
        {
            var testObj = new Object();

            Instance.Add(testObj);

            var operationSuccess = Instance.TryRemove(testObj);

            var result = Instance.SingleOrDefault(o => o == testObj);

            Assert.AreEqual(null, result);
            Assert.IsTrue(operationSuccess);
        }


        [TestMethod]
        public void Test_TryRemove_Failure()
        {
            var testObj = new Object();

            bool operationSuccess = true;

            if (!Instance.Contains<object>(testObj))
                operationSuccess = Instance.TryRemove(testObj);

            Assert.IsFalse(operationSuccess);

            Instance.Add(testObj);

            Instance.BeforeChange +=
                (sender, args) => args.RejectOperation();

            operationSuccess = Instance.TryRemove(testObj);

            var result = Instance.SingleOrDefault(o => o == testObj);

            Assert.AreEqual(testObj, result);
            Assert.IsFalse(operationSuccess);
        }

        [TestMethod]
        public void Test_Changed_Event()
        {
            int hitCount = 0;

            Instance.Changed +=
                (sender, listArgs) =>
                {
                    hitCount++;
                };

            object o1 = new Object();

            Instance.Add(o1);
            Instance.Remove(o1);

            Assert.AreEqual(2, hitCount);
        }

        [TestMethod]
        public void Test_BeforeChange_Event()
        {
            int hitCount = 0;

            Instance.BeforeChange +=
                (sender, listArgs) =>
                {
                    hitCount++;
                };

            object o1 = new Object();

            Instance.Add(o1);
            Instance.Remove(o1);

            Assert.AreEqual(2, hitCount);
        }

        [TestMethod]
        public void Test_ChangeEventArgs_Add()
        {
            var testObj = new Object();

            var result = GetEventArgs(Instance, () => Instance.Add(testObj));

            Assert.AreEqual(Operation.Add, result.Item1);
            Assert.AreEqual(Instance, result.Item2);
            Assert.AreEqual(Instance.Count(), result.Item3);
        }


        [TestMethod]
        public void Test_ChangeEventArgs_Remove()
        {
            var testObj = new Object();
            Instance.Add(testObj);

            var result = this.GetEventArgs(Instance, () => Instance.Remove(testObj));

            Assert.AreEqual(Operation.Remove, result.Item1);
            Assert.AreEqual(Instance, result.Item2);
            Assert.AreEqual(Instance.Count(), result.Item3);
        }

        private Tuple<Operation, object, int> GetEventArgs(IObservableList<object> observableList, Action action)
        {
            Operation actualOperation = (Operation)(-1);
            object actualSender = null;
            int actualCount = -1;

            observableList.Changed +=
                (sender, listArgs) =>
                {
                    actualOperation = listArgs.Operation;
                    actualSender = sender;
                    actualCount = listArgs.Count;
                };

            action();

            return new Tuple<Operation, object, int>(actualOperation, actualSender, actualCount);
        }

        [TestMethod]
        public void CustomArgs_Implements_RejectableEventArgs()
        {
            Instance.BeforeChange +=
                (sender, listArgs) =>
                {
                    try
                    {
                        // make sure that it inherits from RejectableEventArgs
                        var x = listArgs;

                        // Checks that the thrown exception is not a RejectableEventArgs
                        Assert.IsInstanceOfType(x, typeof(RejectableEventArgs<object>));
                    }
                    catch (Exception ex)
                    {
                        Assert.Fail(ex.Message);
                    }
                };

            Instance.Add(new object());
        }
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Models;
using System;
using System.Collections.Generic;




namespace ToDoList.Tests
{
  [TestClass]
  public class TaskTest : IDisposable
  {

    public TaskTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=mytodo_test;";
        }
        public void Dispose()
        {
          Task.DeleteAll();
          Category.DeleteAll();
        }

        [TestMethod]
        public void GetAll_DatabaseEmptyAtFirst_0()
        {
          //Arrange, Act
          int result = Task.GetAll().Count;

          //Assert
          Assert.AreEqual(0, result);
        }

        [TestMethod]
          public void Equals_OverrideTrueForSameDescription_Task()
          {
            //Arrange, Act
            Task firstTask = new Task("Mow the lawn", "2017-01-01",1);
            Task secondTask = new Task("Mow the lawn","2017-01-01",1);

            //Assert
            Assert.AreEqual(firstTask, secondTask);
          }

          [TestMethod]
        public void Save_SavesTaskToDatabase_TaskList()
        {
          //Arrange
          Task testTask = new Task("Mow the lawn","2017-01-01",1);


          //Act
          testTask.Save();
          List<Task> result = Task.GetAll();
          List<Task> testList = new List<Task>{testTask};

          //Assert
          CollectionAssert.AreEqual(testList, result);
        }
       [TestMethod]
        public void Save_DatabaseAssignsIdToObject_Id()
        {
          //Arrange
          Task testTask = new Task("Mow the lawn","2017-01-01",1);


          //Act
          testTask.Save();
          Task savedTask = Task.GetAll()[0];

          int result = savedTask.GetId();
          int testId = testTask.GetId();

          //Assert
          Assert.AreEqual(testId, result);
        }

        [TestMethod]
        public void Find_FindsTaskInDatabase_Task()
        {
          //Arrange
          Task testTask = new Task("Mow the lawn","2017-01-01",1);
          testTask.Save();

          //Act
          Task foundTask = Task.Find(testTask.GetId());

          //Assert
          Assert.AreEqual(testTask, foundTask);
        }

        [TestMethod]
        public void Update_UpdatesTaskInDatabase_String()
        {
          //Arrange
          string description = "Walk the Dog";
          Task testTask = new Task(description, "2017-01-01",1);
          testTask.Save();
          string newDescription = "Mow the lawn";

          //Act
          testTask.UpdateDescription(newDescription);

          string result = Task.Find(testTask.GetId()).GetDescription();

          //Assert
          Assert.AreEqual(newDescription, result);
        }

        [TestMethod]
        public void DeleteTask_DeleteTaskInDatabase_Null()
        {
          //Arrange
          string description = "Feed the Fish";
          Task testTask = new Task(description, "2017-01-01",1);
          testTask.Save();
          // string deletedTask = "";

          //Act
          testTask.DeleteTask();
          int result = Task.GetAll().Count;

          //Assert
          Assert.AreEqual(0, result);
      }

    }

}

//--------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTest1.cs" company="BridgeLabz">
// copyright @2019 
// </copyright>
// <creater name="Nikita Sonawane"/>
//------------------------------------------------------------------------------------------------------------------
namespace XUnitTestProject1
{
    using FundooNotesApp.Repository;
    using FundooNotesApp.Interface;   
    using FundooNotesApp.Model;
    using FundooNotesApp.View;
    using Moq;
    using Xunit;

    /// <summary>
    /// For unit testing use this class
    /// </summary>
    public class UnitTest1
    {
        /// <summary>
        /// Test1 this instance.
        /// </summary>
        [Fact]
        public void Test1()
        {
            Assert.True(true);
        }

        /// <summary>
        /// test case for Create the user.
        /// </summary>
        [Fact]
        public void CreateUser()
        {
            //// arrange
            var service = new Mock<IDatabaseInterface>();
            var user = new UserRepository();

            var adduser = new RegisterUser
            {
                Firstname = "Nikita",
                Lastname = "sonawane",
                UserName = "sonawanenikita2@gmail.com",
                Password = "abc@12354",
                Confirmpassword = "abc@12354"
            };

            //// act
            var data = user.SubmitUserDetails(adduser.Firstname, adduser.Lastname, adduser.UserName, adduser.Password, adduser.Confirmpassword);

            //// assert  
            Assert.NotNull(data);
        }

        /// <summary>
        /// test case for Create the note.
        /// </summary>
        [Fact]
        public void CreateNote()
        {
            //// arrange

            var user = new NotesRepository();

            var addNotes = new Note
            {
                Title = "Main",
                UserNote = "Hii , This is my first Note"
            };

            //// act
         //   var data = user.AddNote(addNotes);

            //// assert
          //  Assert.NotNull(data);
        }
    }
}

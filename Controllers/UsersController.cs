//This is the Controller for the Users http://localhost:2002/api/users
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace AspNetSelfHostDemo
{
    //simple object to store a user
    public class User
    {
        public int userID { get; set; }
        public string userName { get; set; }
    }

    //now create a contorller for our /users service
    //enforce basic auth for access to the users class
    [BasicAuthentication]
    public class UsersController : ApiController
    {
        private static List<User> Users; //just using a simple List object to store users in

        private static int idCounter = 4; // counter for adding products to give them a unique id

        // Consturctor - just used to add some default users to the user list (3 users)
        static UsersController()
        {
            Users = new List<User>();
            Users.Add(new User() { userID = 1, userName = "Bob Jones" });
            Users.Add(new User() { userID = 2, userName = "Jane Smith" });
            Users.Add(new User() { userID = 3, userName = "Jen Booth" });
        }
        // GET api/users
        //[HttpGet]
        public IEnumerable<User> Get()
        {
            return Users; //simply return our users List object, the service does the rest (converts to json)
        }

        // GET api/users/2
        public User Get(int id)
        {
            //get the first one, as there can be duplicate product ids
            var user = Users.FirstOrDefault(p => p.userID == id);
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound); //404
            }
            return user;
        }

        // POST api/users
        public HttpResponseMessage Post([FromBody()] User user)
        {
            try
            {
                user.userID = idCounter; //set the user ID to our current counter
                Users.Add(user);
                idCounter++; //increase the counter so next user created has unique ID
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, user); // 201
                return response;
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest); // 400
            }
        }

        // DELETE api/users/2
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                Users.Remove(Users.Single(p => p.userID == id));
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, id); // 200
                return response;
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest); // 400
            }
        }


    }
}

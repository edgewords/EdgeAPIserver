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
    public class User
    {
        public int userID { get; set; }
        public string userName { get; set; }
    }

    //enforce basic auth for any of the users class
    [BasicAuthentication]
    public class UsersController : ApiController
    {
        private static List<User> Users;

        private static int idCounter = 4; // counter for adding products to give them a unique id

        // Consturctor - just used to add some default users to the user list
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
            return Users;
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
                user.userID = idCounter;
                Users.Add(user);
                idCounter++;
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

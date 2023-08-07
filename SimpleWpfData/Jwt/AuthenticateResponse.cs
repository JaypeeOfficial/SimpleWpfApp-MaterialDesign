using SimpleWpfData.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWpfData.Jwt
{
    public class AuthenticateResponse
    {


        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    //    public string Token { get; set; }


        public AuthenticateResponse(User user)
        {
            Id = user.Id;
            FullName = user.FullName;
            UserName = user.UserName;
            Password = user.Password;
           // Token = token;
        }


    }
}

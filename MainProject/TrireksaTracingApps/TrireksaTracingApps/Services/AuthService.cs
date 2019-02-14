﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrireksaTracingApps.Models;

namespace TrireksaTracingApps.Services
{
    public class AuthService : IAuthService<AuthenticationToken>
    {
        public async Task<AuthenticationToken> Login(string user, string password)
        {
            using (var res= new RestServices())
            {
                try
                {
                    var result = await res.GenerateTokenAsync(user, password);
                    if(res!=null)
                    {
                        return result;
                    }
                    throw new SystemException("Anda Tidak Memiliki AKses");
                }
                catch (Exception ex)
                {

                    throw new SystemException(ex.Message);
                }
            }
        }
    }
}

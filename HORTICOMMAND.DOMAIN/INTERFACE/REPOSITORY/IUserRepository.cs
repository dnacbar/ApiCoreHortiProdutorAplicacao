﻿using HORTICOMMAND.DOMAIN.MODEL;
using System.Threading.Tasks;

namespace HORTICOMMAND.DOMAIN.INTERFACE.REPOSITORY
{
    public interface IUserRepository
    {
        Task CreateUser(Userhorti userhorti);
        Task DeleteUser(Userhorti userhorti);
        Task UpdateUser(Userhorti userhorti);
    }
}
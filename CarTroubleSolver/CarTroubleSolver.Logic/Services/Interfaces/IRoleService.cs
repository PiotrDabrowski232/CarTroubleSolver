﻿using CarTroubleSolver.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTroubleSolver.Logic.Services.Interfaces
{
    public interface IRoleService
    {
        public Role GetRole(Guid roleId);
    }
}

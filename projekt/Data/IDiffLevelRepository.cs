﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projekt.Models;

namespace projekt.Data
{
    public interface IDiffLevelRepository
    {
        IQueryable<DifficultyLevel> DifficultyLevels { get; }
    }
}

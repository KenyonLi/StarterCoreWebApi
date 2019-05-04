using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Starter.WebApi.Filters
{
    public class ValidJtiRequirement : IAuthorizationRequirement
    { }
}

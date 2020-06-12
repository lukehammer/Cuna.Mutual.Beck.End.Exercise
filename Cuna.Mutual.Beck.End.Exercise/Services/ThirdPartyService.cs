using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cuna.Mutual.Beck.End.Exercise.Api.Controllers;

namespace Cuna.Mutual.Beck.End.Exercise.Api.Services
{
    public interface IThirdPartyService
    {
        void Post(MacGuffin macGuffin);
    }
    public class ThirdPartyService : IThirdPartyService
    {
        public void Post(MacGuffin macGuffin)
        {
            Console.WriteLine("This is a simulated API call to example.com");
        }
    }
}

using System;
using Cuna.Mutual.Back.End.Exercise.Api.Controllers;

namespace Cuna.Mutual.Back.End.Exercise.Api.Services
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

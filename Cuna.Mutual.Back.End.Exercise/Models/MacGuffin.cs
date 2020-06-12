using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Cuna.Mutual.Back.End.Exercise.Api.Controllers;

namespace Cuna.Mutual.Back.End.Exercise.Api.Models
{
    public class MacGuffin
    {
        protected MacGuffin()
        {
            //needed for ef 
        }

        public MacGuffin(string body)
        {
            Body = body;
        }

        public string Body { get; protected set;  }
        
        [Key]
        public int Id { get; protected set;  }


        public virtual List<Status> Statuses{ get; protected set; } = new List<Status>();

        public void UpdateStatus(Status status)
        {
            Statuses.Add(status);
        }

    }
}
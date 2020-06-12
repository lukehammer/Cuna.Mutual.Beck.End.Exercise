using System;
using System.ComponentModel.DataAnnotations;

namespace Cuna.Mutual.Back.End.Exercise.Api.Models
{
    public class Status
    {
        [Key]
        public int Id { get; protected set; }
        public DateTimeOffset Time { get; protected set; } = DateTimeOffset.Now;
        public string State { get; set; }
        public string Detail { get; set;  }

    }
}
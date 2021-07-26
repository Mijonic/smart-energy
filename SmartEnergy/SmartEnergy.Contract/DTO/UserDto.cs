using SmartEnergy.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.DTO
{
    public class UserDto
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public DateTime? BirthDay { get; set; }
        public string UserType { get; set; }
        public string ImageURL { get; set; }
        public int? CrewID { get; set; }
        public LocationDto Location { get; set; }
        public string UserStatus { get; set; }
        /* public List<Incident> Incidents { get; set; }
         public List<WorkRequest> WorkRequests { get; set; }
         public List<StateChangeHistory> StateChanges { get; set; }
         public List<SafetyDocument> SafetyDocuments { get; set; }
         public List<WorkPlan> WorkPlans { get; set; }
         public List<Notification> Notifications { get; set; }*/

        public UserDto StripConfidentialData()
        {
            Password = null;
            return this;
        }
    }
}

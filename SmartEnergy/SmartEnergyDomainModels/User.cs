
using SmartEnergy.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergyDomainModels
{
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public DateTime? BirthDay { get; set; }
        public UserType UserType { get; set; }
        public string ImageURL { get; set; }
        public int? CrewID { get; set; }
        public Crew Crew { get; set; }
        public int LocationID { get; set; }
        public Location Location { get; set; }
        public Consumer Consumer { get; set; }
        public List<Incident> Incidents { get; set; }
        public List<WorkRequest> WorkRequests { get; set; }
        public List<StateChangeHistory> StateChanges { get; set; }
        public List<SafetyDocument> SafetyDocuments { get; set; }
        public List<WorkPlan> WorkPlans { get; set; }
        public List<Notification> Notifications { get; set; }
        public UserStatus UserStatus { get; set; }

    }
}

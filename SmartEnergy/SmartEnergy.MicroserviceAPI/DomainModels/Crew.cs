using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.MicroserviceAPI.DomainModels
{
    public class Crew
    {
        public int ID { get; set; }
        public string CrewName { get; set; }
        public List<Incident> Incidents { get; set; }
        public List<User> CrewMembers { get; set; }

        public void UpdateCrew(Crew modified)
        {
            CrewName = modified.CrewName;
        }
    }
}

using iMedOneDB;
using iMedOneDB.Models;
using PatientRegistration.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace PatientWebApi.Controllers
{
    public class PatientController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetPatients() 
        {
            var patients = DBContext.GetData<TBLPATIENT>().ToList();
            return Ok(patients);
        }
        
        [HttpGet]
        public IHttpActionResult GetStates()
        {
            var states = DBContext.GetData<Tblstate>();
            return Ok(states);
        }

        [HttpGet]
        public IHttpActionResult GetCities()
        {
            var cities = DBContext.GetData<Tblcity>();
            return Ok(cities);

        }

        [HttpGet]
        public IHttpActionResult GetStateCities(int stateId)
        {
            var stateCities = DBContext.GetData<Tblcity>(stateId);
            return Ok(stateCities);

        }

        [HttpPost]
        public IHttpActionResult SavePatient([FromBody]Patient patient)
        {
            TBLPATIENT dbpatient;
            IList<TBLPATIENT> lstPatients;
            if (IsPatientExist(patient))
            {
                return BadRequest("Duplicate Patient !!!");
            }
            else
            {
                lstPatients = new List<TBLPATIENT>();
                dbpatient = new TBLPATIENT
                {
                    Name = patient.Name,
                    SurName = patient.SurName,
                    DOB = patient.DOB,
                    CityId = patient.CityId,
                    Gender = patient.Gender
                };
                lstPatients.Add(dbpatient);
                DBContext.SaveAll(lstPatients);
                return Ok("Patient Data saved !!!");
            }
        }

        private bool IsPatientExist(Patient patient)
        {
            return DBContext.GetData<TBLPATIENT>().Where(x => patient.Name.Equals(x.Name) && patient.SurName.Equals(x.SurName) && patient.Gender.Equals(x.Gender) && patient.DOB.Equals(x.DOB)).Any();
        }
    }
}

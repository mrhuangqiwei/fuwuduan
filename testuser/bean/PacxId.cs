using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testuser.bean
{
    public class PacxId
    {public string NAME { get; set; }
     public  string SEX{ get; set; }
     public string PINYIN{ get; set; }
     public string CLINICNO{ get; set; }
     public string INPATIENTNO{ get; set; }
     public string PATIENTID { get; set; }
     public string STUDYID { get; set; }
     public string AGE { get; set; }
     public string AGEUNIT { get; set; }
     public string LODGESECTION { get; set; }
     public string LODGEDOCTOR{ get; set; }
     public string LODGEDATE{ get; set; }
     public string BEDNO{ get; set; }
     public string applyNO { get; set; }
     public string applySerialNumber { get; set; }
     public string applyitem { get; set; }
     public string applyitemAll{ get; set; }
     public string applyID { get; set; }
     public string CLISINPAT{ get; set; }
     public string ENROLDOCTOR{ get; set; }
     public string ENOLDATE { get; set; }
     public string SUGERYRSESULT{ get; set; }
     public string CHECKPURPOSE { get; set; }
     public string STATUS { get; set; }
     public string CLASSNAME { get; set; }
     public string PHOTONO { get; set; }
     public string TOTALFEE { get; set; }
     public string INHOSPITALNO{ get; set; }
     public string MODALITYNAME { get; set; }
     public string CHECKDATE { get; set; }
     public string CHECKDOCTOR { get; set; }
     public string PARTOFCHECK { get; set; }
     public string reportDate { get; set; }
     public string reportDoctor { get; set; }
     public string accession_num { get; set; }





    
    }
    public class PacxIdList
    {
        public List<PacxId> GetPacxId { get; set; }
    }
}
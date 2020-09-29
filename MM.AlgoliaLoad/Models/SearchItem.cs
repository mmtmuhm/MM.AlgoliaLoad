using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace MM.AlgoliaLoad.Models
{
    public class SearchItem
    {
        public int Id { get; set; }
        public int ObjectId { get; set; }
        public string ProgramName { get; set; }
        public string ProgramDescription { get; set; }
        public int Budget { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string UniversalOutcomeId { get; set; }
        public int OutcomeId { get; set; }
        public string OutcomeName { get; set; }
        public string ImpactAreaName { get; set; }
        public string Genome { get; set; }
        public string OutcomeDescription { get; set; }
        public int AverageAchieved { get; set; }
        public int MinAchieved { get; set; }
        public int MaxAchieved { get; set; }
        public int AverageCPO { get; set; }
        public int MinCPO { get; set; }
        public int MaxCPO { get; set; }
        public int AlgoliaObjectId { get; set; }

        public string V1_10 { get; set; }
        public string V1_11 { get; set; }
        public string V1_12 { get; set; }
        public string V2_10 { get; set; }
        public string V2_11 { get; set; }

        public JObject AlgoliaItem { get; set; }


        public SearchItem() 
        {
            
        
        }

        public SearchItem(mmx_map_data data)
        {
            if (data.ImpactArea != null)
            {
                V1_10 = string.Format("{0}", data.ImpactArea);
                V1_11 = string.Format("{0} > {1}", data.ImpactArea, data.ImpactArea);
                V1_12 = string.Format("{0} > {1} > {2}", data.ImpactArea, data.Genome, data.OutcomeName);
            }

            V2_10 = string.Format("{0}", data.State);
            V2_11 = string.Format("{0} > {1}", data.State, data.City);

            AlgoliaItem = new JObject
            {

                ["Id"] = data.id,
                ["ObjectId"] = data.object_id,
                ["ProgramName"] = data.ProgramName,
                ["ProgramDescription"] = data.ProgramDescription,
                ["Budget"] = data.Budget,
                ["Address"] = data.Address,
                ["City"] = data.City,
                ["State"] = data.State,
                ["ZipCode"] = data.Zip_Code,
                ["UniversalOutcomeId"] = data.UniversalOutcomeId,
                ["OutcomeId"] = data.OutcomeId,
                ["OutcomeName"] = data.OutcomeName,
                ["ImpactAreaName"] = data.ImpactArea,
                ["Genome"] = data.Genome,
                ["OutcomeDescription"] = data.OutcomeDescription,
                ["AverageAchieved"] = data.AverageAcheived,
                ["MinAchieved"] = data.MinAcheived,
                ["MaxAchieved"] = data.MaxAcheived,
                ["AverageCPO"] = data.AverageCPO,
                ["MinCPO"] = data.MinCPO,
                ["MaxCPO"] = data.MaxCPO,
                ["V1_10"] = V1_10,
                ["V1_11"] = V1_11,
                ["V1_12"] = V1_12,
                ["V2_10"] = V2_10,
                ["V2_11"] = V2_11

            };
        }
    }

}



//ein varchar(15) DEFAULT NULL,
// OrgName varchar(75) DEFAULT NULL,
// ProgramName longtext DEFAULT NULL,
//  ProgramDescription longtext DEFAULT NULL,
//  Adjusted bigint(20) DEFAULT NULL,
//  Outcomes bigint(20) NOT NULL,
//  CPO bigint(20) NOT NULL,
//  Description longtext NOT NULL,
//  Budget bigint(20) DEFAULT NULL,
//  Address varchar(500) DEFAULT NULL,
//  City varchar(255) DEFAULT NULL,
//  State varchar(255) DEFAULT NULL,
//  Zip_Code varchar(255) DEFAULT NULL,
//  lat varchar(255) DEFAULT NULL,
//  lon varchar(255) DEFAULT NULL,
//  UniversalOutcomeId varchar(255) DEFAULT NULL,
//  OutcomeId int (11) DEFAULT NULL,
//   OutcomeName varchar(255) DEFAULT NULL,
//   ImpactArea varchar(255) DEFAULT NULL,
//   Genome varchar(255) DEFAULT NULL,
//   OutcomeDescription text DEFAULT NULL,
//  AverageAcheived bigint(20) DEFAULT NULL,
//  MinAcheived bigint(20) DEFAULT NULL,
//  MaxAcheived bigint(20) DEFAULT NULL,
//  AverageCPO bigint(20) DEFAULT NULL,
//  MinCPO bigint(20) DEFAULT NULL,
//  MaxCPO bigint(20) DEFAULT NULL,
//  AlgoliaObjectId bigint(20) DEFAULT NULL,
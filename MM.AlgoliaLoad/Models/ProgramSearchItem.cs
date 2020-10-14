using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace MM.AlgoliaLoad.Models
{
    public class ProgramSearchItem
    {
        public int Id { get; set; }
        public int ObjectId { get; set; }
        public string OrganizationName { get; set; }
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
        public string EIN { get; set; }
        public string V1_10 { get; set; }
        public string V1_11 { get; set; }
        public string V1_12 { get; set; }
        public string V2_10 { get; set; }
        public string V2_11 { get; set; }

        public JObject AlgoliaItem { get; set; }


        public ProgramSearchItem() 
        {
            
        
        }

        public ProgramSearchItem(mmx_map_data data)
        {
            if (data.ImpactArea != null)
            {
                V1_10 = string.Format("{0}", data.ImpactArea);
                V1_11 = string.Format("{0} > {1}", data.ImpactArea, data.Genome);
                V1_12 = string.Format("{0} > {1} > {2}", data.ImpactArea, data.Genome, data.OutcomeName);
            }

            V2_10 = string.Format("{0}", data.State);
            V2_11 = string.Format("{0} > {1}", data.State, data.City);

            AlgoliaItem = new JObject
            {

                ["Id"] = data.id,
                ["ObjectId"] = data.object_id,
                ["OrganizationName"] = data.OrgName,
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
                ["EIN"] = data.ein,
                ["V1_10"] = V1_10,
                ["V1_11"] = V1_11,
                ["V1_12"] = V1_12,
                ["V2_10"] = V2_10,
                ["V2_11"] = V2_11

            };
        }
    }

}



using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.AlgoliaLoad.Models
{
    public class OutcomesSeaerchItem
    {
        public JObject AlgoliaItem { get; set; }

        public OutcomesSeaerchItem() { }


        public OutcomesSeaerchItem(v_activeoutcomes outcome) 
        {
            AlgoliaItem = new JObject()
            {
                ["OutcomeId"] = outcome.outcomeid,
                ["Name"] = outcome.name,
                ["Description"] = outcome.description,
                ["Definition"] = outcome.definition,
                ["UniversalOutcomeId"] = outcome.universaloutcomeid,
                ["ImpactArea"] = outcome.impactarea,
                ["IsUniversal"] = outcome.isuniversal,
                ["Genome"] = outcome.genomename,
                ["V1_10"] = string.Format($"{outcome.impactarea}"),
                ["V1_11"] = string.Format($"{outcome.impactarea} > {outcome.genomename}")
            };

            
        }
    }
}

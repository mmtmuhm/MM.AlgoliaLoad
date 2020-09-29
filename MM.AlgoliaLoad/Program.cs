using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Algolia.Search.Clients;
using MM.AlgoliaLoad.Models;

namespace MM.AlgoliaLoad
{
    class Program
    {
        static void Main(string[] args)
        {
            GetData();
        }




        public static void GetData()
        {

            using (mmxEntities db = new mmxEntities())
            {
                var data = db.mmx_map_data.ToList();

                
                foreach (var i in data)
                {
                    UpdateData(i.id);
                }
            
            }
        }

        public static void UpdateData(long id)
        {
            using (mmxEntities db = new mmxEntities())
            {
                var item = db.mmx_map_data.Find(id);

                int ein = Convert.ToInt32(item.ein);


                var outcomeLookupItem = db.ntee_outcomes_lookup.Where(_ => _.EIN == ein).FirstOrDefault();

                if (outcomeLookupItem != null)
                {

                    var outcome = GetOutcomeInfo((int)outcomeLookupItem.outcomeid);

                    if (outcome != null)
                    {
                        item.OutcomeId = outcomeLookupItem.outcomeid;

                        item.UniversalOutcomeId = outcome.universaloutcomeid;
                        item.OutcomeDescription = outcome.description;
                        item.OutcomeName = outcome.name;
                        item.Genome = outcome.genomename;
                        item.ImpactArea = outcome.impactarea;

                        var benchMarks = GetLatestbenchmarks(item.UniversalOutcomeId);


                        if (benchMarks != null)
                        {
                            item.AverageCPO = benchMarks.avg_cpo;
                            item.MinCPO = benchMarks.min_cpo;
                            item.MaxCPO = benchMarks.max_cpo;

                            item.AverageAcheived = item.Budget / benchMarks.avg_cpo;
                            item.MinAcheived = item.Budget / benchMarks.min_cpo;
                            item.MaxAcheived = item.Budget / benchMarks.max_cpo;

                            Console.WriteLine(item.id);
                        }
                    }
                }

                var returnId = LoadAlgolia(item);

                item.AlgoliaObjectId = Convert.ToInt32(returnId);

                Console.WriteLine("Object Id: " + item.object_id + " | Algolia Id: " + item.AlgoliaObjectId);

                db.SaveChanges();
            }
        }


        public static string LoadAlgolia(mmx_map_data item)
        {
            SearchClient client = new SearchClient("VMEXWG7ZKH", "3982201c906d85c7556e7ffd933d7422");

            SearchIndex index = client.InitIndex("990_v3");


            var algoliaItem = new SearchItem(item);

            var returnVal = index.SaveObject(algoliaItem.AlgoliaItem, autoGenerateObjectId: true);

            return returnVal.Responses[0].ObjectIDs.FirstOrDefault();
        }






        public static v_latestbenchmarks GetLatestbenchmarks(string uoid) 
        {
            using (taxonomy_dbEntities db = new taxonomy_dbEntities())
            {

                var i = db.v_latestbenchmarks.Where(_ => _.universalOutcomeId == uoid).FirstOrDefault();

                return i;
            }
        }

        public static v_activeoutcomes GetOutcomeInfo(int outcomeid) 
        {
            using (taxonomy_dbEntities db = new taxonomy_dbEntities())
            {
                var outcome = db.v_activeoutcomes.Where(_ => _.outcomeid == outcomeid).FirstOrDefault();

                return outcome;
                
            }
        }
    }
}

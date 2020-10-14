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
            //GetData();

            //LoadAlgoliaBatch();
            //LoadArticleData();
            //LoadGenes();
            LoadOutcomes();
        }

        public static void LoadGenes()
        {
            using (taxonomy_dbEntities db = new taxonomy_dbEntities())
            {
                var data = db.v_genes.ToList();


                var searchItems = new List<JObject>();
                var index = 0;
                foreach (var i in data)
                {


                    var algoliaItem = new AcivityGeneSearchItem(i);

                    searchItems.Add(algoliaItem.AlgoliaItem);

                    Console.WriteLine("Gene Id: " + i.geneid);

                    if (index == 1000)
                    {
                        SearchClient client = new SearchClient("VMEXWG7ZKH", "3982201c906d85c7556e7ffd933d7422");

                        SearchIndex searchIndex = client.InitIndex("Genes");

                        searchIndex.SaveObjects(searchItems, autoGenerateObjectId: true);

                        Console.WriteLine("data sent");
                        index = 0;
                        searchItems = new List<JObject>();
                    }
                    else
                    {

                        index++;
                    }
                }

            }
        }

        public static void LoadOutcomes()
        {
            using (taxonomy_dbEntities db = new taxonomy_dbEntities())
            {
                var data = db.v_activeoutcomes.ToList();


                var searchItems = new List<JObject>();

                foreach (var i in data)
                {


                    var algoliaItem = new OutcomesSeaerchItem(i);

                    searchItems.Add(algoliaItem.AlgoliaItem);

                    Console.WriteLine("Outcome Id: " + i.outcomeid);


                    SearchClient client = new SearchClient("VMEXWG7ZKH", "3982201c906d85c7556e7ffd933d7422");

                    SearchIndex searchIndex = client.InitIndex("Outcomes");

                    searchIndex.SaveObjects(searchItems, autoGenerateObjectId: true);

                    Console.WriteLine("data sent");

                    searchItems = new List<JObject>();

                }

            }
        }


        public static void LoadArticleData() 
        {
            using (GenomeManagerData db = new GenomeManagerData()) 
            {
                var data = db.genomedatas.Where(_ => _.genomeid != 4).ToList();


                var searchItems = new List<JObject>();
                var index = 0;
                foreach (var i in data)
                {
                    

                    var algoliaItem = new ArticleSearchItem(i);

                    searchItems.Add(algoliaItem.AlgoliaItem);

                    Console.WriteLine("Object Id: " + i.id);

                    if (index == 1000)
                    {
                        SearchClient client = new SearchClient("VMEXWG7ZKH", "3982201c906d85c7556e7ffd933d7422");

                        SearchIndex searchIndex = client.InitIndex("Articles");

                        searchIndex.SaveObjects(searchItems, autoGenerateObjectId: true);

                        Console.WriteLine("data sent");
                        index = 0;
                        searchItems = new List<JObject>();
                    }
                    else
                    {

                        index++;
                    }
                }

            }
        }


        public static void LoadAlgoliaBatch()
        {

            using (mmxEntities db = new mmxEntities())
            {
                var data = db.mmx_map_data.Where(_ => _.OutcomeId != null).Take(10000).ToList();

                var searchItems = new List<JObject>();
                var index = 0;
                foreach (var i in data)
                {


                    var algoliaItem = new ProgramSearchItem(i);

                    searchItems.Add(algoliaItem.AlgoliaItem);

                    Console.WriteLine("Object Id: " + i.ein);

                    if (index == 1000)
                    {
                        SearchClient client = new SearchClient("VMEXWG7ZKH", "3982201c906d85c7556e7ffd933d7422");

                        SearchIndex searchIndex = client.InitIndex("990_v5");

                        searchIndex.SaveObjects(searchItems, autoGenerateObjectId: true);

                        Console.WriteLine("data sent");
                        index = 0;
                        searchItems = new List<JObject>();
                    }
                    else
                    {

                        index++;
                    }
                }
            }
        }



        public static void GetData()
        {

            using (mmxEntities db = new mmxEntities())
            {
                var data = db.mmx_map_data.Where(_ => _.AlgoliaObjectId == null && _.OutcomeId != null).Take(10000).ToList();

                
                foreach (var i in data)
                {
                    UpdateData(i.id);
                }
            
            }
        }

        public static void UpdateData(long id)
        {
            var searchItems = new JArray();
            using (mmxEntities db = new mmxEntities())
            {
                var item = db.mmx_map_data.Find(id);

                //int ein = Convert.ToInt32(item.ein);


                //var outcomeLookupItem = db.ntee_outcomes_lookup.Where(_ => _.EIN == ein).FirstOrDefault();

                //if (outcomeLookupItem != null)
                //{

                //    var outcome = GetOutcomeInfo((int)outcomeLookupItem.outcomeid);

                //    if (outcome != null)
                //    {
                //        item.OutcomeId = outcomeLookupItem.outcomeid;

                //        item.UniversalOutcomeId = outcome.universaloutcomeid;
                //        item.OutcomeDescription = outcome.description;
                //        item.OutcomeName = outcome.name;
                //        item.Genome = outcome.genomename;
                //        item.ImpactArea = outcome.impactarea;

                //        var benchMarks = GetLatestbenchmarks(item.UniversalOutcomeId);


                //        if (benchMarks != null)
                //        {
                //            item.AverageCPO = benchMarks.avg_cpo;
                //            item.MinCPO = benchMarks.min_cpo;
                //            item.MaxCPO = benchMarks.max_cpo;

                //            item.AverageAcheived = item.Budget / benchMarks.avg_cpo;
                //            item.MinAcheived = item.Budget / benchMarks.min_cpo;
                //            item.MaxAcheived = item.Budget / benchMarks.max_cpo;

                //            Console.WriteLine(item.id);
                //        }
                //    }
                //}

                var returnId = LoadAlgolia(item);

                item.AlgoliaObjectId = Convert.ToInt32(returnId);

                Console.WriteLine("Object Id: " + item.object_id + " | Algolia Id: " + item.AlgoliaObjectId);

                db.SaveChanges();
            }
        }




        public static string LoadAlgolia(mmx_map_data item)
        {
            SearchClient client = new SearchClient("VMEXWG7ZKH", "3982201c906d85c7556e7ffd933d7422");

            SearchIndex index = client.InitIndex("990_v4");


            var algoliaItem = new ProgramSearchItem(item);

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

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.AlgoliaLoad.Models
{
    public class ArticleSearchItem
    {

        public int GdataId { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Authors { get; set; }
        public int PublishedYear { get; set; }
        public List<string> Outcomes { get; set; }
        public string OutcomeName { get; set; }
        public string ImpactAreaName { get; set; }
        public string Genome { get; set; }
        public JObject AlgoliaItem { get; set; }

        public ArticleSearchItem() { }


        public ArticleSearchItem(genomedata data)
        {
            var authors = string.Empty;

            if (data.author1 != null) 
            {
                authors += data.author1;
            }

            if (data.author2 != null)
            {
                authors += string.Format($", {data.author2}");
            }

            if (data.author3 != null)
            {
                authors += string.Format($", {data.author3}");
            }

            if (data.author4 != null)
            {
                authors += string.Format($", {data.author4}");
            }

            if (data.author5more != null)
            {
                authors += string.Format($", {data.author5more}");
            }


            using (GenomeManagerData db = new GenomeManagerData())
            {
                var oData = db.coding_ns_stem_outcomes.Where(_ => _.gdataid == data.id).FirstOrDefault();

                var genome = db.genomes.Where(_ => _.id == data.genomeid).FirstOrDefault();

                var impactarea = db.genome_groups.Where(_ => _.id == genome.groupid).FirstOrDefault();

                var v1_12 = string.Format($"{impactarea.groupname} > {genome.name}");
                if (oData != null) 
                {
                    v1_12 = string.Format($"{impactarea.groupname} > {genome.name} > {oData.standardized_outcome}");
                }

                AlgoliaItem = new JObject()
                {
                    ["GdataId"] = data.id,
                    ["Title"] = data.documenttitle,
                    ["Abstract"] = data.abstractsummary,
                    ["Authors"] = authors,
                    ["Year"] = data.yearpublished,
                    ["Outcomes"] = GetOutcomes(data.id, (int)data.genomeid),
                    ["V1_10"] = impactarea.groupname,
                    ["V1_11"] = string.Format($"{impactarea.groupname} > {genome.name}"),
                    ["V1_12"] = v1_12,
                    ["V2_10"] = data.yearpublished
                };

            }
        }



        public string GetGenome(long gdataid, int genomeid)
        {
            using (GenomeManagerData db = new GenomeManagerData())
            {
                string filter = string.Empty;

                var data = db.coding_ns_stem_outcomes.Where(_ => _.gdataid == gdataid).FirstOrDefault();

                var genome = db.genomes.Where(_ => _.id == genomeid).FirstOrDefault();

                var impactarea = db.genome_groups.Where(_ => _.id == genome.groupid).FirstOrDefault();

                


                return filter;
            }
        }


        private JArray GetOutcomes(long gdataid, int genomeid) 
        {
            using (GenomeManagerData db = new GenomeManagerData())
            {
                var data = db.coding_ns_stem_outcomes.Where(_ => _.gdataid == gdataid).ToList();

                var genome = db.genomes.Where(_ => _.id == genomeid).FirstOrDefault();

                var impactarea = db.genome_groups.Where(_ => _.id == genome.groupid).FirstOrDefault();

                var arr = new JArray();
                foreach (var o in data)
                {
                    var stdOutcomes = db.master_outcome_list.Where(_ => _.std_outcome_name == o.standardized_outcome).FirstOrDefault();

                    var universalid = string.Empty;
                    if (stdOutcomes != null) 
                    {
                        universalid = stdOutcomes.universalOutcomeId + " - ";
                    }

                    var outcome = string.Format($"{universalid}{impactarea.groupname} - {genome.name} - {o.standardized_outcome}");

                    arr.Add(outcome);
                }

                return arr;
            }
        }
    }
}

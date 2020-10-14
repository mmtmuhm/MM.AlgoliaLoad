using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.AlgoliaLoad.Models
{
    public class AcivityGeneSearchItem
    {
        public JObject AlgoliaItem { get; set; }

        public AcivityGeneSearchItem() { }


        public AcivityGeneSearchItem(v_genes gene)
        {
            AlgoliaItem = new JObject()
            {
                ["GeneId"] = gene.geneid,
                ["Name"] = gene.name,
                ["Description"] = gene.description,
                ["Chromosome"] = gene.chromosomename,
                ["ImpactArea"] = gene.impactarea,
                ["Genome"] = gene.genome,
                ["V1_10"] = string.Format($"{gene.impactarea}"),
                ["V1_11"] = string.Format($"{gene.impactarea} > {gene.genome}"),
                ["V1_11"] = string.Format($"{gene.impactarea} > {gene.genome} > {gene.chromosomename}")
            };


        }
    }
}
